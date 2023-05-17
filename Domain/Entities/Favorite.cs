﻿using Domain.Common;

namespace Domain.Entities
{
    public class Favorite : BaseCreatioDate
    {
        public int ObserverId { get; set; }
        public Profile Observer { get; set; }

        public int TargetId { get; set; }
        public ConfirmResult Target { get; set; }
    }
}
