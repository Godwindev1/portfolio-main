using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers;

public class HomeController : Controller
{

    private readonly CaseStudyModel Model; 

    public HomeController(ICaseStudyRepository repository)
    {
        Model = new CaseStudyModel(repository);
    }
    public async Task<IActionResult> Index()
    {
        var viewModel = new PortfolioViewModel
        {
            HeroStatus = new HeroStatus(),
            CaseStudies = await GetCaseStudies(),
            SkillDomains = GetSkillDomains(),
            Experiences = GetExperiences(),
            Testimonials = GetTestimonials(),
            Certifications = GetCertifications()
        };

        return View(viewModel);
    }

    private async  Task<List<CaseStudyViewModel>> GetCaseStudies()
    {
        return await Model.GetAllCaseStudiesAsync();
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

    private List<Experience> GetExperiences()
    {
        return new List<Experience>
        {
            new() {
                Id = 1,
                Period = "2023 — PRESENT",
                Role = "Senior Backend Engineer",
                Company = "FREELANCE // REMOTE",
                Description = "Building production backend systems across C++/OpenGL graphics, .NET backend infrastructure, and AI-powered web applications. Delivered a structured 3D graphics build with weekly milestones, a custom job scheduling library, and a RAG pipeline for an AI exam assistant.",
                Tags = new() { "C# .NET", "ASP.NET CORE", "POSTGRESQL", "DEVOPS" },
                Responsibilities = new() {
                    "Designed and implemented a custom job scheduling library in C# to manage complex task dependencies and optimize resource utilization, resulting in a 30% improvement in processing efficiency.",
                    "Developed a RAG (Retrieval-Augmented Generation) pipeline for an AI exam assistant, integrating vector search with PostgreSQL to enable real-time retrieval of relevant information, enhancing response accuracy by 25%.",
                    "Led the backend development of an AI-powered web application, utilizing ASP.NET Core to create scalable APIs and PostgreSQL for data management, achieving seamless integration with frontend components and third-party services."
                }
            },
            new() {
                Id = 2,
                Period = "2021 — 2023",
                Role = "Backend Developer",
                Company = "CONTRACT ROLES // VARIOUS",
                Description = "Designed and deployed high-throughput API layers for data-intensive applications. Specialized in PostgreSQL optimization, concurrent processing architecture, and deployment pipeline design for containerized services.",
                Tags = new() { "REST APIS", "DOCKER", "MYSQL", "CI/CD" },
                Responsibilities = new() {
                    "Architected and implemented high-throughput REST APIs for data-intensive applications, utilizing ASP.NET Core to ensure scalability and maintainability while adhering to best practices in API design.",
                    "Optimized PostgreSQL databases through indexing strategies, query optimization, and schema design improvements, resulting in a 40% reduction in query response times for critical endpoints.",
                    "Designed and implemented deployment pipelines for containerized services using Docker and CI/CD tools, enabling seamless integration and continuous delivery across multiple environments."
                }
            },

            new() {
                Id = 3,
                Period = "2019 — 2021",
                Role = "DevOps Engineer",
                Company = "PRIOR_ROLE // INFRASTRUCTURE",
                Description = "Implemented automated CI/CD pipelines, container orchestration, and infrastructure-as-code workflows. Built real-time monitoring and alerting systems using Prometheus and Grafana for production environments.",
                Tags = new() { "TERRAFORM", "KUBERNETES", "PROMETHEUS", "GITHUB_ACTIONS" }
            }
        };
    }

    private List<Testimonial> GetTestimonials()
    {
        return new List<Testimonial>
        {
            new() {
                Id = 1,
                Quote = "His ability to design systems that don't just work today but hold up under load tomorrow is rare. The job scheduler alone saved us weeks of integration pain.",
                AuthorInitials = "MK",
                AuthorName = "CLIENT // BACKEND_PROJECT",
                AuthorTitle = "VIA FIVERR // VERIFIED"
            },
            new() {
                Id = 2,
                Quote = "Documentation as disciplined as the code itself. Every architectural decision was explained. We didn't just get a system — we got a foundation we understand.",
                AuthorInitials = "AO",
                AuthorName = "CLIENT // RAG_PIPELINE",
                AuthorTitle = "ASP.NET CORE PROJECT // VERIFIED"
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
