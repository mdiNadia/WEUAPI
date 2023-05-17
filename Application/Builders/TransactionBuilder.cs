﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class TransactionBuilder : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne<TransactionType>(x => x.TransactionType).WithOne(x => x.Transaction).HasForeignKey<TransactionType>(x => x.TransactionId);
            builder.HasOne<TransactionStatus>(x => x.TransactionStatus).WithOne(x => x.Transaction).HasForeignKey<TransactionStatus>(x => x.TransactionId);

        }
    }
}
