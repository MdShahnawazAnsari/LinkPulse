using Link.Application.DTOs;
using Link.Domain.Entities;

namespace Link.Application.Mappings;

public static class LinkMappingExtensions
{
    public static LinkResponse ToResponse(this LinkObject linkObject)
    {
        return new LinkResponse(
            ShortCode: linkObject.ShortCode,
            OriginalUrl: linkObject.OriginalUrl,
            CreatedAt: linkObject.CreatedAt
        );
    }
}