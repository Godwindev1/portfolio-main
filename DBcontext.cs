using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // JSON Conversions
        ConfigureJsonConversion<ProjectCard, List<string>>(modelBuilder, e => e.Tags);
        ConfigureJsonConversion<Experience, List<string>>(modelBuilder, e => e.Tags);
        ConfigureJsonConversion<CaseStudy, List<string>>(modelBuilder, e => e.Tags);

        ConfigureJsonConversion<CaseStudy, Dictionary<string, string>>(modelBuilder, e => e.Metrics);
        ConfigureJsonConversion<HeroStatus, Dictionary<string, string>>(modelBuilder, e => e.Metrics);

        // Owned Collection
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