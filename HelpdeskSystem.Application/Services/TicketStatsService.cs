using HelpdeskSystem.Application.DTOs;
using HelpdeskSystem.Application.Interfaces;
using HelpdeskSystem.Domain.Enums;

namespace HelpdeskSystem.Application.Services;

public class TicketStatsService : ITicketStatsService
{
    private readonly ITicketRepository _ticketRepository;

    public TicketStatsService(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<TicketStatsDto> GetStatsAsync()
    {
        var tickets = await _ticketRepository.GetAllAsync();

        var stats = new TicketStatsDto
        {
            TotalTickets = tickets.Count,
            OpenTickets = tickets.Count(t => t.Status == TicketStatus.Open),
            InProgressTickets = tickets.Count(t => t.Status == TicketStatus.InProgress),
            ResolvedTickets = tickets.Count(t => t.Status == TicketStatus.Resolved),
            ClosedTickets = tickets.Count(t => t.Status == TicketStatus.Closed)
        };

        var resolvedWithTime = tickets.Where(t => t.ResolvedAt.HasValue).ToList();
        if (resolvedWithTime.Any())
        {
            stats.AverageResolutionHours = resolvedWithTime
                .Average(t => (t.ResolvedAt!.Value - t.CreatedAt).TotalHours);
        }

        stats.CategoryStats = tickets
            .Where(t => t.Category != null)
            .GroupBy(t => t.Category!.Name)
            .Select(g => new CategoryStatDto { CategoryName = g.Key, Count = g.Count() })
            .ToList();

        stats.AgentStats = tickets
            .Where(t => !string.IsNullOrEmpty(t.AssignedAgentId))
            .GroupBy(t => t.AssignedAgentId)
            .Select(g => new AgentStatDto
            {
                AgentName = g.Key!,
                AssignedCount = g.Count(),
                ResolvedCount = g.Count(t => t.Status == TicketStatus.Resolved || t.Status == TicketStatus.Closed)
            })
            .ToList();

        return stats;
    }
}