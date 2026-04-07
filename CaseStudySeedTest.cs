using System.Text.Json;
using Portfolio.Models;
public class CaseStudySeedTest
{
    public async Task GenerateCaseStudies(ICaseStudyRepository repo)
    {
        var caseStudies = new List<CaseStudy>
{
    new CaseStudy
    {
        CoverImageUrl = "/images/case-study-1.png",
        Category = "SYSTEMS_ARCHITECTURE",
        DisplayOrder = 1,
        IsFeatured = true,

        Label = "SCALABILITY",
        Title = "High-Throughput Order Processing System",
        Summary = "Designed a distributed backend system handling 50k+ daily transactions with low latency.",

        ProblemJson = JsonSerializer.Serialize(new ProblemSection
        {
            Context = "E-commerce backend experiencing performance degradation under load.",
            ProblemStatement = "Order processing latency exceeded 2 seconds during peak traffic.",
            Challenges = new List<string>
            {
                "Synchronous processing bottlenecks",
                "Database contention under concurrency",
                "Lack of caching layer"
            }
        }),

        SolutionJson = JsonSerializer.Serialize(new SolutionSection
        {
            Overview = "Introduced asynchronous processing, caching, and queue-based workload distribution.",
            KeyDecisions = new List<string>
            {
                "Adopted message queue for decoupling",
                "Implemented Redis caching",
                "Split read/write workloads"
            },
            ArchitectureSummary = "Microservice-based system with API gateway, worker services, and caching layer."
        }),

        ImplementationSteps = new List<ImplementationStep>
        {
            new ImplementationStep { Order = 1, Title = "Queue Integration", Description = "Integrated RabbitMQ for async order processing." },
            new ImplementationStep { Order = 2, Title = "Caching Layer", Description = "Added Redis to cache product and pricing data." },
            new ImplementationStep { Order = 3, Title = "DB Optimization", Description = "Optimized queries and added indexing." }
        },

        Metrics = new List<Metric>
        {
            new Metric { Label = "Latency Reduction", Value = "2s → 120ms", Description = "Improved response time under load" },
            new Metric { Label = "Throughput", Value = "50k+/day", Description = "Handled increased transaction volume" },
            new Metric { Label = "Error Rate", Value = "-85%", Description = "Reduced failed transactions" }
        },

        Skills = new List<CaseStudySkill>
        {
            new CaseStudySkill { Name = "ASP.NET Core", Category = "Backend" },
            new CaseStudySkill { Name = "Redis", Category = "Caching" },
            new CaseStudySkill { Name = "RabbitMQ", Category = "Messaging" },
            new CaseStudySkill { Name = "MySQL", Category = "Database" }
        },

        Artifacts = new List<ArtifactLink>
        {
            new ArtifactLink { Label = "GitHub Repo", Url = "https://github.com/example/order-system", Type = "Repo" },
            new ArtifactLink { Label = "API Docs", Url = "https://postman.com/example", Type = "Docs" }
        },

        ArchitectureComponents = new List<ArchitectureComponent>
        {
            new ArchitectureComponent { Name = "API Gateway", Role = "Handles incoming HTTP requests", Tech = "ASP.NET Core" },
            new ArchitectureComponent { Name = "Worker Service", Role = "Processes background jobs", Tech = "Hangfire" },
            new ArchitectureComponent { Name = "Cache Layer", Role = "Reduces DB load", Tech = "Redis" },
            new ArchitectureComponent { Name = "Message Broker", Role = "Queues tasks", Tech = "RabbitMQ" },
            new ArchitectureComponent { Name = "Database", Role = "Persistent storage", Tech = "MySQL" }
        }
    },

    new CaseStudy
    {
        CoverImageUrl = "/images/case-study-2.png",
        Category = "PERFORMANCE",
        DisplayOrder = 2,
        IsFeatured = true,

        Label = "OPTIMIZATION",
        Title = "Low-Latency Analytics API",
        Summary = "Built a high-performance analytics API with sub-50ms response time.",

        ProblemJson = JsonSerializer.Serialize(new ProblemSection
        {
            Context = "Analytics dashboard required real-time data aggregation.",
            ProblemStatement = "API response time exceeded acceptable thresholds.",
            Challenges = new List<string>
            {
                "Heavy aggregation queries",
                "Large dataset processing",
                "Repeated redundant queries"
            }
        }),

        SolutionJson = JsonSerializer.Serialize(new SolutionSection
        {
            Overview = "Implemented caching, pre-aggregation, and query optimization.",
            KeyDecisions = new List<string>
            {
                "Used Redis for query caching",
                "Precomputed aggregates",
                "Reduced DB round-trips"
            },
            ArchitectureSummary = "Layered architecture with caching and optimized query pipeline."
        }),

        ImplementationSteps = new List<ImplementationStep>
        {
            new ImplementationStep { Order = 1, Title = "Query Refactor", Description = "Reduced joins and optimized indexes." },
            new ImplementationStep { Order = 2, Title = "Caching Strategy", Description = "Cached frequent queries using Redis." },
            new ImplementationStep { Order = 3, Title = "Aggregation Layer", Description = "Precomputed heavy analytics." }
        },

        Metrics = new List<Metric>
        {
            new Metric { Label = "Latency", Value = "300ms → 45ms", Description = "Improved API response time" },
            new Metric { Label = "DB Load", Value = "-60%", Description = "Reduced database pressure" }
        },

        Skills = new List<CaseStudySkill>
        {
            new CaseStudySkill { Name = "ASP.NET Core", Category = "Backend" },
            new CaseStudySkill { Name = "Redis", Category = "Caching" },
            new CaseStudySkill { Name = "SQL Optimization", Category = "Database" }
        },

        Artifacts = new List<ArtifactLink>
        {
            new ArtifactLink { Label = "Live Demo", Url = "https://demo.example.com", Type = "Demo" }
        },

        ArchitectureComponents = new List<ArchitectureComponent>
        {
            new ArchitectureComponent { Name = "API Layer", Role = "Handles requests", Tech = "ASP.NET Core" },
            new ArchitectureComponent { Name = "Cache Layer", Role = "Stores computed results", Tech = "Redis" },
            new ArchitectureComponent { Name = "Database", Role = "Stores raw data", Tech = "MySQL" }
        }
    }
};
        Console.WriteLine("Generating case studies:");
        foreach (var cs in caseStudies)
        {
            Console.WriteLine($"- {cs.Title}");
            await repo.AddAsync(cs);
        }

        Console.WriteLine("Case studies generation completed. Check the database for results.");

        List<CaseStudy>? CaseStudies = await repo.GetAllAsync() ?? null;
        if(CaseStudies != null)
        {
            Console.WriteLine($"Total case studies in database: {CaseStudies.Count}");
            foreach (var cs in CaseStudies)
            {
                Console.WriteLine($"- {cs.Title} (ID: {cs.Id})");
            }
        }
        else
        {
            Console.WriteLine("No case studies found in the database.");
        }


    }
}