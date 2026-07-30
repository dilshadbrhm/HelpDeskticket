using HelpdeskSystem.Application.DTOs;

namespace HelpdeskSystem.Application.Interfaces;

public interface IPdfReportService
{
    byte[] GenerateStatsReport(TicketStatsDto stats);
}