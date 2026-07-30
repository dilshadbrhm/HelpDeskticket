using Microsoft.EntityFrameworkCore;
using HelpdeskSystem.Domain.Entities;
using HelpdeskSystem.Application.Interfaces;
using HelpdeskSystem.Infrastructure.Persistence;

namespace HelpdeskSystem.Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _context;

    public TicketRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Ticket>> GetAllAsync()
    {
        return await _context.Tickets
            .Include(t => t.Category)
            .Include(t => t.Comments)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<Ticket?> GetByIdAsync(int id)
    {
        return await _context.Tickets
            .Include(t => t.Category)
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Ticket>> GetByUserAsync(string userId)
    {
        return await _context.Tickets
            .Include(t => t.Category)
            .Where(t => t.CreatedByUserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<Ticket>> GetByAgentAsync(string agentId)
    {
        return await _context.Tickets
            .Include(t => t.Category)
            .Where(t => t.AssignedAgentId == agentId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<Ticket> CreateAsync(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task UpdateAsync(Ticket ticket)
    {
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket != null)
        {
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }
    }
}