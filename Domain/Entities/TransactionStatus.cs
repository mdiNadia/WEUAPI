using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities
{
    //1 عملیات با موفقیت انجام شد
    //2 با خطا مواجه شد
    public class TransactionStatus:BaseEntity
    {
        public Enums.TransactionStatus TransactionStatusEnum { get; set; }
   

        //رابطه
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
