using Avalanche.Core.Domain.Common;
using Avalanche.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Avalanche.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<Affiliate> Affiliate { get; set; }
        public DbSet<AffiliatePolicy> AffiliatePolicie { get; set; }
        public DbSet<Analyst> Analysts { get; set; }
        public DbSet<Authorization> Authorization { get; set; }
        public DbSet<AuthorizationType> AuthorizationType { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Coverage> Coverage { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<Hospital> Hospital { get; set; }
        public DbSet<InstitutionType> InstitutionType { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Plan> Plan { get; set; }
        public DbSet<PlanCoverage> PlanCoverage { get; set; }
        public DbSet<Policy> Policy { get; set; }
        public DbSet<Status> Status { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.UtcNow;
                        entry.Entity.CreatedBy = "DefaultBaseUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = "DefaultBaseUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API
            base.OnModelCreating(modelBuilder);

            #region Tables
            modelBuilder.Entity<Affiliate>()
                .ToTable("Affiliates");

            modelBuilder.Entity<AffiliatePolicy>()
                .ToTable("AffiliatePolicies");

            modelBuilder.Entity<Analyst>()
                .ToTable("Analysts");

            modelBuilder.Entity<Authorization>()
                .ToTable("Authorizations");

            modelBuilder.Entity<AuthorizationType>()
                .ToTable("AuthorizationTypes");

            modelBuilder.Entity<Client>()
                .ToTable("Clients");

            modelBuilder.Entity<Coverage>()
                .ToTable("Coverages");

            modelBuilder.Entity<DocumentType>()
                .ToTable("DocumentTypes");

            modelBuilder.Entity<Hospital>()
                .ToTable("Hospitals");

            modelBuilder.Entity<InstitutionType>()
                .ToTable("InstitutionTypes");

            modelBuilder.Entity<Notification>()
                .ToTable("Notifications");

            modelBuilder.Entity<Plan>()
                .ToTable("Plans");

            modelBuilder.Entity<PlanCoverage>()
                .ToTable("PlanCoverages");

            modelBuilder.Entity<Policy>()
                .ToTable("Policies");

            modelBuilder.Entity<Status>()
                .ToTable("Statuses");
            #endregion

            #region Primary keys
            modelBuilder.Entity<Affiliate>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<AffiliatePolicy>()
                .HasKey(x => x.Id);
            
            modelBuilder.Entity<Analyst>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Authorization>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<AuthorizationType>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Client>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Coverage>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<DocumentType>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Hospital>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<InstitutionType>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Notification>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Plan>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<PlanCoverage>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Policy>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Status>()
                .HasKey(x => x.Id);
            #endregion

            #region Relationships
            modelBuilder.Entity<Affiliate>()
                .HasOne<Client>(x => x.Client)
                .WithMany(x => x.Affiliates)
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Affiliate>()
                .HasOne<DocumentType>(x => x.DocumentType)
                .WithMany(x => x.Affiliates)
                .HasForeignKey(x => x.DocumentTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Affiliate>()
                .HasOne<Status>(x => x.Status)
                .WithMany(x => x.Affiliates)
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Affiliate>()
                .HasMany<AffiliatePolicy>(x => x.AffiliatePolicies)
                .WithOne(x => x.Affiliate)
                .HasForeignKey(x => x.AffiliateId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Affiliate>()
                .HasMany<Authorization>(x => x.Authorizations)
                .WithOne(x => x.Affiliate)
                .HasForeignKey(x => x.AffiliateId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AffiliatePolicy>()
                .HasOne<Policy>(x => x.Policy)
                .WithMany(x => x.AffiliatePolicies)
                .HasForeignKey(x => x.PolicyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AffiliatePolicy>()
                .HasOne<Status>(x => x.Status)
                .WithMany(x => x.AffiliatePolicies)
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Analyst>()
                .HasMany<Authorization>(x => x.Authorizations)
                .WithOne(x => x.Analyst)
                .HasForeignKey(x => x.AssignedAnalyst)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Authorization>()
                .HasOne<Status>(x => x.Status)
                .WithMany(x => x.Authorizations)
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Authorization>()
                .HasOne<AuthorizationType>(x => x.AuthorizationType)
                .WithMany(x => x.Authorizations)
                .HasForeignKey(x => x.AuthorizationTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Authorization>()
                .HasOne<Policy>(x => x.Policy)
                .WithMany(x => x.Authorizations)
                .HasForeignKey(x => x.PolicyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Authorization>()
                .HasOne<Hospital>(x => x.Hospital)
                .WithMany(x => x.Authorizations)
                .HasForeignKey(x => x.HospitalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Client>()
                .HasOne<DocumentType>(x => x.DocumentType)
                .WithMany(x => x.Clients)
                .HasForeignKey(x => x.DocumentTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Client>()
                .HasOne<Status>(x => x.Status)
                .WithMany(x => x.Clients)
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Client>()
                .HasMany<Policy>(x => x.Policies)
                .WithOne(x => x.Client)
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Coverage>()
                .HasMany<PlanCoverage>(x => x.PlanCoverages)
                .WithOne(x => x.Coverage)
                .HasForeignKey(x => x.CoverageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Hospital>()
                .HasOne<InstitutionType>(x => x.InstitutionType)
                .WithMany(x => x.Hospitals)
                .HasForeignKey(x => x.InstitutionTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Hospital>()
                .HasOne<Status>(x => x.Status)
                .WithMany(x => x.Hospitals)
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notification>()
                .HasOne<Analyst>(x => x.Analyst)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.AssignedAnalyst)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notification>()
                .HasOne<Authorization>(x => x.Authorization)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.AuthorizationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Plan>()
                .HasMany<Policy>(x => x.Policies)
                .WithOne(x => x.Plan)
                .HasForeignKey(x => x.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Plan>()
                .HasMany<PlanCoverage>(x => x.PlanCoverages)
                .WithOne(x => x.Plan)
                .HasForeignKey(x => x.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Policy>()
                .HasOne<Status>(x => x.Status)
                .WithMany(x => x.Policies)
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Property configurations
            modelBuilder.Entity<Affiliate>().HasIndex(x => x.DocumentNumber).IsUnique();

            modelBuilder.Entity<Authorization>().HasIndex(x => x.AssignedAnalyst);

            modelBuilder.Entity<Authorization>().HasIndex(x => x.HospitalApplicationId).IsUnique();

            modelBuilder.Entity<Client>().HasIndex(x => x.DocumentNumber).IsUnique();

            modelBuilder.Entity<Policy>().HasIndex(x => x.Number).IsUnique();
            #endregion
        }

        public void TruncateTables()
        {
            SaveChanges();
        }
    }
}
