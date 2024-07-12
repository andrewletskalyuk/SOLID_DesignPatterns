using Microsoft.AspNetCore.Mvc;
using RedisCacheExample.Services;

namespace RedisCacheExample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LockDemoController : ControllerBase
{
    private readonly DistributedLockService _lockService;

    public LockDemoController(DistributedLockService lockService)
    {
        _lockService = lockService;
    }

    [HttpPost("lock/{resource}")]
    public async Task<IActionResult> AcquireLock(string resource)
    {
        var lockId = Guid.NewGuid().ToString();
        var acquired = await _lockService.AcquireLockAsync(resource, lockId);
        if (acquired)
        {
            // Simulate some work
            await Task.Delay(5000); // 5 seconds

            // Release the lock
            await _lockService.ReleaseLockAsync(resource, lockId);
            return Ok($"Lock acquired and released for resource: {resource} with lock ID: {lockId}");
        }

        return Conflict("Failed to acquire lock.");
    }

    [HttpPost("acquire")]
    public async Task<IActionResult> AcquireLock()
    {
        var lockKey = "test-lock";
        var lockValue = Guid.NewGuid().ToString();
        var expiry = TimeSpan.FromSeconds(30);

        var isLocked = await _lockService.AcquireLockAsync(lockKey, lockValue, expiry);

        if (isLocked)
        {
            return Ok(new { LockKey = lockKey, LockValue = lockValue });
        }
        else
        {
            return Conflict("Unable to acquire lock");
        }
    }

    [HttpPost("release")]
    public async Task<IActionResult> ReleaseLock([FromBody] ReleaseLockRequest request)
    {
        var isReleased = await _lockService.ReleaseLockAsync(request.LockKey, request.LockValue);

        if (isReleased)
        {
            return Ok("Lock released");
        }
        else
        {
            return Conflict("Unable to release lock");
        }
    }

    public class ReleaseLockRequest
    {
        public required string LockKey { get; set; }
        public required string LockValue { get; set; }
    }
}
