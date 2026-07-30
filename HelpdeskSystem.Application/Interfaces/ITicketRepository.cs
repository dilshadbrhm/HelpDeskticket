using HelpdeskSystem.Domain.Entities;

namespace HelpdeskSystem.Application.Interfaces;

public interface ITicketRepository
{
    Task<List<Ticket>> GetAllAsync();
    Task<Ticket?> GetByIdAsync(int id);
    Task<List<Ticket>> GetByUserAsync(string userId);
    Task<List<Ticket>> GetByAgentAsync(string agentId);
    Task<Ticket> CreateAsync(Ticket ticket);
    Task UpdateAsync(Ticket ticket);
    Task DeleteAsync(int id);
}