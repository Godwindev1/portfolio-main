using System.Text.Json;
using Portfolio.Models;
public class ExperienceSeedTest
{
    public async Task GenerateExperiences(IExperienceRepository repo)
    {
       var experiences = new List<Experience> {
            new() {
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
            }
       };


       Console.WriteLine("Generating Experience :");
        foreach (var exp in experiences)
        {
            Console.WriteLine($"- {exp.Role} at {exp.Company}");
            await repo.AddAsync(exp);
        }

        Console.WriteLine("Experiences generation completed. Check the database for results.");

        List<Experience>? ExperiencesReturn = await repo.GetAllAsync() ?? null;
        if(ExperiencesReturn != null)
        {
            Console.WriteLine($"Total Experiences in database: {ExperiencesReturn.Count}");
            foreach (var exp in ExperiencesReturn)
            {
            Console.WriteLine($"- {exp.Role} at {exp.Company}");
            }
        }
        else
        {
            Console.WriteLine("no Experiences found in database.");
        }

    }
}