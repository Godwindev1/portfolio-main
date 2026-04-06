using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers;

[Authorize(policy: "AdminOnly")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        var viewModel = new PortfolioViewModel
        {
            HeroStatus = new HeroStatus(),
            Projects = GetProjects(),
            CaseStudies = GetCaseStudies(),
            SkillDomains = GetSkillDomains(),
            Experiences = GetExperiences(),
            Testimonials = GetTestimonials(),
            Certifications = GetCertifications()
        };

        return View(viewModel);
    }

    private List<ProjectCard> GetProjects()
    {
        return new List<ProjectCard>
        {
            new() {
                Id = 1,
                Category = "SYSTEMS_ARCHITECTURE",
                Title = "FastJobServer",
                Description = "Custom .NET background job processing library with DateTime-triggered scheduling via PriorityQueue, DI thread safety, and expression-tree-based job enqueueing. Built to outlast off-the-shelf solutions.",
                Tags = new() { "C# .NET", "CONCURRENCY", "PRIORITY_QUEUE", "DI" }
            },
            new() {
                Id = 2,
                Category = "AI_INFRASTRUCTURE",
                Title = "RAG Exam Engine",
                Description = "AI-powered exam revision assistant built on a full RAG pipeline. ASP.NET Core backend with pgvector semantic search, Hangfire job runner, SignalR real-time delivery, and Claude API integration.",
                Tags = new() { "ASP.NET CORE", "POSTGRESQL", "PGVECTOR", "SIGNALR" }
            },
            new() {
                Id = 3,
                Category = "GRAPHICS_SYSTEMS",
                Title = "Toon Shader Viewer",
                Description = "C++/OpenGL cel-shading viewer featuring a nine-effect GLSL uber shader system — mutually exclusive base lighting, additive modifiers, and outline pass — with a ToonMaterial C++ class managing uniform state.",
                Tags = new() { "C++", "OPENGL", "GLSL", "IMGUI" }
            },
            new() {
                Id = 4,
                Category = "DEVOPS_PIPELINE",
                Title = "CI/CD Infrastructure",
                Description = "End-to-end DevOps pipeline architecture for multi-service deployments. Automated build, test, and release workflows with containerisation, environment parity, and rollback strategy built in.",
                Tags = new() { "DOCKER", "GITHUB_ACTIONS", "TERRAFORM", "K8S" }
            }
        };
    }

    private List<CaseStudy> GetCaseStudies()
    {
        return new List<CaseStudy>
        {
            new() {
                Id = 1,
                Label = "CASE_STUDY_01 // THE_BENCHMARK",
                Title = "FastJobServer: The Scheduling Engine Built From Scratch.",
                ProblemStatement = "When Hangfire's overhead was killing throughput and Quartz.NET was too rigid — what does it take to build a scheduling engine that fits the constraint exactly?",
                BodyText = "FastJobServer was born from a real constraint: existing libraries brought too much ceremony. The solution required building a PriorityQueue-backed scheduler with DateTime triggers, DI-safe worker threading, and expression tree support for EnqueueJob — without the baggage of a framework not designed for this shape of problem.",
                Metrics = new() {
                    { "SCHEDULING_ACCURACY", "<5ms ±0.3ms" },
                    { "MEMORY_OVERHEAD", "-62% vs Hangfire" },
                    { "CONCURRENT_JOBS", "10K+ sustained" },
                    { "THREAD_MODEL", "LongRunning" }
                },
                Tags = new() { "C# .NET", "BACKGROUND_JOBS", "THREADING" }
            }
        };
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
                Tags = new() { "C# .NET", "ASP.NET CORE", "POSTGRESQL", "DEVOPS" }
            },
            new() {
                Id = 2,
                Period = "2021 — 2023",
                Role = "Backend Developer",
                Company = "CONTRACT ROLES // VARIOUS",
                Description = "Designed and deployed high-throughput API layers for data-intensive applications. Specialized in PostgreSQL optimization, concurrent processing architecture, and deployment pipeline design for containerized services.",
                Tags = new() { "REST APIS", "DOCKER", "MYSQL", "CI/CD" }
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
