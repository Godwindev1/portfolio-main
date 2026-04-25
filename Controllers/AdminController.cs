

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.ViewModels;


[Authorize(policy: "AdminOnly")]
public class AdminController : Controller
{
    private readonly BucketService _bucketService;
    private readonly CaseStudyModel _caseStudyModel;
    private readonly IExperienceRepository _experienceRepo;
    private readonly ITestimonialRepository _testimonialRepo;
    private readonly ICertificationRepository _CertificationRepo;
    private readonly ISkillDomainReposirtory _skilldomainRepository;


    public AdminController(BucketService bucketService, ICaseStudyRepository caseStudyRepository, IExperienceRepository experienceRepository, ITestimonialRepository testimonialRepository, ICertificationRepository certificationRepository, ISkillDomainReposirtory skillDomainReposirtory)
    {
        _bucketService = bucketService;
        _caseStudyModel = new CaseStudyModel(caseStudyRepository);
        _experienceRepo = experienceRepository;
        _testimonialRepo = testimonialRepository;
        _CertificationRepo = certificationRepository;
        _skilldomainRepository = skillDomainReposirtory;
    }

        [HttpGet("admin")]
    public IActionResult Dashboard()
    {
        return View("Views/Admin/AdminDashboard.cshtml");
    }

    //SKILL DOMAINS (Technical Arsenal )
    [HttpGet("admin/TechnicalArsenal")]
    public async Task<ViewResult> TechnicalArsenal()
    {
        Console.WriteLine("Reached AdminController.SkillDomain");

        var Dtos = await _skilldomainRepository.GetAllAsync();
   
        return View("Views/Admin/TechnicalArsenal.cshtml", Dtos);
    }


    [HttpPost("admin/TechnicalArsenal/Save", Name = "SaveSkill")]
    public  async  Task<IActionResult> TechnicalArsenal(SkillDomain domain)
    {
        bool isEdit = domain != null && domain.Id != null;

        if(isEdit)
        {
            await _skilldomainRepository.UpdateAsync(domain);
        }
        else
        await _skilldomainRepository.AddAsync(domain);

        return LocalRedirect("~/admin/TechnicalArsenal");
    }

    public async Task<IActionResult> DeleteSkillDomain([FromForm]int id)
    {
        await _skilldomainRepository.DeleteAsync(id);
        return LocalRedirect("~/admin/TechnicalArsenal");
    }


    //CERTIFICATIONS 
    [HttpGet("admin/Certifications")]
    public async Task<ViewResult> Certification()
    {
        Console.WriteLine("Reached AdminController.Certifications");

        var Dtos = await _CertificationRepo.GetAllAsync();
   
        return View("Views/Admin/Certifications.cshtml", Dtos);
    }


    [HttpPost("admin/Certifications/Save", Name = "SaveCertifications")]
    public  async  Task<IActionResult> Certification(Certification SaveCertifications)
    {
        bool isEdit = SaveCertifications != null && SaveCertifications.Id != null;

        if(isEdit)
        {
            await _CertificationRepo.UpdateAsync(SaveCertifications);
        }
        else
        await _CertificationRepo.AddAsync(SaveCertifications);

        return LocalRedirect("~/admin/Certifications");
    }

    public async Task<IActionResult> DeleteCertification([FromForm]int id)
    {
        await _CertificationRepo.DeleteAsync(id);
        return LocalRedirect("~/admin/Certifications");
    }

    //TESTIMONIALS
    [HttpGet("admin/Testimonial")]
    public async Task<ViewResult> Testimonial()
    {
        Console.WriteLine("Reached AdminController.Testimonial");

        var Dtos = await _testimonialRepo.GetAllAsync();
   
        return View("Views/Admin/Testimonials.cshtml", Dtos);
    }


    [HttpPost("admin/Testimonial/Save", Name = "SaveTestimonial")]
    public  async  Task<IActionResult> Experience(Testimonial SaveTestimonial)
    {
        bool isEdit = SaveTestimonial != null && SaveTestimonial.Id != null;

        if(isEdit)
        {
            await _testimonialRepo.UpdateAsync(SaveTestimonial);
        }
        else
        await _testimonialRepo.AddAsync(SaveTestimonial);

        return LocalRedirect("~/admin/Testimonial");
    }

    public async Task<IActionResult> DeleteTestimonial([FromForm]int id)
    {
        await _testimonialRepo.DeleteAsync(id);
        return LocalRedirect("~/admin/Testimonial");
    }


    //EXPERIENC AND WORKHISTORY
    [HttpGet("admin/Experience")]
    public async Task<ViewResult> Experience()
    {
        Console.WriteLine("Reached AdminController.Experience");

        var Dtos = await _experienceRepo.GetAllAsync();
   
        return View("Views/Admin/Experience.cshtml", Dtos);
    }


    [HttpPost("admin/Experience/Save", Name = "SaveExperience")]
    public  async  Task<IActionResult> Experience(Experience SaveExperience)
    {
        bool isEdit = SaveExperience != null && SaveExperience.Id != null;
        if(isEdit)
        {
            await _experienceRepo.UpdateAsync(SaveExperience);
        }
        else
        await _experienceRepo.AddAsync(SaveExperience);

        return LocalRedirect("~/admin/Experience");
    }

    public async Task<IActionResult> DeleteExperience([FromForm]int id)
    {
        await _experienceRepo.DeleteAsync(id);
        return LocalRedirect("~/admin/Experience");
    }


    //CASESTUDIES
    [HttpGet("admin/CaseStudy/List")]
    public async Task<ViewResult> CaseStudyList()
    {
        Console.WriteLine("Reached AdminController.CaseStudyList");
        
        var caseStudies = await _caseStudyModel.GetAllCaseStudiesAsync();
        
        // Convert CaseStudyViewModel to SaveCaseStudyViewModel for display
        var viewModels = caseStudies
            .Select(cs => _caseStudyModel.ConvertToViewModel(cs.CaseStudy))
            .ToList();
   
        return View("Views/Admin/CaseStudyList.cshtml", viewModels);
    }

    [HttpGet("admin/CaseStudy")]
    public async Task<ViewResult> CaseStudy(int? id = null)
    {
        Console.WriteLine("Reached AdminController.CaseStudy");
        
        SaveCaseStudyViewModel viewModel;
        
        if (id.HasValue && id > 0)
        {
            // Load existing case study for editing
            var caseStudy = await _caseStudyModel.GetCaseStudyByIdAsync(id.Value);
            if (caseStudy == null)
            {
                viewModel = SaveCaseStudyReturnDto.Get();
            }
            else
            {
                viewModel = _caseStudyModel.ConvertToViewModel(caseStudy.CaseStudy);
            }
        }
        else
        {
            // Create new case study
            viewModel = SaveCaseStudyReturnDto.Get();
        }
   
        return View("Views/Admin/CaseStudyAdd.cshtml", viewModel);
    }

    [HttpPost("admin/CaseStudy/Save", Name = "SaveCaseStudy")]
    public  async  Task<IActionResult> CaseStudy(SaveCaseStudyViewModel SaveCaseStudy)
    {
        Console.WriteLine("Received CaseStudy data:");


        string CoverImageUrl = SaveCaseStudy.ExistingCoverImageUrl ?? "";
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

        // LINKS
        foreach (var link in SaveCaseStudy.Links)
        {
            if (string.IsNullOrWhiteSpace(link.Url)) continue;

            artifacts.Add(new ArtifactLink {
                Label = link.Label,
                Url = link.Url,
                Type = ArtifactTypes.Links
            });
        }

        // REPOS
        foreach (var repo in SaveCaseStudy.Repos)
        {
            if (string.IsNullOrWhiteSpace(repo.Url)) continue;

            artifacts.Add(new ArtifactLink {
                Label = repo.Label,
                Url = repo.Url,
                Type = ArtifactTypes.Repo
            });
        }

        // LIVE DEMOS
        foreach (var live in SaveCaseStudy.LiveDemos)
        {
            if (string.IsNullOrWhiteSpace(live.Url)) continue;

            artifacts.Add(new ArtifactLink {
                Label = live.Label,
                Url = live.Url,
                Type = ArtifactTypes.Live
            });
        }

        //NOTE FOR ALL BUCKET STORED FILES THE URL IS THE OBJECT KEY WITHIN THE NAMED BUCKET THIS PORTFOLIO USES
        //ResolvedUrl = ObjectKey
        //MAP IMPLEMENTATION DETAILS Should Ideally Be Just One
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
                    Type = ArtifactTypes.Videos
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

            if(doc.File == null) 
            {   
                artifacts.Add(new ArtifactLink {
                    Label = doc.Label,
                    Url = doc.ExistingUrl ?? "",
                    Type = ArtifactTypes.Document
                });

                continue;
            }

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
            Id = SaveCaseStudy.Id ?? 0,  // 0 for new, existing ID for edit
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
    
        return LocalRedirect("~/admin/CaseStudy/List");
    }

    public async Task<IActionResult> DeleteCaseStudy([FromForm]int id)
    {
        await _caseStudyModel.DeleteAsync(id);
        return LocalRedirect("~/admin/CaseStudy/List");
    }
}