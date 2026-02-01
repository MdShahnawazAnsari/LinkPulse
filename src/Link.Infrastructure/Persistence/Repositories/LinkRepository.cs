using Link.Application.Interfaces;
using Link.Domain.Entities;
using Link.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Link.Infrastructure.Persistence.Repositories;

public class LinkRepository : ILinkRepository
{

    private readonly AppDbContext _context;

    public LinkRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(LinkObject linkObject)
    {
        await _context.LinkObjects.AddAsync(linkObject);
    }

    public async Task<bool> ExistsAsync(string shortCode)
    {
        return await _context.LinkObjects.AnyAsync(l => l.ShortCode == shortCode);
    }

    public async Task<LinkObject?> LinkExistsAsync(string link)
    {
        return await _context.LinkObjects.AsNoTracking().FirstOrDefaultAsync(l => l.OriginalUrl == link);
    }

    public async Task<LinkObject?> GetByShortCodeAsync(string shortCode)
    {
        return await _context.LinkObjects
        .AsNoTracking()
        .FirstOrDefaultAsync(l => l.ShortCode == shortCode);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}