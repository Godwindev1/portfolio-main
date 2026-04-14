using Portfolio.Models;

public static class CertificationsReturnDto 
{
    public static List<Certification> Get() 
    {

        return new List<Certification>
        {
            new() {
                Icon = "☁",
                Name = "AWS Certified Solutions Architect",
                Grade = "ASSOCIATE // PROFESSIONAL GRADE",
                Year = "2023",
                Provider = "Amazon Web Services",
                BadgeUrl = "#"
            },
            new() {
                Icon = "⎈",
                Name = "CKAD: Certified Kubernetes Application Developer",
                Grade = "LINUX FOUNDATION // CLOUD NATIVE",
                Year = "2022",
                Provider = "CNCF",
                BadgeUrl = "#"
            },
            new() {
                Icon = "◈",
                Name = "HashiCorp Certified: Terraform Associate",
                Grade = "INFRASTRUCTURE AUTOMATION // MULTI-CLOUD",
                Year = "2022",
                Provider = "HashiCorp",
                BadgeUrl = "#"
            },
            new() {
                Icon = "◎",
                Name = "GitHub Actions: CI/CD Specialist",
                Grade = "AUTOMATION // PIPELINE ENGINEERING",
                Year = "2023",
                Provider = "GitHub",
                BadgeUrl = "#"
            }
        };
    }
}