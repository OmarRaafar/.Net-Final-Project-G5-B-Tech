using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ModelsMAFP;
using ModelsMAFP.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DbContextMAFP
{
    public class ASPDbContextMAFP: DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public virtual DbSet<ProductMAFP> products { get; set; }
        public virtual DbSet<CategoryMAFP> Categories { get; set; }
        public virtual DbSet<CustomerMAFP> Customers { get; set; }
        public virtual DbSet<OrderMAFP> Orders { get; set; }
        public ASPDbContextMAFP(DbContextOptions<ASPDbContextMAFP> dbContextOptions, IHttpContextAccessor httpContextAccessor) 
            : base(dbContextOptions) { _httpContextAccessor = httpContextAccessor; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MVCTrialBook;Integrated Security=True;Encrypt=True;" +
              "TrustServerCertificate=true");


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userId = GetCurrentUserId(); // Method to get the current user's ID

            foreach (var entry in ChangeTracker.Entries<BaseEntityMAFP>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = userId;
                    entry.Entity.Created = DateTime.Now;
                    entry.Entity.IsDeleted = false; 
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedBy = userId;
                    entry.Entity.Updated = DateTime.Now;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        private int GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId != null ? int.Parse(userId) : 0;
        }
    }
}
