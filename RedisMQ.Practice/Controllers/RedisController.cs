using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisMQ.Practice.Services;

namespace RedisMQ.Practice.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RedisController : ControllerBase
{
    private readonly RedisService _redisService;

	public RedisController(RedisService redisService)
	{
		_redisService = redisService;
	}

    [HttpGet("{key}")]
    public async Task<IActionResult> Get(string key)
    {
        var value = await _redisService.GetValueAsync(key);
        if (value == null)
        {
            return NotFound();
        }
        return Ok(value);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] KeyValuePair<string, string> kvp)
    {
        await _redisService.SetValueAsync(kvp.Key, kvp.Value);
        return Ok();
    }


}
