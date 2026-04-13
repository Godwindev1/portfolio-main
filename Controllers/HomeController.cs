using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers;

public class HomeController : Controller
{

    private readonly CaseStudyModel Model; 
    private readonly IExperienceRepository _experienceRepo;

    private readonly ITestimonialRepository _testimonialRepo;


    public HomeController(ICaseStudyRepository repository, IExperienceRepository experienceRepository, ITestimonialRepository testimonialRepository)
    {
        Model = new CaseStudyModel(repository);
        _experienceRepo = experienceRepository;
        _testimonialRepo = testimonialRepository;
    }
    public async Task<IActionResult> Index()
    {
        var viewModel = new PortfolioViewModel
        {
            HeroStatus = new HeroStatus(),
            CaseStudies = await GetCaseStudies(),
            SkillDomains = GetSkillDomains(),
            Experiences = await GetWorkHistory(),
            Testimonials = await GetTestimonials(),
            Certifications = GetCertifications()
        };

        return View(viewModel);
    }

    private async  Task<List<CaseStudyViewModel>> GetCaseStudies()
    {
        return await Model.GetAllCaseStudiesAsync();
    }

    private async Task<List<Experience>> GetWorkHistory()
    {
        return await _experienceRepo.GetAllAsync();
    }

        private async Task<List<Testimonial>> GetTestimonials()
    {
        return await _testimonialRepo.GetAllAsync();
    }



    private List<SkillDomain> GetSkillDomains()
    {
        return new List<SkillDomain>
        {
            new() {
                Id = 1,
                Number = 1,
                Icon = "⚙",
                Title = "Concurrency",
                Items = new() {
                    new() { Name = "Task / async-await", Level = "CORE" },
                    new() { Name = "SemaphoreSlim / Interlocked", Level = "CORE" },
                    new() { Name = "Thread Pooling", Level = "ADVANCED" },
                    new() { Name = "Expression Trees", Level = "ADVANCED" }
                }
            },
            new() {
                Id = 2,
                Number = 2,
                Icon = "⏱",
                Title = "Job Processing",
                Items = new() {
                    new() { Name = "Custom PriorityQueue Sched.", Level = "CORE" },
                    new() { Name = "DateTime-Triggered Jobs", Level = "CORE" },
                    new() { Name = "Hangfire / Quartz.NET", Level = "ADVANCED" },
                    new() { Name = "Event-Driven Architecture", Level = "ADVANCED" }
                }
            },
            new() {
                Id = 3,
                Number = 3,
                Icon = "🗄",
                Title = "Persistence",
                Items = new() {
                    new() { Name = "PostgreSQL + pgvector", Level = "CORE" },
                    new() { Name = "MySQL / Entity Framework", Level = "CORE" },
                    new() { Name = "Redis Caching", Level = "ADVANCED" },
                    new() { Name = "Vector Search (RAG)", Level = "ADVANCED" }
                }
            },
            new() {
                Id = 4,
                Number = 4,
                Icon = "☁",
                Title = "Cloud & Infrastructure",
                Items = new() {
                    new() { Name = "Docker / Kubernetes", Level = "CORE" },
                    new() { Name = "GitHub Actions CI/CD", Level = "CORE" },
                    new() { Name = "Terraform / IaC", Level = "PRO" },
                    new() { Name = "Nginx / Reverse Proxy", Level = "ADVANCED" }
                }
            },
            new() {
                Id = 5,
                Number = 5,
                Icon = "🔒",
                Title = "Auth & Security",
                Items = new() {
                    new() { Name = "JWT / OAuth2", Level = "CORE" },
                    new() { Name = "ASP.NET Identity", Level = "CORE" },
                    new() { Name = "RBAC Systems", Level = "ADVANCED" },
                    new() { Name = "API Rate Limiting", Level = "ADVANCED" }
                }
            },
            new() {
                Id = 6,
                Number = 6,
                Icon = "📊",
                Title = "Monitoring",
                Items = new() {
                    new() { Name = "Structured Logging", Level = "CORE" },
                    new() { Name = "Prometheus / Grafana", Level = "PRO" },
                    new() { Name = "Distributed Tracing", Level = "ADVANCED" },
                    new() { Name = "Health Checks", Level = "CORE" }
                }
            }
        };
    }


    private List<Certification> GetCertifications()
    {
        return new List<Certification>
        {
            new() {
                Id = 1,
                Icon = "☁",
                Name = "AWS Certified Solutions Architect",
                Grade = "ASSOCIATE // PROFESSIONAL GRADE",
                Year = "2023",
                Provider = "Amazon Web Services",
                BadgeUrl = "#"
            },
            new() {
                Id = 2,
                Icon = "⎈",
                Name = "CKAD: Certified Kubernetes Application Developer",
                Grade = "LINUX FOUNDATION // CLOUD NATIVE",
                Year = "2022",
                Provider = "CNCF",
                BadgeUrl = "#"
            },
            new() {
                Id = 3,
                Icon = "◈",
                Name = "HashiCorp Certified: Terraform Associate",
                Grade = "INFRASTRUCTURE AUTOMATION // MULTI-CLOUD",
                Year = "2022",
                Provider = "HashiCorp",
                BadgeUrl = "#"
            },
            new() {
                Id = 4,
                Icon = "◎",
                Name = "GitHub Actions: CI/CD Specialist",
                Grade = "AUTOMATION // PIPELINE ENGINEERING",
                Year = "2023",
                Provider = "GitHub",
                BadgeUrl = "#"
            }
        };
    }
}
