using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers;

//todo Bcket Url Will use this domain bucket.GodwinOluowho.com and later on Bucket will be hidden and all video and images will come through asp.net
public class HomeController : Controller
{

    private readonly CaseStudyModel Model; 
    private readonly IExperienceRepository _experienceRepo;

    private readonly ITestimonialRepository _testimonialRepo;

    private readonly ICertificationRepository _certificationsRepo;
    private readonly ISkillDomainReposirtory _skilldomainRepository;


    public HomeController(ICaseStudyRepository repository, IExperienceRepository experienceRepository, ITestimonialRepository testimonialRepository, ICertificationRepository certificationRepository, ISkillDomainReposirtory skillDomainReposirtory)
    {
        Model = new CaseStudyModel(repository);
        _experienceRepo = experienceRepository;
        _testimonialRepo = testimonialRepository;
        _certificationsRepo = certificationRepository;
        _skilldomainRepository = skillDomainReposirtory;
    }
    public async Task<IActionResult> Index()
    {
        var viewModel = new PortfolioViewModel
        {
            HeroStatus = new HeroStatus(),
            CaseStudies = await GetCaseStudies(),
            SkillDomains = await GetSkillDomains(),
            Experiences = await GetWorkHistory(),
            Testimonials = await GetTestimonials(),
            Certifications = await GetCertifications()
        };

        return View(viewModel);
    }

    private async  Task<List<CaseStudyViewModel>> GetCaseStudies()
    {
        return await Model.GetAllCaseStudiesAsync();
    }

    private async Task<List<Experience>> GetWorkHistory()
    {
        return await _experienceRepo.GetAllAsync();
    }

        private async Task<List<Testimonial>> GetTestimonials()
    {
        return await _testimonialRepo.GetAllAsync();
    }



    private async Task<List<SkillDomain>> GetSkillDomains()
    {
        return await _skilldomainRepository.GetAllAsync();
    }


    private async  Task<List<Certification>> GetCertifications()
    {
        return await _certificationsRepo.GetAllAsync();
    }
}
