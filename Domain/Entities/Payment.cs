using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment : BaseEntity
    {
        //شماره پیگیری فاکتور موجود
        public long ReferenceNumber { get; set; }

        public long SaleReferenceId { get; set; }

        public bool StatusPayment{get;set;}
        public string BankName { get;set; }
        public int ProfileId { get; set; }  
    
    }
}
