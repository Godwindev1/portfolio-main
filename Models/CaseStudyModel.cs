using System.Text.Json;
using Portfolio.Models;
using Portfolio.ViewModels;

public class CaseStudyViewModel {
   public   CaseStudy CaseStudy { get; set; } = new CaseStudy();
    public  ProblemSection Problem { get; set; } = new ProblemSection();
    public  SolutionSection Solution { get; set; } = new SolutionSection();
}

public class CaseStudyModel
{
    private readonly ICaseStudyRepository _caseStudyRepository;

    public CaseStudyModel(ICaseStudyRepository caseStudyRepository)
    {
        _caseStudyRepository = caseStudyRepository;
    }


    public async Task<List<CaseStudyViewModel>> GetAllCaseStudiesAsync()
    {
        var caseStudies  = await _caseStudyRepository.GetAllAsync(includeDetails: true);

        List<CaseStudyViewModel> result = new List<CaseStudyViewModel>();

        foreach(var cs in caseStudies)
        {
            ProblemSection problem = JsonSerializer.Deserialize<ProblemSection>(cs.ProblemJson) ?? new ProblemSection();
            SolutionSection solution = JsonSerializer.Deserialize<SolutionSection>(cs.SolutionJson) ?? new SolutionSection();

            result.Add(new CaseStudyViewModel {
                CaseStudy = cs,
                Problem = problem,
                Solution = solution
            });
                
        }
        return result;
    }

    public async Task<List<CaseStudyViewModel>> GetFeaturedCaseStudiesAsync()
    {
        var all = await _caseStudyRepository.GetAllAsync(includeDetails: true);
        var Featured = all
            .Where(c => c.IsFeatured)
            .OrderBy(c => c.DisplayOrder)
            .ToList();
        

         List<CaseStudyViewModel> result = new List<CaseStudyViewModel>();

        foreach(var cs in Featured)
        {
            ProblemSection problem = JsonSerializer.Deserialize<ProblemSection>(cs.ProblemJson) ?? new ProblemSection();
            SolutionSection solution = JsonSerializer.Deserialize<SolutionSection>(cs.SolutionJson) ?? new SolutionSection();

            result.Add(new CaseStudyViewModel {
                CaseStudy = cs,
                Problem = problem,
                Solution = solution
            });
                
        }

        return  result;
    }

    public async Task<CaseStudyViewModel?> GetCaseStudyByIdAsync(int id)
    {
        var cs = await _caseStudyRepository.GetByIdAsync(id, includeDetails: true);

        if (cs == null) return null;

        ProblemSection problem = JsonSerializer.Deserialize<ProblemSection>(cs.ProblemJson) ?? new ProblemSection();
        SolutionSection solution = JsonSerializer.Deserialize<SolutionSection>(cs.SolutionJson) ?? new SolutionSection();

        
        return new CaseStudyViewModel {
            CaseStudy = cs,
            Problem = problem,
            Solution = solution
        };
    }

    public async Task SaveCaseStudyAsync(CaseStudy caseStudy)
    {
        if (caseStudy.Id == 0)
        {
            await _caseStudyRepository.AddAsync(caseStudy);
        }
        else
        {
            await _caseStudyRepository.UpdateAsync(caseStudy);
        }
    }

    public async Task CreateCaseStudyAsync(CaseStudy caseStudy)
    {
        await _caseStudyRepository.AddAsync(caseStudy);
    }

    public async Task UpdateAsync(CaseStudy caseStudy)
    {
        await _caseStudyRepository.UpdateAsync(caseStudy);
    }

    public async Task DeleteAsync(int id)
    {
        await _caseStudyRepository.DeleteAsync(id);
    }

    /// <summary>
    /// Converts a CaseStudy (storage model) to SaveCaseStudyViewModel (admin input model)
    /// Used when loading case studies for editing
    /// </summary>
    public SaveCaseStudyViewModel ConvertToViewModel(CaseStudy caseStudy)
    {
        var problem = JsonSerializer.Deserialize<ProblemSection>(caseStudy.ProblemJson) ?? new ProblemSection();
        var solution = JsonSerializer.Deserialize<SolutionSection>(caseStudy.SolutionJson) ?? new SolutionSection();

        var viewModel = new SaveCaseStudyViewModel
        {
            Id = caseStudy.Id,
            Title = caseStudy.Title,
            Summary = caseStudy.Summary,
            Category = caseStudy.Category,
            Label = caseStudy.Label,
            DisplayOrder = caseStudy.DisplayOrder,
            IsFeatured = caseStudy.IsFeatured,
            ExistingCoverImageUrl = caseStudy.CoverImageUrl,
            Problem = problem,
            Solution = solution,

            // Map relational entities
            ImplementationSteps = caseStudy.ImplementationSteps
                .OrderBy(s => s.Order)
                .Select(s => new ImplementationStepInput
                {
                    Id = s.Id,
                    Order = s.Order,
                    Title = s.Title,
                    Description = s.Description
                })
                .ToList(),

            Metrics = caseStudy.Metrics
                .Select(m => new MetricInput
                {
                    Id = m.Id,
                    Label = m.Label,
                    Value = m.Value,
                    Description = m.Description
                })
                .ToList(),

            Skills = caseStudy.Skills
                .Select(s => new SkillInput
                {
                    Id = s.Id,
                    Name = s.Name,
                    Category = s.Category
                })
                .ToList(),

            ArchitectureComponents = caseStudy.ArchitectureComponents
                .Select(a => new ArchitectureComponentInput
                {
                    Id = a.Id,
                    Name = a.Name,
                    Role = a.Role,
                    Tech = a.Tech
                })
                .ToList(),

            // Map artifacts by type
            Documents = caseStudy.Artifacts
                .Where(a => a.Type == ArtifactTypes.Document)
                .Select(a => new UploadArtifactInput
                {
                    Id = a.Id,
                    Label = a.Label,
                    ExistingUrl = a.Url
                })
                .ToList(),

            Videos = caseStudy.Artifacts
                .Where(a => a.Type == ArtifactTypes.Videos)
                .Select(a => new UploadArtifactInput
                {
                    Id = a.Id,
                    Label = a.Label,
                    ExistingUrl = a.Url
                })
                .ToList(),

            Screenshots = caseStudy.Artifacts
                .Where(a => a.Type == ArtifactTypes.ScreenShot)
                .Select(a => new UploadArtifactInput
                {
                    Id = a.Id,
                    Label = a.Label,
                    ExistingUrl = a.Url
                })
                .ToList(),

            implementationDetails = caseStudy.Artifacts
                .Where(a => a.Type == ArtifactTypes.ImplementationDetail)
                .Select(a => new UploadArtifactInput
                {
                    Id = a.Id,
                    Label = a.Label,
                    ExistingUrl = a.Url
                })
                .ToList(),

            Links = caseStudy.Artifacts
                .Where(a => a.Type == ArtifactTypes.Links)
                .Select(a => new LinkArtifactInput
                {
                    Id = a.Id,
                    Label = a.Label,
                    Url = a.Url
                })
                .ToList(),

            Repos = caseStudy.Artifacts
                .Where(a => a.Type == ArtifactTypes.Repo)
                .Select(a => new LinkArtifactInput
                {
                    Id = a.Id,
                    Label = a.Label,
                    Url = a.Url
                })
                .ToList(),

            LiveDemos = caseStudy.Artifacts
                .Where(a => a.Type == ArtifactTypes.Live)
                .Select(a => new LinkArtifactInput
                {
                    Id = a.Id,
                    Label = a.Label,
                    Url = a.Url
                })
                .ToList()
        };

        return viewModel;
    }


}