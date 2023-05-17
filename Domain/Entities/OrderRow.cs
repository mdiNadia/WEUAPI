using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderRow : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //آیدی محصول یا آگهی که باعث ایجاد این ردیف شده 
        public int TargetId { get; set; }
        public char sign { get; set; }
       
        public int OrderId { get; set; }
        public Order Order { get; set; }
        //1- کیف پولی - wallet
        //2- خرید محصول - product
        public OrderType OrderType { get; set; }
        //1- شارژ کیف پول با سکه
        //2- گرفتن سکه از کیف پول
        //3- برداشت سکه از کیف پول
        //4- انتقال سکه
        public Domain.Enums.WalletType TransactionType { get; set; }


    }
}
