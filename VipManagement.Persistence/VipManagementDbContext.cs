using Microsoft.EntityFrameworkCore;
using VipManagement.Domain.Cards.Entities;

namespace VipManagement.Persistence
{



    public class VipManagementDbContext : DbContext
    {
        public VipManagementDbContext(DbContextOptions<VipManagementDbContext> options)
            : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ✅ AIXÒ ÉS EL QUE BUSQUES
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VipManagementDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }



}
