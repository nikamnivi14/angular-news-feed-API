using NewsFeedAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsFeedAPI.Data.IData
{
    public interface INewsFeedData
    {
        Task<List<Feeds>> GetNewsFeedList(string type);
        Task<Feeds> GetNewsFeedDetails(string FeedNumber);
        Task<List<Feeds>> GetNewsFeedDataAsync(IEnumerable<string> newsCodes);


    }
}
