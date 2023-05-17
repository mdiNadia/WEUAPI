﻿namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }

}
