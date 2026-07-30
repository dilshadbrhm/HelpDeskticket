using HelpdeskSystem.Application.Interfaces;

namespace HelpdeskSystem.Web;

public static class ReportEndpoints
{
    public static void MapReportEndpoints(this WebApplication app)
    {
        app.MapGet("/reports/stats-pdf", async (
            ITicketStatsService statsService,
            IPdfReportService pdfService) =>
        {
            var stats = await statsService.GetStatsAsync();
            var pdfBytes = pdfService.GenerateStatsReport(stats);

            return Results.File(pdfBytes, "application/pdf", $"helpdesk-hesabat-{DateTime.Now:yyyyMMdd}.pdf");
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));
    }
}