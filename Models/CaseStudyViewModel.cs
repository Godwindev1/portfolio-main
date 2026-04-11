using Microsoft.AspNetCore.Http;
using Portfolio.Models;

namespace Portfolio.ViewModels;

// ── Artifact Inputs ────────────────────────────────────────────────────────────

/// <summary>
/// For artifact types that are file uploads: Document, Videos, Screenshot.
/// The controller uploads the file to the bucket and writes the resulting URL
/// into an ArtifactLink row.
/// </summary>
public class UploadArtifactInput
{
    public int? Id { get; set; }                   // null = new, set = edit
    public string Label { get; set; } = string.Empty;
    public IFormFile? File { get; set; }           // the incoming upload
    public string? ExistingUrl { get; set; }       // retained on edit if no new file supplied
}

/// <summary>
/// For artifact types that are plain hyperlinks: Links, Repo, Live.
/// No file involved – the user just provides a label and a URL.
/// </summary>d
public class LinkArtifactInput
{
    public int? Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}

// ── Nested Entity Inputs ───────────────────────────────────────────────────────

public class ImplementationStepInput
{
    public int? Id { get; set; }
    public int Order { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class MetricInput
{
    public int? Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class SkillInput
{
    public int? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
}

public class ArchitectureComponentInput
{
    public int? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Tech { get; set; } = string.Empty;
}

// ── Root ViewModel ─────────────────────────────────────────────────────────────

public class SaveCaseStudyViewModel
{
    public int? Id { get; set; }   // null on Create

    // ── Scalar fields ──────────────────────────────────────────────────────────
    public string Category { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public bool IsFeatured { get; set; }
    public string Label { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;

    // ── Cover image ────────────────────────────────────────────────────────────
    // Controller uploads CoverImageFile → bucket → writes the URL to CaseStudy.CoverImageUrl.
    public IFormFile? CoverImageFile { get; set; }
    public string? ExistingCoverImageUrl { get; set; }   // kept on edit when no new file

    // ── JSON-backed sections ───────────────────────────────────────────────────
    public ProblemSection Problem { get; set; } = new();
    public SolutionSection Solution { get; set; } = new();

    // ── Relational entities ────────────────────────────────────────────────────
    public List<ImplementationStepInput> ImplementationSteps { get; set; } = [];
    public List<MetricInput> Metrics { get; set; } = [];
    public List<SkillInput> Skills { get; set; } = [];
    public List<ArchitectureComponentInput> ArchitectureComponents { get; set; } = [];

    // ── Upload-based artifacts (blob → bucket → URL) ───────────────────────────
    // ArtifactTypes.Document
    public List<UploadArtifactInput> Documents { get; set; } = [];

    // ArtifactTypes.Videos
    public List<UploadArtifactInput> Videos { get; set; } = [];

    // ArtifactTypes.ScreenShot
    public List<UploadArtifactInput> Screenshots { get; set; } = [];
    
    //ArtifactTypes.implementationDetails
    public List<UploadArtifactInput> implementationDetails { get; set; } = [];

    // ── Link-based artifacts (label + URL only, no upload) ─────────────────────
    // ArtifactTypes.Links
    public List<LinkArtifactInput> Links { get; set; } = [];

    // ArtifactTypes.Repo
    public List<LinkArtifactInput> Repos { get; set; } = [];

    // ArtifactTypes.Live
    public List<LinkArtifactInput> LiveDemos { get; set; } = [];


}