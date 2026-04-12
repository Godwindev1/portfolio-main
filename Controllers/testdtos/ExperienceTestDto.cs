using Portfolio.Models;
using Portfolio.ViewModels;

public static class ExperienceReturnDto 
{
    public static Experience Get() 
    {
        return new Experience{
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
            };
    }
} 
