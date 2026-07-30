using Microsoft.EntityFrameworkCore;
using HelpdeskSystem.Domain.Entities;
using HelpdeskSystem.Application.Interfaces;
using HelpdeskSystem.Infrastructure.Persistence;

namespace HelpdeskSystem.Infrastructure.Repositories;

public class TicketCategoryRepository : ITicketCategoryRepository
{
    private readonly AppDbContext _context;

    public TicketCategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TicketCategory>> GetAllAsync()
    {
        return await _context.TicketCategories.ToListAsync();
    }

    public async Task<TicketCategory?> GetByIdAsync(int id)
    {
        return await _context.TicketCategories.FindAsync(id);
    }

    public async Task<TicketCategory> CreateAsync(TicketCategory category)
    {
        _context.TicketCategories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }
}