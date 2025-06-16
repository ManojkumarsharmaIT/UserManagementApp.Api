using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using UserManagementApp.Application.Interface;

namespace UserManagementApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalVendorController : ControllerBase
    {
        private readonly IReqresApplication _reqresApplication;
        private readonly IMemoryCache _cache;

        public ExternalVendorController(IReqresApplication reqresApplication, IMemoryCache cache)
        {
            _reqresApplication = reqresApplication;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByIdAsync(int userId)
        {
            string cacheKey = $"User_{userId}";
            if (!_cache.TryGetValue(cacheKey, out var result))
            {
                try
                {
                    result = await _reqresApplication.GetUserByIdAsync(userId);

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(1)); 

                    _cache.Set(cacheKey, result, cacheEntryOptions);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(new { Message = ex.Message });
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(new { Message = ex.Message });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        Message = "An unexpected error occurred.",
                        Details = ex.Message
                    });
                }
            }

            return Ok(result);
        }

        [HttpGet("page")]
        public async Task<IActionResult> GetAllUser(int page)
        {
            string cacheKey = $"Users_Page_{page}";
            if (!_cache.TryGetValue(cacheKey, out var result))
            {
                try
                {
                    result = await _reqresApplication.GetAllUsersAsync(page);

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                    _cache.Set(cacheKey, result, cacheEntryOptions);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    return BadRequest(new { Message = ex.Message });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        Message = "An unexpected error occurred.",
                        Details = ex.Message
                    });
                }
            }

            return Ok(result);
        }
    }
}
