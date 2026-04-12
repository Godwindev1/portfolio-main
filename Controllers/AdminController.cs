

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.ViewModels;


public class AdminController : Controller
{
    private readonly BucketService _bucketService;
    private readonly CaseStudyModel _caseStudyModel;

    public AdminController(BucketService bucketService, ICaseStudyRepository caseStudyRepository)
    {
        _bucketService = bucketService;
        _caseStudyModel = new CaseStudyModel(caseStudyRepository);
    }


    [HttpGet("admin/Experience")]
    public ViewResult Experience()
    {
        Console.WriteLine("Reached AdminController.Experience");
   
        return View("Views/Admin/Experience.cshtml", new List<Experience> { ExperienceReturnDto.Get() });
    }



    [HttpGet("admin/CaseStudy")]
    public ViewResult CaseStudy()
    {
        Console.WriteLine("Reached AdminController.CaseStudy");
   
        return View("Views/Admin/CaseStudyAdd.cshtml", SaveCaseStudyReturnDto.Get());
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