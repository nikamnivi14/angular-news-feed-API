using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NewsFeedAPI.Common;
using NewsFeedAPI.Filter;
using NewsFeedAPI.Model;
using NewsFeedAPI.Service.IService;
using NewsFeedAPI.Wrappers;

namespace NewsFeedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsFeedController : ControllerBase
    {
        private readonly INewsFeedService service;

        private readonly IMemoryCache _cache;

        public NewsFeedController(INewsFeedService service, IMemoryCache memoryCache)
        {
            this.service = service;
            _cache = memoryCache;

        }


        [HttpGet("getNewsFeedList")]
        [ProducesResponseType(typeof(ServiceResponse<List<Feeds>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Feeds>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNewsFeedList([FromQuery] PaginationFilter filter)
        {
             

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageRecords);
            double totalRecords = 0;


            if (!_cache.TryGetValue(CacheKeys.newsList, out List<Feeds> newsList))
            {

                try
                {
                    newsList = await service.GetNewsFeedList("Top");

                    totalRecords = Math.Round(Convert.ToDouble(newsList.Count) / 10);

                    //add cdata to caching
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddSeconds(120),
                        SlidingExpiration = TimeSpan.FromSeconds(20),
                        Size = 1024,
                        Priority = CacheItemPriority.High,

                    };
                    _cache.Set(CacheKeys.newsList, newsList, cacheEntryOptions);

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            var newsData = newsList.Skip((validFilter.PageNumber -1) * validFilter.PageRecords).Take(validFilter.PageRecords).ToList();
            totalRecords = Math.Round(Convert.ToDouble(newsList.Count) / 10);
            return Ok(new PagedResponse<List<Feeds>>(newsData, validFilter.PageNumber, validFilter.PageRecords, totalRecords));

        }

        [HttpGet("getLatestFeedList")]
        [ProducesResponseType(typeof(ServiceResponse<List<Feeds>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Feeds>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLatestFeedList([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageRecords);
            double totalRecords = 0;
             
             
            if (!_cache.TryGetValue(CacheKeys.topnewsList, out List<Feeds> newsList))
            {

                try
                {
                    newsList = await service.GetNewsFeedList("New");

                    totalRecords = Math.Round(Convert.ToDouble(newsList.Count) / 10);

                    //add cdata to caching
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddSeconds(120),
                        SlidingExpiration = TimeSpan.FromSeconds(20),
                        Size = 1024,
                        Priority = CacheItemPriority.High,

                    };
                    _cache.Set(CacheKeys.topnewsList, newsList, cacheEntryOptions);
 
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            var newsData = newsList.Skip((validFilter.PageNumber - 1) * validFilter.PageRecords).Take(validFilter.PageRecords).ToList();
            totalRecords = Math.Round(Convert.ToDouble(newsList.Count) / 10);
            return Ok(new PagedResponse<List<Feeds>>(newsData, validFilter.PageNumber, validFilter.PageRecords, totalRecords));

        }
    }
}
