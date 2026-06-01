using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VipManagement.Domain.Cards.Entities;

namespace VipManagement.Persistence.Cards.EntityTypeConfiguration
{
    public class CardsEntityTypeConfiguration : IEntityTypeConfiguration<Card>
    {
        // Aquest metode s'executa quan EF construeix el model
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(x => x.Id); // Table primaria de la tabla
            builder.Property(x => x.Id). // Indiques anem a configurar la peropietat Id
                HasConversion(v => v.Value, v => new CardId(v)).ValueGeneratedOnAdd(); // Aquest valor es genera quan s'inserta el registre



           
        }
    }
}
