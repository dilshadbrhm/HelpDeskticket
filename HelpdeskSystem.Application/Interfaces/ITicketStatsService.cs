using HelpdeskSystem.Application.DTOs;

namespace HelpdeskSystem.Application.Interfaces;

public interface ITicketStatsService
{
    Task<TicketStatsDto> GetStatsAsync();
}