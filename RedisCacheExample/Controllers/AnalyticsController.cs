using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisCacheExample.Services;

namespace RedisCacheExample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnalyticsController : ControllerBase
{
    private readonly AnalyticsService _analyticsService;

    public AnalyticsController(AnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    [HttpPost("record/{page}")]
    public async Task<IActionResult> RecordPageView(string page)
    {
        await _analyticsService.RecordPageViewAsync(page);
        return Ok(new
        {
            isSuccessful = true,
            message = "Record was done!"
        });
    }

    [HttpGet("views/{page}")]
    public async Task<IActionResult> GetPageViews(string page)
    {
        var views = await _analyticsService.GetPageViewsAsync(page);
        return Ok(new { Page = page, Views = views });
    }
}
