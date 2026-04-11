using System.Text.Json;
using Portfolio.Models;

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


}