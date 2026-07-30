using HelpdeskSystem.Domain.Entities;

namespace HelpdeskSystem.Application.Interfaces;

public interface ITicketCommentRepository
{
    Task<List<TicketComment>> GetByTicketIdAsync(int ticketId);
    Task<TicketComment> AddAsync(TicketComment comment);
}