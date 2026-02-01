using Link.Domain.Common;

namespace Link.Domain.Entities;

public sealed class LinkObject : BaseEntity
{
    public string OriginalUrl { get; set; } = string.Empty;
    // Default expiration is 30 days, can be null (no expiry)
    public string ShortCode { get; set; } = string.Empty;

    public DateTime? ExpirationDate { get; set; }


    // Relationships: who created this link
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}