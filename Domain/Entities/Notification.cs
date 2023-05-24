using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Notification:BaseEntity
    {
        public int ObserverId { get; set; }
        public Profile Observer { get; set; }

        public int? TargetId { get; set; }
        public Profile? Target { get; set; }
        public NotificationType NotificationType { get; set; }
        public int? AdvertiseId { get; set; }//confirmed Advertise ID
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsChecked { get;set; }
        public DateTime CheckedDate { get; set; }

    }
}
