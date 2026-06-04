using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using History.Domain.Actions;

namespace History.Persistance
{
   

        public class HistoryDbContext : DbContext
        {
            public HistoryDbContext(DbContextOptions<HistoryDbContext> options)
                : base(options)
            {
            }

            public DbSet<HistoryAction> HistoryActions { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(typeof(HistoryDbContext).Assembly);

                base.OnModelCreating(modelBuilder);
            }
        }





    
}
