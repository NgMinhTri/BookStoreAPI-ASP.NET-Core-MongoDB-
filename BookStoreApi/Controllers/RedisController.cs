using BookStoreApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ICacheService _cacheService;

        public RedisController(IDistributedCache distributedCache, ICacheService cacheService)
        {
            _distributedCache = distributedCache;
            _cacheService = cacheService;
        }

        [HttpGet]
        public string Get()
        {
            //var cacheKey = "TheTime";
            //var currentTime = DateTime.Now.ToString();
            //var cachedTime = _distributedCache.GetString(cacheKey);
            //if (string.IsNullOrEmpty(cachedTime))
            //{
            //    // cachedTime = "Expired";
            //    // Cache expire trong 5s
            //    var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(5));
            //    // Nạp lại giá trị mới cho cache
            //    _distributedCache.SetString(cacheKey, currentTime, options);
            //    cachedTime = _distributedCache.GetString(cacheKey);
            //}
            //var result = $"Current Time : {currentTime} \nCached  Time : {cachedTime}";
            //return result;

            var cacheKey = "TheTime";
            var currentTime = DateTime.Now.ToString();
            var cachedTime = _cacheService.Get<string>(cacheKey);
            if (string.IsNullOrEmpty(cachedTime))
            {
                // cachedTime = "Expired";
                // Cache expire trong 5s
                //var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(5));
                // Nạp lại giá trị mới cho cache
                 cachedTime = _cacheService.Set(cacheKey, currentTime);
            }
            var result = $"Current Time : {currentTime} \nCached  Time : {cachedTime}";
            return result;
        }
    }
}
