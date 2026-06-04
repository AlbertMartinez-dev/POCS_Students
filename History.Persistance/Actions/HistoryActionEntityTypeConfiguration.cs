using History.Domain.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace History.Persistance.Actions
{

    public class HistoryActionEntityTypeConfiguration : IEntityTypeConfiguration<HistoryAction>
    {
        public void Configure(EntityTypeBuilder<HistoryAction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Type)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Domain)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.HistoryId);

            builder.Property(x => x.CreatedOn)
                .IsRequired();
        }
    }

}
