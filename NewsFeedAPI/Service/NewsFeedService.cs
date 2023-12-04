using NewsFeedAPI.Data.IData;
using NewsFeedAPI.Model;
using NewsFeedAPI.Service.IService;
using System.Collections.Generic;
using System.Threading.Tasks;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NewsFeedAPI.Service
{
    public class NewsFeedService : INewsFeedService
    {
        private readonly INewsFeedData data;
        public NewsFeedService(INewsFeedData ldata)
        {
            data = ldata;

        }
        public async Task<List<Feeds>> GetNewsFeedList(string type)
        {
            return await data.GetNewsFeedList(type);
        }
    }
}
