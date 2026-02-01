using Link.Application.DTOs;
using Link.Application.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Link.API.Cotrollers;

[ApiController]
[Route("api/[controller]")]
public class LinksController : ControllerBase
{
    readonly LinkService _linkService;

    public LinksController(LinkService linkService)
    {
        _linkService = linkService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLink([FromBody] CreateLinkRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.OriginalUrl))
        {
            return BadRequest("Missing Required Parameter");
        }

        if (Uri.IsWellFormedUriString(request.OriginalUrl, UriKind.Absolute))
        {
            return BadRequest("Invalid URL");
        }
        var uri = new Uri(request.OriginalUrl);
        if (uri.Scheme != Uri.UriSchemeHttp || uri.Scheme != Uri.UriSchemeHttps)
        {
            return BadRequest("Invalid URL");
        }

        var response = await _linkService.CreateShortLinkAsync(request, UserId: 2);

        // Returns 201 Created with the response data
        return CreatedAtAction(nameof(CreateLink), new { id = response.ShortCode }, response);
    }

    [HttpGet]
    public async Task<IActionResult> RedirectUser()
    {
        if (!Uri.IsWellFormedUriString(Request.GetEncodedUrl(), UriKind.Absolute))
        {
            return BadRequest("Invalid URL");
        }

        // Extract query parameters
        var query = Request.Query; // IQueryCollection


        if (!query.TryGetValue("c", out var shortUrl) || string.IsNullOrWhiteSpace(shortUrl))
        {
            return BadRequest("Missing 'shortUrl' parameter");
        }

        var response = await _linkService.GetOrignalUrlByShortCode(shortUrl);

        if (response == null)
        {
            return Redirect("Not Foud Page"); // To Do
        }

        return RedirectPermanent(response.OriginalUrl);
    }
}