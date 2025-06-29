﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradProj.Domain.Entities
{
    public class Reminder:BaseEntity
    {
        public Guid UserId { get; set; }

        [EnumDataType(typeof(ReferenceType))]
        public ReferenceType referenceType { get; set; } // The type of the related entity (e.g., Task, Event) 

        public Guid ReferenceID { get; set; } // The ID of the related task or event 
        public DateTime ReminderTime { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool isReminded { get; set; } = false;

        //[ForeignKey(nameof(ReferenceID))]
        //...Burada hem task olabilir hem event, nasil FK tanimlayacagiz?

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }
        



    }
    public enum ReferenceType  
    {
        Task,
        Event
    }
   

}
