

using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.ViewModels;

[Route("briefs")]
public class ProjectbriefController : Controller
{
    private readonly IProjectBriefRepository _projectBriefRepository;
    private readonly IEmailService _service ;

    public ProjectbriefController(IProjectBriefRepository repo, IEmailService service)
    {
        _projectBriefRepository = repo;
        _service = service;
    }

    
    [HttpPost("SubmitBrief")]
    //Set To ignore When Testing with hot reload via dotnet watch run. or antiForgery will fail
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitBrief([FromBody] ProjectBrief model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _projectBriefRepository.AddAsync(model);

        await _service.SendProjectBriefNotificationAsync(model);
        await _service.SendClientConfirmationAsync(model);

        return Ok();
    }

   
}