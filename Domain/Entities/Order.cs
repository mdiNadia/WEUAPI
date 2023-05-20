using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order: BaseEntity
    {
        public string OrderNum { get; set; }
        public bool IsPaid { get; set; }

        public ICollection<OrderRow> OrderRows { get; set; }
        public int ProfileId { get;set; }
        public Profile Profile { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

    }
}
