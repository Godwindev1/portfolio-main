namespace Portfolio.Models;



public class SkillDomain
{
    public int Id { get; set; }
    public int Number { get; set; }
    public string Icon { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public List<SkillItem> Items { get; set; } = [];
}

public class SkillItem
{
    public string Name { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
}

public class Experience
{
    public int Id { get; set; }
    public string Period { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = [];
}

public class Testimonial
{
    public int Id { get; set; }
    public string Quote { get; set; } = string.Empty;
    public string AuthorInitials { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorTitle { get; set; } = string.Empty;
}

public class Certification
{
    public int Id { get; set; }
    public string Icon { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Grade { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public string BadgeUrl { get; set; } = string.Empty;
}

public class HeroStatus
{
    public bool OpenToWork { get; set; } = true;
    public string StatusMessage { get; set; } = "Focused on distributed systems, background job processing, and cloud-native DevOps.";
    public Dictionary<string, string> Metrics { get; set; } = new()
    {
        { "LATENCY_TARGET", "<10ms" },
        { "UPTIME_SLA", "99.9%" },
        { "EXP_YEARS", "4+" },
        { "VERSION", "v2.4.0" }
    };
}

public class PortfolioViewModel
{
    public HeroStatus HeroStatus { get; set; } = new();
    public List<CaseStudyViewModel> CaseStudies { get; set; } = [];
    public List<SkillDomain> SkillDomains { get; set; } = [];
    public List<Experience> Experiences { get; set; } = [];
    public List<Testimonial> Testimonials { get; set; } = [];
    public List<Certification> Certifications { get; set; } = [];
}
