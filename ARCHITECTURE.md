# Architecture & Component Map

## Application Structure

```
Portfolio/                           ASP.NET Core Web Application
│
├── 📋 Program.cs                   ← Application Entry Point
│   └── Configures services, middleware, routing
│
├── 🎮 Controllers/
│   └── HomeController.cs           ← Data Aggregation Layer
│       ├── Index() action
│       ├── GetProjects()           → List<ProjectCard>
│       ├── GetCaseStudies()        → List<CaseStudy>
│       ├── GetSkillDomains()       → List<SkillDomain>
│       ├── GetExperiences()        → List<Experience>
│       ├── GetTestimonials()       → List<Testimonial>
│       └── GetCertifications()     → List<Certification>
│
├── 📦 Models/
│   └── PortfolioModels.cs          ← Data Contracts (11 classes)
│       ├── ProjectCard
│       ├── CaseStudy
│       ├── SkillDomain
│       ├── SkillItem
│       ├── Experience
│       ├── Testimonial
│       ├── Certification
│       ├── HeroStatus
│       └── PortfolioViewModel (Main aggregator)
│
├── 🎨 Views/
│   ├── Home/
│   │   └── Index.cshtml            ← Main Page Orchestrator
│   │       (Renders all partials using the PortfolioViewModel)
│   │
│   ├── Shared/
│   │   ├── _Layout.cshtml          ← Master Template
│   │   │   └── HTML structure, <head>, scripts
│   │   │
│   │   ├── _Navigation.cshtml      ← Hero Component
│   │   │   (Model: none - static)
│   │   │
│   │   ├── _SidebarWidget.cshtml   ← Sidebar Component
│   │   │   (Model: none - static)
│   │   │
│   │   ├── _Hero.cshtml            ← Hero Section
│   │   │   (Model: HeroStatus)
│   │   │   ├── Status badge
│   │   │   ├── Metrics grid
│   │   │   └── CTAs
│   │   │
│   │   ├── _CaseStudy.cshtml       ← Case Study Component
│   │   │   (Model: CaseStudy)
│   │   │   ├── Arch diagram
│   │   │   ├── Problem statement
│   │   │   ├── Metrics
│   │   │   └── CTA
│   │   │
│   │   ├── _FeaturedWork.cshtml    ← Projects Grid Component
│   │   │   (Model: List<ProjectCard>)
│   │   │   └── Renders 4 project cards
│   │   │
│   │   ├── _TechnicalArsenal.cshtml ← Skills Grid Component
│   │   │   (Model: List<SkillDomain>)
│   │   │   └── 6-column skill domain cards
│   │   │
│   │   ├── _Experience.cshtml      ← Timeline + Testimonials
│   │   │   (Model: (List<Experience>, List<Testimonial>))
│   │   │   ├── Left: experience timeline
│   │   │   └── Right: testimonial cards
│   │   │
│   │   ├── _Certifications.cshtml  ← Credentials Component
│   │   │   (Model: List<Certification>)
│   │   │   ├── Certificate rows
│   │   │   └── Philosophy block
│   │   │
│   │   ├── _Contact.cshtml         ← Contact Section
│   │   │   (Model: none - static)
│   │   │   └── Email + social links
│   │   │
│   │   ├── _Footer.cshtml          ← Footer
│   │   │   (Model: none - static)
│   │   │   └── Copyright info
│   │   │
│   │   └── _ViewStart.cshtml       ← View Initialization
│   │       └── Sets Layout = "_Layout"
│   │
│   └── _ViewStart.cshtml           ← Global view setup
│
├── 🎯 wwwroot/                     ← Static Files (Served to browser)
│   ├── css/
│   │   └── site.css                ← All Styling (1000+ lines)
│   │       ├── CSS Variables
│   │       │   ├── Dark theme (default)
│   │       │   └── Light theme (togglable)
│   │       ├── Base styles
│   │       ├── Component styles
│   │       ├── Layout styles
│   │       ├── Animation keyframes
│   │       └── Responsive rules
│   │
│   └── js/
│       └── site.js                 ← Client-Side Logic
│           ├── Theme toggle
│           ├── Custom cursor
│           ├── Scroll reveal
│           └── Nav highlighting
│
├── ⚙️ Configuration Files
│   ├── Portfolio.csproj            ← .NET 8.0 Project
│   ├── appsettings.json            ← Production config
│   ├── appsettings.Development.json ← Dev config
│   └── .gitignore                  ← Git ignore rules
│
└── 📚 Documentation
    ├── README.md                   ← Full project guide
    ├── QUICKSTART.md               ← Getting started
    └── ARCHITECTURE.md             ← This file


════════════════════════════════════════════════════════════════════════════════

## Data Flow Diagram

┌─────────────┐
│   Browser   │
└──────┬──────┘
       │ HTTP GET /
       ▼
┌─────────────────────────────────────────────────────────────────┐
│                      HomeController                              │
│                                                                   │
│  public IActionResult Index()                                    │
│  {                                                               │
│    var viewModel = new PortfolioViewModel                       │
│    {                                                             │
│      Projects = GetProjects(),           ◄── 4 items           │
│      CaseStudies = GetCaseStudies(),     ◄── 1 item            │
│      SkillDomains = GetSkillDomains(),   ◄── 6 items           │
│      Experiences = GetExperiences(),     ◄── 3 items           │
│      Testimonials = GetTestimonials(),   ◄── 2 items           │
│      Certifications = GetCertifications()◄── 4 items           │
│    };                                                           │
│    return View(viewModel);                                      │
│  }                                                              │
└────────────────────┬────────────────────────────────────────────┘
                     │
                     │ PortfolioViewModel
                     │
                     ▼
┌─────────────────────────────────────────────────────────────────┐
│                  Views/Home/Index.cshtml                         │
│                                                                   │
│  @model PortfolioViewModel                                       │
│  <partial name="_Hero" model="Model.HeroStatus" />              │
│  <partial name="_CaseStudy" model="Model.CaseStudies..." />     │
│  <partial name="_FeaturedWork" model="Model.Projects" />        │
│  <partial name="_TechnicalArsenal" model="Model.SkillDom..." /> │
│  <partial name="_Experience" model="(Model.Exp, Model.Test)" /> │
│  <partial name="_Certifications" model="Model.Certs" />         │
│  <partial name="_Contact" />                                     │
└────────────────────┬────────────────────────────────────────────┘
                     │
          ┌──────────┴──────────┬──────────┬────────────┬─────────────┐
          │                     │          │            │             │
          ▼                     ▼          ▼            ▼             ▼
    ┌──────────────┐   ┌─────────────┐ ┌────────────┐ ┌──────────┐ ┌──────────┐
    │ _Layout.css  │   │ _Hero.html  │ │ _Work.html │ │ _Exp.html│ │_Cert.html│
    │ site.css     │   │ (HeroStatus)│ │(Projects)  │ │(Exp+Test)│ │(Certs)   │
    │ site.js      │   └──────────────┘ └────────────┘ └──────────┘ └──────────┘
    └──────────────┘
          │
          ▼
    ┌──────────────────────────┐
    │  Complete HTML Response  │
    │                          │
    │  <!DOCTYPE html>         │
    │  <html>                  │
    │    <head>...</head>      │
    │    <body>                │
    │      (All sections)      │
    │    </body>               │
    │  </html>                 │
    └──────────────────────────┘
          │
          ▼
    ┌──────────────────────────┐
    │      Browser Renders     │
    │                          │
    │   + Applies CSS          │
    │   + Runs JavaScript      │
    │   + Shows Interactive UI │
    └──────────────────────────┘


════════════════════════════════════════════════════════════════════════════════

## Component Dependencies

┌─────────────────────────────────────────────────────────────────────────┐
│                        PortfolioViewModel                               │
│  ┌────────────────────────────────────────────────────────────────┐   │
│  │ Properties:                                                    │   │
│  │  • HeroStatus HeroStatus                                       │   │
│  │  • List<ProjectCard> Projects                                  │   │
│  │  • List<CaseStudy> CaseStudies                                 │   │
│  │  • List<SkillDomain> SkillDomains                              │   │
│  │  • List<Experience> Experiences                                │   │
│  │  • List<Testimonial> Testimonials                              │   │
│  │  • List<Certification> Certifications                          │   │
│  └────────────────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────────────────┘
         │                  │                 │                │
         ▼                  ▼                 ▼                ▼
    ┌─────────┐        ┌──────────┐     ┌───────────┐    ┌──────────┐
    │HeroStatus       │ProjectCard│     │CaseStudy  │    │SkillDom. │
    ├─────────┤       ├──────────┤     ├───────────┤    ├──────────┤
    │Status   │       │Category  │     │Label      │    │Number    │
    │StatusMsg│       │Title     │     │Title      │    │Icon      │
    │Metrics[]│       │Desc      │     │Problem    │    │Title     │
    └─────────┘       │Tags[]    │     │Body       │    │Items[]   │
                      └──────────┘     │Metrics[]  │    └──────────┘
                                       │Tags[]     │
                                       └───────────┘


════════════════════════════════════════════════════════════════════════════════

## Request/Response Cycle

1. USER LOADS https://localhost:5001
   
2. ROUTER matches "/" → HomeController.Index()
   
3. CONTROLLER executes:
   - Creates PortfolioViewModel
   - Populates via GetProjects(), GetCaseStudies(), etc.
   - Returns View(viewModel)
   
4. RAZOR ENGINE processes Views/Home/Index.cshtml:
   - Renders _Layout.cshtml wrapper
   - Calls all partial views in order
   - Each partial builds its HTML from model data
   - Includes site.css and site.js
   
5. HTML RESPONSE is sent to browser (complete page)
   
6. BROWSER:
   - Parses HTML
   - Downloads & applies site.css
   - Downloads & executes site.js
   - User sees interactive portfolio
   
7. USER INTERACTS:
   - Clicks theme toggle → site.js toggles 'light' class on <html>
   - CSS variables update → entire page recolors instantly
   - Hovers on elements → custom cursor grows & recolors
   - Scrolls → JavaScript reveals fade-up animations
   - Clicks links → Navbar highlights active section


════════════════════════════════════════════════════════════════════════════════

## Styling Layers (CSS Cascade)

site.css is organized as:
┌────────────────────────────────────────┐
│  1. CSS Variables (Colors, fonts)      │
│  2. Base Styles (*selector, html, body) │
│  3. Component Styles (.nav, .button)   │
│  4. Layout Styles (.grid, .flex)       │
│  5. Animation Styles (@keyframes)      │
│  6. Responsive Rules (@media)          │
│  7. Utility Classes (.fade-up)         │
│  8. Dark/Light Mode Overrides          │
└────────────────────────────────────────┘

THEME SWITCHING:
┌─────────────────────┐
│  html.light { ... } │ ← Added when user clicks toggle
└─────────────────────┘
        │
        └──► Overrides CSS variables
             :root { --bg: dark } → html.light { --bg: light }
             All colors update via CSS inheritance


════════════════════════════════════════════════════════════════════════════════

## Modification Points

To add a NEW section (e.g., BLOG):

1. CREATE MODEL in PortfolioModels.cs:
   public class BlogPost { ... }

2. CREATE METHOD in HomeController.cs:
   private List<BlogPost> GetBlogPosts() { ... }

3. ADD PROPERTY to PortfolioViewModel:
   public List<BlogPost> BlogPosts { get; set; }

4. POPULATE in HomeController.Index():
   BlogPosts = GetBlogPosts()

5. CREATE PARTIAL in Views/Shared/_Blog.cshtml:
   @model List<Portfolio.Models.BlogPost>
   <section id="blog"> ... </section>

6. RENDER in Views/Home/Index.cshtml:
   <partial name="_Blog" model="Model.BlogPosts" />

7. ADD STYLES to wwwroot/css/site.css:
   .blog-grid { ... }
   .blog-card { ... }

8. ADD NAV LINK in Views/Shared/_Navigation.cshtml:
   <a href="#blog">BLOG</a>

════════════════════════════════════════════════════════════════════════════════

Built with ❤️ in C# // ASP.NET Core 8.0 // MVC Pattern
