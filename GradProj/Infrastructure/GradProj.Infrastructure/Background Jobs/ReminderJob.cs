﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradProj.Application;
using GradProj.Application.ServiceAbs;
using GradProj.Domain.Entities;
using GradProj.Domain.RepositoryAbs;
using Microsoft.AspNetCore.SignalR;

namespace GradProj.Infrastructure.Background_Jobs
{
    public class ReminderJob
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly INotificationService _notificationService;
        private readonly IHubContext<NotificationHub> _hubContext; // SignalR context

        public ReminderJob(
            IReminderRepository reminderRepository,
            INotificationService notificationService,
            IHubContext<NotificationHub> hubContext)
        {
            _reminderRepository = reminderRepository;
            _notificationService = notificationService;
            _hubContext = hubContext;
        }

        public async Task ExecuteAsync()
        {
            var dueReminders = await _reminderRepository
                .GetListGetWhere(r => r.ReminderTime <= DateTime.Now && !r.isReminded);

            foreach (var reminder in dueReminders)
            {
                var notification = new Notification
                {
                    UserId = reminder.UserId,
                    Message = reminder.Message,
                    CreatedAt = DateTime.Now
                };

                await _notificationService.CreateAsync(notification);

                // ✅ SignalR üzerinden notification gönder
                await _hubContext.Clients.User(reminder.UserId.ToString())
                    .SendAsync("ReceiveNotification", notification);

                reminder.isReminded = true;
                 _reminderRepository.UpdateAsync(reminder);
            }
        }
    }
}
    

