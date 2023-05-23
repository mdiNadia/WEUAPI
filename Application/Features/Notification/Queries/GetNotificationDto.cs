using Application.Dtos.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notification.Queries
{
    public class GetNotificationDto
    {
        public int Id { get; set; } 
        public DateTime CreationDate { get; set; }  
        public NotificationType NotificationType { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public GetNameAndId Observer { get; set; }
        public GetNameAndId Targeter { get; set; }
    }
}
