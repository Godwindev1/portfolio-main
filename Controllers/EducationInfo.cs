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
                    Institution = "University of Benin",
                    DateRange   = "2017 – 2021",
                    Classification = "Second Class Upper",
                    GPA      = "3.74 / 5.0",          // null to hide
                    Description = "Focused on software engineering, database systems, and networks.",
                    Highlights = new() {
                        "Final year project: real-time vehicle tracking system (ASP.NET Core + SignalR)",
                        "Member, Computer Science Students' Association"
                    },
                    VerifyUrl = null                   // or a real URL to show the VERIFY button
                }
            };
    }
} 


