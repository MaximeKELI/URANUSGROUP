using Microsoft.EntityFrameworkCore;
using UranusGroup.Models;

namespace UranusGroup.Data
{
    public class UranusGroupContext : DbContext
    {
        public UranusGroupContext(DbContextOptions<UranusGroupContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceFeature> ServiceFeatures { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Contact entity
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Subject).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Message).IsRequired().HasMaxLength(2000);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // Configure Service entity
            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
                entity.Property(e => e.LongDescription).HasMaxLength(1000);
                entity.Property(e => e.Icon).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // Configure ServiceFeature entity
            modelBuilder.Entity<ServiceFeature>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Feature).IsRequired().HasMaxLength(200);
                entity.HasOne(e => e.Service)
                      .WithMany(s => s.Features)
                      .HasForeignKey(e => e.ServiceId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Newsletter entity
            modelBuilder.Entity<Newsletter>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.SubscribedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.Source).HasMaxLength(50);
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Services
            modelBuilder.Entity<Service>().HasData(
                new Service
                {
                    Id = 1,
                    Title = "Développement Web",
                    Description = "Création d'applications web modernes et performantes avec les dernières technologies.",
                    LongDescription = "Nous développons des applications web sur mesure en utilisant les frameworks les plus récents comme React, Angular, Vue.js pour le frontend et .NET Core, Node.js pour le backend.",
                    Icon = "fas fa-code",
                    Category = "Développement",
                    IsActive = true,
                    SortOrder = 1
                },
                new Service
                {
                    Id = 2,
                    Title = "Applications Mobiles",
                    Description = "Développement d'applications mobiles natives et cross-platform.",
                    LongDescription = "Nos applications mobiles sont développées avec React Native et Flutter pour assurer une compatibilité maximale entre iOS et Android.",
                    Icon = "fas fa-mobile-alt",
                    Category = "Développement",
                    IsActive = true,
                    SortOrder = 2
                },
                new Service
                {
                    Id = 3,
                    Title = "Solutions Cloud",
                    Description = "Migration et optimisation de vos infrastructures cloud.",
                    LongDescription = "Nous vous accompagnons dans votre migration vers le cloud avec AWS, Azure ou Google Cloud Platform, en optimisant vos coûts et performances.",
                    Icon = "fas fa-cloud",
                    Category = "Infrastructure",
                    IsActive = true,
                    SortOrder = 3
                },
                new Service
                {
                    Id = 4,
                    Title = "Formation",
                    Description = "Formations professionnelles en technologies modernes.",
                    LongDescription = "Nos formations sont dispensées par des experts certifiés et couvrent tous les aspects du développement moderne.",
                    Icon = "fas fa-graduation-cap",
                    Category = "Formation",
                    IsActive = true,
                    SortOrder = 4
                },
                new Service
                {
                    Id = 5,
                    Title = "Cybersécurité",
                    Description = "Protection et sécurisation de vos systèmes et données.",
                    LongDescription = "Nous assurons la sécurité de vos systèmes avec des audits complets, des tests de pénétration et une surveillance 24/7.",
                    Icon = "fas fa-shield-alt",
                    Category = "Sécurité",
                    IsActive = true,
                    SortOrder = 5
                },
                new Service
                {
                    Id = 6,
                    Title = "Conseil IT",
                    Description = "Accompagnement stratégique pour votre transformation digitale.",
                    LongDescription = "Nos consultants vous accompagnent dans la définition de votre stratégie IT et la mise en place de solutions adaptées à vos besoins.",
                    Icon = "fas fa-chart-line",
                    Category = "Conseil",
                    IsActive = true,
                    SortOrder = 6
                }
            );

            // Seed Service Features
            modelBuilder.Entity<ServiceFeature>().HasData(
                new ServiceFeature { Id = 1, ServiceId = 1, Feature = "Applications React/Angular", SortOrder = 1 },
                new ServiceFeature { Id = 2, ServiceId = 1, Feature = "APIs REST/GraphQL", SortOrder = 2 },
                new ServiceFeature { Id = 3, ServiceId = 1, Feature = "Bases de données optimisées", SortOrder = 3 },
                new ServiceFeature { Id = 4, ServiceId = 2, Feature = "iOS & Android", SortOrder = 1 },
                new ServiceFeature { Id = 5, ServiceId = 2, Feature = "React Native", SortOrder = 2 },
                new ServiceFeature { Id = 6, ServiceId = 2, Feature = "Flutter", SortOrder = 3 },
                new ServiceFeature { Id = 7, ServiceId = 3, Feature = "AWS/Azure/GCP", SortOrder = 1 },
                new ServiceFeature { Id = 8, ServiceId = 3, Feature = "DevOps & CI/CD", SortOrder = 2 },
                new ServiceFeature { Id = 9, ServiceId = 3, Feature = "Microservices", SortOrder = 3 },
                new ServiceFeature { Id = 10, ServiceId = 4, Feature = "Certifications officielles", SortOrder = 1 },
                new ServiceFeature { Id = 11, ServiceId = 4, Feature = "Formation sur mesure", SortOrder = 2 },
                new ServiceFeature { Id = 12, ServiceId = 4, Feature = "Support continu", SortOrder = 3 },
                new ServiceFeature { Id = 13, ServiceId = 5, Feature = "Audits de sécurité", SortOrder = 1 },
                new ServiceFeature { Id = 14, ServiceId = 5, Feature = "Conformité RGPD", SortOrder = 2 },
                new ServiceFeature { Id = 15, ServiceId = 5, Feature = "Monitoring 24/7", SortOrder = 3 },
                new ServiceFeature { Id = 16, ServiceId = 6, Feature = "Architecture IT", SortOrder = 1 },
                new ServiceFeature { Id = 17, ServiceId = 6, Feature = "Digitalisation", SortOrder = 2 },
                new ServiceFeature { Id = 18, ServiceId = 6, Feature = "Optimisation", SortOrder = 3 }
            );
        }
    }
}
