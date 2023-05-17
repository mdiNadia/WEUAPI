using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TransferValueHistory: BaseCreatioDate
    {
        public int ObserverId { get; set; }
        public Profile Observer { get; set; }

        public int TargetId { get; set; }
        public Profile Target { get; set; }

    }
}
