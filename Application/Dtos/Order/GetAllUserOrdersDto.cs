using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Order
{
    public class GetAllUserOrdersDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public char sign { get; set; }
        public Domain.Enums.OrderType OrderType { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
