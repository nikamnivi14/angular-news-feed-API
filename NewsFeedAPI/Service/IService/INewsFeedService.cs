using NewsFeedAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsFeedAPI.Service.IService
{
    public interface INewsFeedService
    {
        Task<List<Feeds>> GetNewsFeedList(string type);

    }
}
