
namespace Portfolio.Models;
public class CaseStudy
{
    public int Id { get; set; }

    public string CoverImageUrl { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; // "SYSTEMS_ARCHITECTURE"
    public int DisplayOrder { get; set; }
    public bool IsFeatured { get; set; }

    public string Label { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;

    // JSON-backed sections (store as string in DB)
    public string ProblemJson { get; set; } = string.Empty;
    public string SolutionJson { get; set; } = string.Empty;

    // Structured relational data
    public List<ImplementationStep> ImplementationSteps { get; set; } = [];
    public List<Metric> Metrics { get; set; } = [];
    public List<CaseStudySkill> Skills { get; set; } = [];

    // Optional artifacts (datasets, repos, demos)
    public List<ArtifactLink> Artifacts { get; set; } = [];

    // Optional architecture breakdown
    public List<ArchitectureComponent> ArchitectureComponents { get; set; } = [];
}

public class ProblemSection
{
    public string Context { get; set; } = string.Empty;
    public string ProblemStatement { get; set; } = string.Empty;
    public List<string> Challenges { get; set; } = [];
}

public class SolutionSection
{
    public string Overview { get; set; } = string.Empty;
    public List<string> KeyDecisions { get; set; } = [];
    public string ArchitectureSummary { get; set; } = string.Empty;
}

public class CaseStudySkill
{
    public int Id { get; set; }
    public int CaseStudyId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; // Backend, DevOps, DB

    public CaseStudy CaseStudy { get; set; }
}
public class ImplementationStep
{
    public int Id { get; set; }
    public int CaseStudyId { get; set; }

    public int Order { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public CaseStudy CaseStudy { get; set; }
}

public class Metric
{
    public int Id { get; set; }
    public int CaseStudyId { get; set; }

    public string Label { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public CaseStudy CaseStudy { get; set; }
}

public class ArtifactLink
{
    public int Id { get; set; }
    public int CaseStudyId { get; set; }

    public string Label { get; set; } = string.Empty; // "Dataset", "GitHub", "API Docs"
    public string Url { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;  // Dataset | Repo | Demo | Docs

    public CaseStudy CaseStudy { get; set; }
}

public class ArchitectureComponent
{
    public int Id { get; set; }
    public int CaseStudyId { get; set; }

    public string Name { get; set; } = string.Empty;   // "API Gateway"
    public string Role { get; set; } = string.Empty;   // what it does
    public string Tech { get; set; } = string.Empty;   // "ASP.NET Core", "Redis"

    public CaseStudy CaseStudy { get; set; }
}