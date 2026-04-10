

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;


public class AdminController : Controller
{
    [HttpGet("admin/CaseStudy")]
    public ViewResult CaseStudy()
    {
        Console.WriteLine("Reached AdminController.CaseStudy");
        return View("Views/Admin/CaseStudyAdd.cshtml", new CaseStudyViewModel { CaseStudy = new CaseStudy() });
    }

    [HttpPost("admin/CaseStudy/Save")]
    public  async  Task<IActionResult> CaseStudy(CaseStudy caseStudy, [FromServices] ICaseStudyRepository repository)
    {
        Console.WriteLine("Received CaseStudy data:");
        Console.WriteLine($"Title: {caseStudy.Title}");
        Console.WriteLine($"Summary: {caseStudy.Summary}");
        Console.WriteLine($"Category: {caseStudy.Category}");
        // Log other properties as needed


    
        return Ok();
    }
}