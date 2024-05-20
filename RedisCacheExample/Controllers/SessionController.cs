using Microsoft.AspNetCore.Mvc;
using RedisCacheExample.Services;

namespace RedisCacheExample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SessionController : ControllerBase
{
    private readonly SessionService _sessionService;

	public SessionController(SessionService sessionService)
	{
		_sessionService = sessionService;
	}

	[HttpGet("get/{key}")]
	public IActionResult GetSessionValue(string key)
	{
		var value = _sessionService.GetSessionValue(key);

		if (value == null)
		{
			return NotFound();
		}

		return Ok(value);
	}

	[HttpPost("set")]
	public IActionResult SetSessionValue([FromBody] KeyValuePair<string, string> kvp)
	{
		_sessionService.SetSessionValue(kvp.Key, kvp.Value);
		return Ok(new
		{
			isConfigured = true,
			message = $"Sesstion with {kvp.Key} was set!",
		});
	}
}
