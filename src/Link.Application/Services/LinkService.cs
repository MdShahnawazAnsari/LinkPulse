using Link.Application.DTOs;
using Link.Application.Interfaces;
using Link.Domain.Entities;

namespace Link.Application.Services;

public class LinkService
{
    readonly ILinkRepository _repository;
    readonly IUrlShorteningService _shortener;

    public LinkService(ILinkRepository linkRepository, IUrlShorteningService urlShorteningService)
    {
        _repository = linkRepository;
        _shortener = urlShorteningService;
    }

    public async Task<LinkResponse> CreateShortLinkAsync(CreateLinkRequest request, int UserId)
    {

        LinkObject? link = null;

        link = await _repository.LinkExistsAsync(request.OriginalUrl);

        if (link != null)
        {
            return new LinkResponse(link.ShortCode, link.OriginalUrl, link.CreatedAt);
        }

        string code = _shortener.GenerateShortCode();

        while (await _repository.ExistsAsync(code))
        {
            code = _shortener.GenerateShortCode();
        }

        link = new LinkObject
        {
            OriginalUrl = request.OriginalUrl,
            ShortCode = code,
            UserId = UserId,
            ExpirationDate = request.ExpiryDays.HasValue ? DateTime.UtcNow.AddDays(request.ExpiryDays.Value) : null
        };


        await _repository.AddAsync(link);
        await _repository.SaveChangesAsync();

        return new LinkResponse(link.ShortCode, link.OriginalUrl, link.CreatedAt);
    }


    public async Task<LinkResponse?> GetOrignalUrlByShortCode(string ShortCode)
    {
        LinkObject? link = null;

        // To Do Fetch Url from Reddis


        // if url not found in the Reddis then fetch from database
        link = await _repository.GetByShortCodeAsync(ShortCode);

        if (link == null)
            return null;

        return new LinkResponse(link.ShortCode, link.OriginalUrl, link.CreatedAt);
    }


}