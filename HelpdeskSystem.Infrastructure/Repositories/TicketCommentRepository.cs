using Microsoft.EntityFrameworkCore;
using HelpdeskSystem.Domain.Entities;
using HelpdeskSystem.Application.Interfaces;
using HelpdeskSystem.Infrastructure.Persistence;

namespace HelpdeskSystem.Infrastructure.Repositories;

public class TicketCommentRepository : ITicketCommentRepository
{
    private readonly AppDbContext _context;

    public TicketCommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TicketComment>> GetByTicketIdAsync(int ticketId)
    {
        return await _context.TicketComments
            .Where(c => c.TicketId == ticketId)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<TicketComment> AddAsync(TicketComment comment)
    {
        _context.TicketComments.Add(comment);
        await _context.SaveChangesAsync();
        return comment;
    }
}