using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using HelpdeskSystem.Application.DTOs;
using HelpdeskSystem.Application.Interfaces;

namespace HelpdeskSystem.Infrastructure.Services;

public class PdfReportService : IPdfReportService
{
    public PdfReportService()
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public byte[] GenerateStatsReport(TicketStatsDto stats)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header()
                    .Text("Helpdesk Sistemi — Statistika Hesabatı")
                    .SemiBold().FontSize(18);

                page.Content().Column(column =>
                {
                    column.Spacing(15);

                    column.Item().Text($"Yaradılma tarixi: {DateTime.Now:dd.MM.yyyy HH:mm}").FontSize(9).FontColor(Colors.Grey.Medium);

                    column.Item().PaddingTop(10).Text("Ümumi Göstəricilər").SemiBold().FontSize(14);

                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        AddRow(table, "Ümumi Tiket Sayı", stats.TotalTickets.ToString());
                        AddRow(table, "Açıq", stats.OpenTickets.ToString());
                        AddRow(table, "Baxılır", stats.InProgressTickets.ToString());
                        AddRow(table, "Həll olundu", stats.ResolvedTickets.ToString());
                        AddRow(table, "Bağlandı", stats.ClosedTickets.ToString());
                        AddRow(table, "Orta Həll Vaxtı (saat)", stats.AverageResolutionHours.ToString("0.0"));
                    });

                    column.Item().PaddingTop(15).Text("Kateqoriyaya Görə Bölgü").SemiBold().FontSize(14);

                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Kateqoriya").SemiBold();
                            header.Cell().Text("Say").SemiBold();
                        });

                        foreach (var cat in stats.CategoryStats)
                        {
                            table.Cell().Text(cat.CategoryName);
                            table.Cell().Text(cat.Count.ToString());
                        }
                    });

                    column.Item().PaddingTop(15).Text("Agentlərə Görə Bölgü").SemiBold().FontSize(14);

                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Agent").SemiBold();
                            header.Cell().Text("Təyin olunub").SemiBold();
                            header.Cell().Text("Həll olunub").SemiBold();
                        });

                        foreach (var agent in stats.AgentStats)
                        {
                            table.Cell().Text(agent.AgentName);
                            table.Cell().Text(agent.AssignedCount.ToString());
                            table.Cell().Text(agent.ResolvedCount.ToString());
                        }
                    });
                });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Səhifə ");
                        x.CurrentPageNumber();
                    });
            });
        });

        return document.GeneratePdf();
    }

    private void AddRow(TableDescriptor table, string label, string value)
    {
        table.Cell().Text(label);
        table.Cell().Text(value).SemiBold();
    }
}