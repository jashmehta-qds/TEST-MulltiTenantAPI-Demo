using TEST_MulltiTenantAPI_Demo.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TEST_MulltiTenantAPI_Demo.Entity.Context
{
    public partial class SlaveDbContext : DbContext
    {
        private HttpContext httpContext;

        public SlaveDbContext(DbContextOptions<SlaveDbContext> options, IHttpContextAccessor httpContextAccessor = null) : base(options)
        {
            httpContext = httpContextAccessor?.HttpContext;
        }

        public DbSet<TaxAccountNumber> TaxAccountNumbers { get; set; }

        

        //lazy-loading
        //https://entityframeworkcore.com/querying-data-loading-eager-lazy
        //https://docs.microsoft.com/en-us/ef/core/querying/related-data
        //EF Core will enable lazy-loading for any navigation property that is virtual and in an entity class that can be inherited
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
           
                var clientClaim = httpContext?.User.Claims.Where(c => c.Type == ClaimTypes.GroupSid).Select(c => c.Value).SingleOrDefault();
                if (clientClaim == null)
                {
                    optionsBuilder.UseInMemoryDatabase("TestDB-" + Guid.NewGuid().ToString());
                }
                // Let's say there is no http context, like when you update-database from PMC
                else
                {
                    optionsBuilder.UseInMemoryDatabase("TestDB-" + Guid.NewGuid().ToString());
                }

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API
         
            //concurrency
            modelBuilder.Entity<TaxAccountNumber>().Property(a => a.RowVersion).IsRowVersion();


            SetAdditionalConcurency(modelBuilder);
        }

        partial void SetAdditionalConcurency(ModelBuilder modelBuilder);

        public override int SaveChanges()
        {
            Audit();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            Audit();
            return await base.SaveChangesAsync();
        }

        private void Audit()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).Created = DateTime.UtcNow;
                }
            ((BaseEntity)entry.Entity).Modified = DateTime.UtcNow;
            }
        }

    }
}


