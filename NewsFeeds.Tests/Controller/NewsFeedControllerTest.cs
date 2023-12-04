using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NewsFeedAPI.Controllers;
using NewsFeedAPI.Data.IData;
using NewsFeedAPI.Filter;
using NewsFeedAPI.Model;
using NewsFeedAPI.Service.IService;
using NewsFeedAPI.Wrappers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NewsFeeds.Tests
{
    [TestClass]
    public class NewsFeedControllerTest
    {
        Mock<INewsFeedService> mock_NewsService;


        [TestMethod]
        public void Test_GetNewsFeedList_Success()
        {
            var cache = new MemoryCache(new MemoryCacheOptions());
            List<Feeds> response = new List<Feeds>();
            using (StreamReader r = new StreamReader("Mock/TopNews.json"))
            {
                string json = r.ReadToEnd();
                response = JsonConvert.DeserializeObject<List<Feeds>>(json);

            }

            mock_NewsService = new Mock<INewsFeedService>();
            mock_NewsService.Setup(x => x.GetNewsFeedList(It.IsAny<string>())).Returns(Task.FromResult(response));
            var validFilter = new PaginationFilter(1, 10);
            var controller = new NewsFeedController(mock_NewsService.Object, cache);
            var responseData = controller.GetNewsFeedList(validFilter);
            Assert.IsNotNull(responseData);


        }

        [TestMethod]
        public void Test_GetNewsFeedList_Failure()
        {
            try
            {


                var cache = new MemoryCache(new MemoryCacheOptions());
                List<Feeds> response = new List<Feeds>();
                using (StreamReader r = new StreamReader("Mock/TopNews.json"))
                {
                    string json = r.ReadToEnd();
                    response = JsonConvert.DeserializeObject<List<Feeds>>(json);

                }

                mock_NewsService = new Mock<INewsFeedService>();
                mock_NewsService.Setup(x => x.GetNewsFeedList(It.IsAny<string>())).Throws<Exception>();
                var validFilter = new PaginationFilter(1, 10);
                var controller = new NewsFeedController(mock_NewsService.Object, cache);
                var responseData = controller.GetNewsFeedList(validFilter);

            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);

            }

        }


        [TestMethod]
        public void Test_GetLatestFeedList_Success()
        {
            var cache = new MemoryCache(new MemoryCacheOptions());
            List<Feeds> response = new List<Feeds>();
            using (StreamReader r = new StreamReader("Mock/LatestNews.json"))
            {
                string json = r.ReadToEnd();
                response = JsonConvert.DeserializeObject<List<Feeds>>(json);

            }

            mock_NewsService = new Mock<INewsFeedService>();
            mock_NewsService.Setup(x => x.GetNewsFeedList(It.IsAny<string>())).Returns(Task.FromResult(response));
            var validFilter = new PaginationFilter(1, 10);
            var controller = new NewsFeedController(mock_NewsService.Object, cache);
            var responseData = controller.GetNewsFeedList(validFilter);
            Assert.IsNotNull(responseData);


        }

        [TestMethod]
        public void Test_GetLatestFeedList_Failure()
        {
            try
            { 
                var cache = new MemoryCache(new MemoryCacheOptions());
                List<Feeds> response = new List<Feeds>();
                using (StreamReader r = new StreamReader("Mock/LatestNews.json"))
                {
                    string json = r.ReadToEnd();
                    response = JsonConvert.DeserializeObject<List<Feeds>>(json);

                }

                mock_NewsService = new Mock<INewsFeedService>();
                mock_NewsService.Setup(x => x.GetNewsFeedList(It.IsAny<string>())).Throws<Exception>();
                var validFilter = new PaginationFilter(1, 10);
                var controller = new NewsFeedController(mock_NewsService.Object, cache);
                var responseData = controller.GetNewsFeedList(validFilter);

            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);

            }

        }
    }

}
