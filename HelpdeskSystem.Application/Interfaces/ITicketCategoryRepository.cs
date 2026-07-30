using HelpdeskSystem.Domain.Entities;

namespace HelpdeskSystem.Application.Interfaces;

public interface ITicketCategoryRepository
{
    Task<List<TicketCategory>> GetAllAsync();
    Task<TicketCategory?> GetByIdAsync(int id);
    Task<TicketCategory> CreateAsync(TicketCategory category);
}