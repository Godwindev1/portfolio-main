namespace Portfolio.Models
{
    public class Education
    {
        /// <summary>
        /// Short glyph or abbreviation shown in the badge cell.
        /// e.g. "BSc", "MSc", "HND", "🎓"
        /// </summary>
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// Full degree title. e.g. "B.Sc. in Computer Science"
        /// </summary>
        public string Degree { get; set; } = string.Empty;

        /// <summary>
        /// Name of the university / polytechnic / institution.
        /// </summary>
        public string Institution { get; set; } = string.Empty;

        /// <summary>
        /// Date range string. e.g. "2018 – 2022" or "Sep 2018 – Jul 2022"
        /// </summary>
        public string DateRange { get; set; } = string.Empty;

        /// <summary>
        /// Optional GPA or grade string. e.g. "3.8 / 4.0" or "4.62 / 5.0"
        /// Leave null or empty to hide the GPA block entirely.
        /// </summary>
        public string? GPA { get; set; }

        /// <summary>
        /// Optional classification or honours string.
        /// e.g. "Second Class Upper", "First Class Honours", "Distinction"
        /// </summary>
        public string? Classification { get; set; }

        /// <summary>
        /// Optional short description, thesis title, major focus, etc.
        /// Rendered as a small paragraph under the degree name.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Optional bullet-point highlights — relevant modules, societies,
        /// awards, or extra-curricular achievements.
        /// </summary>
        public List<string>? Highlights { get; set; }

        /// <summary>
        /// Optional URL to a certificate, transcript, or verification portal.
        /// When provided, a "VERIFY ↗" button is rendered.
        /// </summary>
        public string? VerifyUrl { get; set; }
    }
}
