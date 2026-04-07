using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Portfolio.Models;

namespace Portfolio.Data;

public class PortfolioDbContext : DbContext
{
    public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
        : base(options)
    {
    }

    public DbSet<ProjectCard> Projects => Set<ProjectCard>();
    public DbSet<CaseStudy> CaseStudies => Set<CaseStudy>();
    public DbSet<SkillDomain> SkillDomains => Set<SkillDomain>();
    public DbSet<Experience> Experiences => Set<Experience>();
    public DbSet<Testimonial> Testimonials => Set<Testimonial>();
    public DbSet<Certification> Certifications => Set<Certification>();

    // NEW DbSets (optional but recommended for querying)
    public DbSet<ImplementationStep> ImplementationSteps => Set<ImplementationStep>();
    public DbSet<Metric> Metrics => Set<Metric>();
    public DbSet<CaseStudySkill> CaseStudySkills => Set<CaseStudySkill>();
    public DbSet<ArtifactLink> ArtifactLinks => Set<ArtifactLink>();
    public DbSet<ArchitectureComponent> ArchitectureComponents => Set<ArchitectureComponent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // -------------------------
        // JSON Conversions (KEEP ONLY WHAT YOU NEED)
        // -------------------------

        ConfigureJsonConversion<ProjectCard, List<string>>(modelBuilder, e => e.Tags);
        ConfigureJsonConversion<Experience, List<string>>(modelBuilder, e => e.Tags);

        // CaseStudy JSON sections
        modelBuilder.Entity<CaseStudy>()
            .Property(x => x.ProblemJson)
            .HasColumnType("json");

        modelBuilder.Entity<CaseStudy>()
            .Property(x => x.SolutionJson)
            .HasColumnType("json");

        // -------------------------
        // CaseStudy Relationships
        // -------------------------

        modelBuilder.Entity<CaseStudy>()
            .HasMany(c => c.ImplementationSteps)
            .WithOne(i => i.CaseStudy)
            .HasForeignKey(i => i.CaseStudyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CaseStudy>()
            .HasMany(c => c.Metrics)
            .WithOne(m => m.CaseStudy)
            .HasForeignKey(m => m.CaseStudyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CaseStudy>()
            .HasMany(c => c.Skills)
            .WithOne(s => s.CaseStudy)
            .HasForeignKey(s => s.CaseStudyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CaseStudy>()
            .HasMany(c => c.Artifacts)
            .WithOne(a => a.CaseStudy)
            .HasForeignKey(a => a.CaseStudyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CaseStudy>()
            .HasMany(c => c.ArchitectureComponents)
            .WithOne(a => a.CaseStudy)
            .HasForeignKey(a => a.CaseStudyId)
            .OnDelete(DeleteBehavior.Cascade);

        // -------------------------
        // Ordering Index (important for UI)
        // -------------------------

        modelBuilder.Entity<CaseStudy>()
            .HasIndex(c => c.DisplayOrder);

        modelBuilder.Entity<ImplementationStep>()
            .HasIndex(i => new { i.CaseStudyId, i.Order });

        // -------------------------
        // Owned Collection (unchanged)
        // -------------------------

        modelBuilder.Entity<SkillDomain>()
            .OwnsMany(s => s.Items, a =>
            {
                a.WithOwner().HasForeignKey("SkillDomainId");
                a.Property<int>("Id");
                a.HasKey("Id");
            });
    }

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private static void ConfigureJsonConversion<TEntity, TProperty>(
        ModelBuilder modelBuilder,
        System.Linq.Expressions.Expression<Func<TEntity, TProperty>> propertyExpression)
        where TEntity : class
    {
        modelBuilder.Entity<TEntity>()
            .Property(propertyExpression)
            .HasConversion(
                v => JsonSerializer.Serialize(v, JsonOptions),
                v => JsonSerializer.Deserialize<TProperty>(v, JsonOptions)!
            );
    }
}