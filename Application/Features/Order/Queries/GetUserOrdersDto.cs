using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Queries
{
    public class GetUserOrdersDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public char sign { get; set; }
        public OrderType OrderType { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
