

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.ViewModels;


public class AdminController : Controller
{
    [HttpGet("admin/CaseStudy")]
    public ViewResult CaseStudy()
    {
        Console.WriteLine("Reached AdminController.CaseStudy");
        return View("Views/Admin/CaseStudyAdd.cshtml", new SaveCaseStudyViewModel ());
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