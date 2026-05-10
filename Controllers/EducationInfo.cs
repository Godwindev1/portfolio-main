//EDUCATION INFO WOULD BE HARD CODED AS IT RARELY CHANGESusing Portfolio.Models;
using Portfolio.Models;
using Portfolio.ViewModels;

public static class EducationReturnDto 
{
    public static List<Education> Get() 
    {
       return  new List<Education> { 
                new Education {
                    Icon     = "BSc",
                    Degree   = "B.Sc. in Computer Science",
                    Institution = "Igbinedion University Okada",
                    DateRange   = "2022 – 2026",
                    Classification = "First Class",
                    GPA      = null,          // null to hide
                    Description = "Focused on software engineering, database systems, and networks.",
                    Highlights = new() {
                        "Final year project: Background Processing ",
                        "Member, Computer Science Students' Association"
                    },
                    VerifyUrl = null                   // or a real URL to show the VERIFY button
                }
            };
    }
} 


