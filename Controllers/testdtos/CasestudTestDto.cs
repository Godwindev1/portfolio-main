using Portfolio.Models;
using Portfolio.ViewModels;

public static class SaveCaseStudyReturnDto 
{
    public static SaveCaseStudyViewModel Get() 
    {
        return new SaveCaseStudyViewModel
        {
            Id                   = null,
            Category             = "SYSTEMS_ARCHITECTURE",
            DisplayOrder         = 1,
            IsFeatured           = true,
            Label                = "CASE_STUDY_01 // THE_BENCHMARK",
            Title                = "FastJobServer: The Scheduling Engine Built From Scratch.",
            Summary              = "FastJobServer was born from a real constraint: existing libraries brought too much ceremony. The solution required building a PriorityQueue-backed scheduler with DateTime triggers, DI-safe worker threading, and expression tree support for EnqueueJob — without the baggage of a framework not designed for this shape of problem.",
            ExistingCoverImageUrl = "/images/Test.webp",

            Problem = new ProblemSection
            {
                Context          = "E-commerce backend experiencing performance degradation under load.",
                ProblemStatement = "When Hangfire's overhead was killing throughput and Quartz.NET was too rigid — what does it take to build a scheduling engine that fits the constraint exactly?",
                Challenges       =
                [
                    "Synchronous processing bottlenecks",
                    "Database contention under concurrency",
                    "Lack of caching layer"
                ]
            },

            Solution = new SolutionSection
            {
                Overview             = "Introduced asynchronous processing, caching, and queue-based workload distribution.",
                ArchitectureSummary  = "Microservice-based system with API gateway, worker services, and caching layer.",
                KeyDecisions         =
                [
                    "Adopted message queue for decoupling",
                    "Implemented Redis caching",
                    "Split read/write workloads"
                ]
            },

            ImplementationSteps =
            [
                new() { Id = null, Order = 1, Title = "Queue Integration",  Description = "Integrated RabbitMQ for async order processing." },
                new() { Id = null, Order = 2, Title = "Caching Layer",      Description = "Added Redis to cache product and pricing data."  },
                new() { Id = null, Order = 3, Title = "DB Optimization",    Description = "Optimized queries and added indexing."           }
            ],

            Metrics =
            [
                new() { Id = null, Label = "Latency Reduction", Value = "2s → 120ms",    Description = "Improved response time under load"    },
                new() { Id = null, Label = "Throughput",        Value = "50k+ /day",     Description = "Handled increased transaction volume"  },
                new() { Id = null, Label = "Error Rate",        Value = "-85%",          Description = "Reduced failed transactions"           },
                new() { Id = null, Label = "CONCURRENT_JOBS",  Value = "10K+ sustained", Description = " Empty String"                                     }
            ],

            Skills =
            [
                new() { Id = null, Name = "ASP.NET Core", Category = "Backend"   },
                new() { Id = null, Name = "Redis",        Category = "Caching"   },
                new() { Id = null, Name = "RabbitMQ",     Category = "Messaging" },
                new() { Id = null, Name = "MySQL",        Category = "Database"  }
            ],

            ArchitectureComponents =
            [
                new() { Id = null, Name = "API Gateway",    Role = "Handles incoming HTTP requests", Tech = "ASP.NET Core" },
                new() { Id = null, Name = "Worker Service", Role = "Processes background jobs",       Tech = "Hangfire"     },
                new() { Id = null, Name = "Cache Layer",    Role = "Reduces DB load",                Tech = "Redis"        },
                new() { Id = null, Name = "Message Broker", Role = "Queues tasks",                  Tech = "RabbitMQ"     },
                new() { Id = null, Name = "Database",       Role = "Persistent storage",             Tech = "MySQL"        }
            ],

            // ── Upload artifacts — seed with ExistingUrl so the editor shows previews ──

            // ArtifactTypes.ImplementationDetail  →  implementationDetails
            implementationDetails =
            [
                new() { Id = null, Label = "ScreenShot1", ExistingUrl = "/Images/CaseStudyImage.png" }
            ],

            // ArtifactTypes.ScreenShot  →  Screenshots
            Screenshots =
            [
                new() { Id = null, Label = "ScreenShot2", ExistingUrl = "/Images/Test2.webp" },
                new() { Id = null, Label = "ScreenShot2", ExistingUrl = "/Images/Test2.webp" },
                new() { Id = null, Label = "ScreenShot2", ExistingUrl = "/Images/Test2.webp" },
                new() { Id = null, Label = "ScreenShot2", ExistingUrl = "/Images/Test2.webp" }
            ],

            // ── Link artifacts ─────────────────────────────────────────────────────────

            // ArtifactTypes.Repo  →  Repos
            Repos =
            [
                new() { Id = null, Label = "GitHub Repo", Url = "https://github.com/example/order-system" }
            ],

            // ArtifactTypes.Links  →  Links
            Links =
            [
                new() { Id = null, Label = "API Docs", Url = "https://postman.com/example" }
            ],

            // No LiveDemos in the seed data
            LiveDemos    = [],
            Documents    = [],
            Videos       = []
        };
    }
}