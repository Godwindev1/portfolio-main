# Portfolio Web Application & Management Portal

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0-512BD4?logo=dotnet&logoColor=white)](https://docs.microsoft.com/en-us/ef/core/)
[![Docker](https://img.shields.io/badge/Docker-Containerized-2496ED?logo=docker&logoColor=white)](https://www.docker.com/)
[![Nginx](https://img.shields.io/badge/Nginx-Reverse%20Proxy-009639?logo=nginx&logoColor=white)](https://nginx.org/)
[![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

A full-stack, enterprise-grade personal portfolio and content management system designed to showcase software engineering case studies, manage high-performance media streaming, and serve custom API endpoints. 

Built with **ASP.NET Core**, **Entity Framework Core**, **MariaDB/MySQL**, and S3-compatible cloud object storage (**Cloudflare R2 / MinIO**), fully containerized with **Docker** and deployed via **GitHub Actions CI/CD**.

---

## 🌟 Key Features

* **Dynamic Case Study Management**: Fully structured database schema for managing software project case studies, complete with tech stack tagging, dynamic metrics, and rich content.
* **S3-Compatible Media Handler**: Integrated video and asset storage supporting Cloudflare R2 and MinIO via the AWS S3 .NET SDK, supporting presigned URLs and streaming endpoints.
* **Admin Management Portal**: Secure dashboard built with Razor MVC & Tailwind CSS for handling content publishing, media uploads, and portfolio metrics.
* **Multi-Factor Authentication (MFA)**: Built-in 2FA workflow and session management using JWT and security tokens.
* **Robust Automated Deployment**: Automated CI/CD pipelines via GitHub Actions, building multi-stage Docker images and deploying seamlessly behind an Nginx reverse proxy.

---

## 🛠️ Tech Stack

### **Backend**
* **Framework:** ASP.NET Core MVC / Web API (.NET 8)
* **Language:** C# 12
* **ORM:** Entity Framework Core / Dapper
* **Authentication:** ASP.NET Core Identity / JWT / 2FA

### **Database & Storage**
* **Database:** MariaDB / MySQL
* **Object Storage:** Cloudflare R2 / MinIO (via AWS S3 .NET SDK)

### **Frontend**
* **Templating & UI:** Razor Pages / MVC, Angular (Admin Components)
* **Styling:** Tailwind CSS

### **DevOps & Infrastructure**
* **Containerization:** Docker & Docker Compose
* **Web Server / Reverse Proxy:** Nginx (Certbot SSL / TLS Encryption)
* **CI/CD:** GitHub Actions workflows

---

## 📁 Repository Structure

```text
portfolio-main/
├── src/
│   ├── Portfolio.Web/             # ASP.NET Core Web MVC / API Application
│   │   ├── Controllers/           # Public & Admin API Controllers
│   │   ├── Models/                # ViewModels and DTOs
│   │   ├── Views/                 # Razor Views & Admin Dashboard Layouts
│   │   ├── Services/              # S3 Storage, Auth, and Business Logic
│   │   ├── wwwroot/               # Static assets (CSS, JS, Images)
│   │   ├── Program.cs             # Application Entry Point & Dependency Injection
│   │   └── appsettings.json       # App Configurations
│   ├── Portfolio.Infrastructure/  # EF Core DbContext, Migrations & Storage Providers
│   └── Portfolio.Core/            # Core Domain Entities and Interfaces
├── docker/
│   ├── Dockerfile                 # Multi-stage production build
│   ├── docker-compose.yml         # Compose config for App, Database & MinIO
│   └── nginx/                     # Nginx configuration files
├── .github/
│   └── workflows/                 # CI/CD deployment pipelines
├── README.md
└── Portfolio.sln
