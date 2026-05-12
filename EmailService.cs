using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;

public interface IEmailService
{
    Task SendProjectBriefNotificationAsync(ProjectBrief brief);
    Task SendClientConfirmationAsync(ProjectBrief brief);
}

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;
    private readonly HttpClient _httpClient;

    public EmailService(IOptions<EmailSettings> settings, HttpClient httpClient)
    {
        _settings = settings.Value;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api.brevo.com/v3");
        _httpClient.DefaultRequestHeaders.Add("api-key", _settings.BrevoApiKey);
    }

    private async Task SendAsync(string toEmail, string toName, string subject, string htmlBody)
    {
        var payload = new
        {
            sender = new { name = _settings.SenderName, email = _settings.SenderEmail },
            to = new[] { new { email = toEmail, name = toName } },
            subject,
            htmlContent = htmlBody
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/v3/smtp/email", content);
        response.EnsureSuccessStatusCode();
    }

    public async Task SendProjectBriefNotificationAsync(ProjectBrief brief)
    {
        var html = $"""
            <div style="font-family:monospace; max-width:600px; margin:0 auto; background:#0a0a0a; color:#e5e5e5; padding:40px; border:1px solid #333;">
                <h2 style="color:#c8a96e; font-style:italic; margin-bottom:24px;">New Project Brief Received</h2>

                <table style="width:100%; border-collapse:collapse;">
                    <tr><td style="padding:8px 0; color:#888; width:140px;">PROJECT</td>
                        <td style="padding:8px 0;">{brief.ProjectName}</td></tr>
                    <tr><td style="padding:8px 0; color:#888;">CLIENT</td>
                        <td style="padding:8px 0;">{brief.Email}</td></tr>
                    <tr><td style="padding:8px 0; color:#888;">SCOPE</td>
                        <td style="padding:8px 0;">{brief.ProjectScope}</td></tr>
                    <tr><td style="padding:8px 0; color:#888;">ROLE</td>
                        <td style="padding:8px 0;">{brief.RoleNeeded}</td></tr>
                    <tr><td style="padding:8px 0; color:#888;">TIMELINE</td>
                        <td style="padding:8px 0;">{brief.Timeline}</td></tr>
                    <tr><td style="padding:8px 0; color:#888;">BUDGET</td>
                        <td style="padding:8px 0;">{brief.Budget ?? "—"}</td></tr>
                </table>

                <div style="margin-top:24px; padding:16px; border:1px solid #333; background:#111;">
                    <div style="color:#888; font-size:.75rem; margin-bottom:8px;">PROJECT DESCRIPTION</div>
                    <p style="margin:0; line-height:1.7;">{brief.ProjectDescription}</p>
                </div>

                {(string.IsNullOrWhiteSpace(brief.Quote) ? "" : $"""
                <div style="margin-top:16px; padding:16px; border:1px solid #333; background:#111;">
                    <div style="color:#888; font-size:.75rem; margin-bottom:8px;">ADDITIONAL MESSAGE</div>
                    <p style="margin:0; line-height:1.7;">{brief.Quote}</p>
                </div>
                """)}
            </div>
        """;

        await SendAsync(_settings.SenderEmail, _settings.SenderName,
            $"[NEW BRIEF] {brief.ProjectName} — {brief.Email}", html);
    }

    public async Task SendClientConfirmationAsync(ProjectBrief brief)
    {
        var html = $"""
            <div style="font-family:monospace; max-width:600px; margin:0 auto; background:#0a0a0a; color:#e5e5e5; padding:40px; border:1px solid #333;">
                <h2 style="color:#c8a96e; font-style:italic; margin-bottom:8px;">Project Brief Received</h2>
                <p style="color:#888; margin-bottom:32px;">godwinoluowho.dev</p>

                <p style="line-height:1.8;">Hi {brief.Email},</p>
                <p style="line-height:1.8;">
                    Thank you for reaching out. I've received your project brief for
                    <strong style="color:#c8a96e;">{brief.ProjectName}</strong> and will review the details shortly.
                </p>
                <p style="line-height:1.8;">
                    You can expect a personalised response within <strong>24 hours</strong> where I'll share
                    my initial thoughts and proposed approach.
                </p>

                <div style="margin-top:32px; padding:16px; border-left:2px solid #c8a96e;">
                    <p style="margin:0; color:#888; font-size:.85rem; line-height:1.7;">
                        In the meantime, feel free to review my work at
                        <a href="https://godwinoluowho.dev" style="color:#c8a96e;">godwinoluowho.dev</a>
                        or reach out directly at
                        <a href="mailto:godwin@godwinoluowho.dev" style="color:#c8a96e;">godwin@godwinoluowho.dev</a>
                    </p>
                </div>

                <p style="margin-top:32px; color:#888; font-size:.8rem;">— Godwin Oluowho</p>
            </div>
        """;

        await SendAsync(brief.Email, brief.Email,
            "Project Brief Confirmed — I'll Be In Touch Shortly", html);
    }
}