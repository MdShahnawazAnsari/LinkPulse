namespace Link.Domain.Entities;


// No BaseEntity here because Analytics tables often don't need Update/Id overhead
// They are "Append Only" logs.
public sealed class ClickEvent
{
    public string Shortcode { get; set; } = string.Empty;
    public DateTime ClickedAt { get; set; }
    public string IpAddress { get; set; } = string.Empty;

    public string UserAgent { get; set; } = string.Empty;
    public string? Refrer { get; set; }
}