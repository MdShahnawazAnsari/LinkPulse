namespace Link.Application.DTOs
{
    public record LinkResponse(string ShortCode, string OriginalUrl, DateTime CreatedAt);
}