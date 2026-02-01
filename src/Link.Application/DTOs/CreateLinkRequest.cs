namespace Link.Application.DTOs;

public record CreateLinkRequest(string OriginalUrl, int? ExpiryDays);