namespace Portfolio.Models;




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
