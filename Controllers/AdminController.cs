

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.ViewModels;


public class AdminController : Controller
{
    private readonly BucketService _bucketService;
    private readonly CaseStudyModel _caseStudyModel;
    //private readonly CaseStudyMapperHelpers mapperHelpers;

    public AdminController(BucketService bucketService, ICaseStudyRepository caseStudyRepository)
    {
        _bucketService = bucketService;
        _caseStudyModel = new CaseStudyModel(caseStudyRepository);
    }

    [HttpGet("admin/CaseStudy")]
    public ViewResult CaseStudy()
    {
        Console.WriteLine("Reached AdminController.CaseStudy");
        var TestModel = new SaveCaseStudyViewModel
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

        return View("Views/Admin/CaseStudyAdd.cshtml", TestModel);
    }

    [HttpPost("admin/CaseStudy/Save", Name = "SaveCaseStudy")]
    public  async  Task<IActionResult> CaseStudy(SaveCaseStudyViewModel SaveCaseStudy)
    {
        Console.WriteLine("Received CaseStudy data:");
        Console.WriteLine($"Title: {SaveCaseStudy.Title}");
        Console.WriteLine($"Summary: {SaveCaseStudy.Summary}");
        Console.WriteLine($"Category: {SaveCaseStudy.Category}");

        string CoverImageUrl = "";
        if(SaveCaseStudy.CoverImageFile != null)
        {
            CoverImageUrl = await  _bucketService.UploadScreenShot(
                SaveCaseStudy.CoverImageFile.OpenReadStream(),
                SaveCaseStudy.CoverImageFile.FileName,
                SaveCaseStudy.CoverImageFile.ContentType
            );
        }

        var Isteps = SaveCaseStudy.ImplementationSteps.Select((ImplementationStepInput input) => {
            return new ImplementationStep { Order = input.Order, Title = input.Title, Description = input.Description ?? "" };
        });

        var ArchComps = SaveCaseStudy.ArchitectureComponents.Select((ArchitectureComponentInput input) => {
            return new ArchitectureComponent { Name = input.Name, Role = input.Role, Tech = input.Tech   };
        });

        var MetricsList = SaveCaseStudy.Metrics.Select((MetricInput input) => {
            return new Metric { Value = input.Value, Description = input.Description ?? "", Label = input.Label };
        });

        var Skills = SaveCaseStudy.Skills.Select((SkillInput input) => { 
            return new CaseStudySkill { Name = input.Name, Category = input.Category };
        });

        List<ArtifactLink> artifacts = new List<ArtifactLink>();

        //Map LINKS 
        foreach(LinkArtifactInput link in SaveCaseStudy.Links)
        {
            if(string.IsNullOrWhiteSpace(link.Url)) continue;

            artifacts.Add(new ArtifactLink {
                Label = link.Label,
                Url = link.Url,
                Type = ArtifactTypes.Links
            });
        }

        //MAP IMPLEMENTATION DETAILS
        foreach(UploadArtifactInput implDetail in SaveCaseStudy.implementationDetails)
        {
            
            if(implDetail.File == null) 
            {   
                artifacts.Add(new ArtifactLink {
                    Label = implDetail.Label,
                    Url = implDetail.ExistingUrl ?? "",
                    Type = ArtifactTypes.ImplementationDetail
                });
                continue;
            }

            var ResolvedUrl = await  _bucketService.UploadFile(
                implDetail.File.OpenReadStream(),
                implDetail.File.FileName,
                implDetail.File.ContentType,
                UploadType.File
            );

           artifacts.Add(new ArtifactLink {
                Label = implDetail.Label,
                Url = ResolvedUrl,
                Type = ArtifactTypes.ImplementationDetail
            });
        }

        //MAP VIDEOS
        foreach(UploadArtifactInput video in SaveCaseStudy.Videos)
        {
            if(video.File == null) 
            {   
                artifacts.Add(new ArtifactLink {
                    Label = video.Label,
                    Url = video.ExistingUrl ?? "",
                    Type = ArtifactTypes.ScreenShot
                });
                continue;
            }


            var ResolvedUrl = await  _bucketService.UploadVideo(
                video.File.OpenReadStream(),
                video.File.FileName,
                video.File.ContentType
            );

           artifacts.Add(new ArtifactLink {
                Label = video.Label,
                Url = ResolvedUrl,
                Type = ArtifactTypes.Videos
            });
        }

        //MAP SCREENSHOTS
        foreach(UploadArtifactInput screenshot in SaveCaseStudy.Screenshots)
        {
            if(screenshot.File == null) 
            {   
                artifacts.Add(new ArtifactLink {
                    Label = screenshot.Label,
                    Url = screenshot.ExistingUrl ?? "",
                    Type = ArtifactTypes.ScreenShot
                });
                continue;
            }

            var ResolvedUrl = await  _bucketService.UploadScreenShot(
                screenshot.File.OpenReadStream(),
                screenshot.File.FileName,
                screenshot.File.ContentType
            );

           artifacts.Add(new ArtifactLink {
                Label = screenshot.Label,
                Url = ResolvedUrl,
                Type = ArtifactTypes.ScreenShot
            });
        }

        //MAP DOCUMENTS
        foreach(UploadArtifactInput doc in SaveCaseStudy.Documents)
        {
            if(doc.File == null) continue;

            var ResolvedUrl = await  _bucketService.UploadFile(
                doc.File.OpenReadStream(),
                doc.File.FileName,
                doc.File.ContentType
            );

           artifacts.Add(new ArtifactLink {
                Label = doc.Label,
                Url = ResolvedUrl,
                Type = ArtifactTypes.Document
            });
        }

        CaseStudy CaseStudyDbStore = new CaseStudy {
            Title = SaveCaseStudy.Title,
            Summary = SaveCaseStudy.Summary,
            Category = SaveCaseStudy.Category,
            ProblemJson = System.Text.Json.JsonSerializer.Serialize(SaveCaseStudy.Problem),
            SolutionJson = System.Text.Json.JsonSerializer.Serialize(SaveCaseStudy.Solution),
            IsFeatured = SaveCaseStudy.IsFeatured,
            Label = SaveCaseStudy.Label,
            DisplayOrder = SaveCaseStudy.DisplayOrder,
            CoverImageUrl = CoverImageUrl,
            ImplementationSteps = Isteps.ToList(),
            ArchitectureComponents = ArchComps.ToList(),
            Metrics = MetricsList.ToList(),
            Skills = Skills.ToList(),
            Artifacts = artifacts
         };
        

        await _caseStudyModel.SaveCaseStudyAsync(CaseStudyDbStore);
    
        return Ok();
    }
}