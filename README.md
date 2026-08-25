# Portfolio Web Application — Free Hosted Architecture

[![Render](https://img.shields.io/badge/Render-Free%20Tier-46E3B7?logo=render&logoColor=white)](https://render.com/)
[![Cloudflare R2](https://img.shields.io/badge/Cloudflare-R2%20Storage-F38020?logo=cloudflare&logoColor=white)](https://www.cloudflare.com/developer-platform/r2/)
[![Layerbase](https://img.shields.io/badge/Layerbase-MariaDB-003545?logo=mariadb&logoColor=white)](https://layerbase.com/)
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)

This branch (`Portfolio/FreeHostedBranch`) contains the production-ready configuration optimized for **zero-cost, high-performance cloud hosting**. It adapts the portfolio infrastructure to run seamlessly across distributed free-tier cloud platforms: **Render** for application compute, **Layerbase** for managed MariaDB database hosting, and **Cloudflare R2** for S3-compatible media asset storage.

---

## 🏗️ Cloud Infrastructure Architecture

```text
               +----------------------------------+
               |          User / Client           |
               +----------------------------------+
                                |
                                v
               +----------------------------------+
               |        Render Web Service        |
               |      (ASP.NET Core .NET 8)       |
               +----------------------------------+
                   /                          \
                  /                            \
                 v                              v
+-------------------------------+  +-------------------------------+
|     Layerbase Managed DB      |  |     Cloudflare R2 Storage     |
|       (MariaDB / MySQL)       |  |  (Media Uploads & Streaming)  |
+-------------------------------+  +-------------------------------+
