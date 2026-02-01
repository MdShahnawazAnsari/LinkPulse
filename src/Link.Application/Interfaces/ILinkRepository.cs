using Link.Domain.Entities;

namespace Link.Application.Interfaces;

public interface ILinkRepository
{
    Task<LinkObject?> GetByShortCodeAsync(string shortCode);
    Task<bool> ExistsAsync(string shortCode);

    Task<LinkObject?> LinkExistsAsync(string link);

    Task AddAsync(LinkObject link);
    Task SaveChangesAsync();
}
