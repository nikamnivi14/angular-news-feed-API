using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using NewsFeedAPI.Data;
using NewsFeedAPI.Data.IData;
using NewsFeedAPI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace NewsFeeds.Tests.Business
{
    [TestClass]
    public class NewsFeedDataTest
    {
        private Mock<INewsFeedData> mock_NewsData;
        private NewsFeedData newsFeedDataManager;



        [TestMethod]
        public async Task Test_GetNewsFeedList_Success()
        {
            try
            {
                List<Feeds> responseList = new List<Feeds>();
                Feeds feeds = new Feeds();
                using (StreamReader r = new StreamReader("Mock/TopNews.json"))
                {
                    string json = r.ReadToEnd();
                    responseList = JsonConvert.DeserializeObject<List<Feeds>>(json);

                }

                using (StreamReader r = new StreamReader("Mock/Feeds.json"))
                {
                    string json = r.ReadToEnd();
                    feeds = JsonConvert.DeserializeObject<Feeds>(json);

                }
                var mockFactory = new Mock<IHttpClientFactory>();
                var configuration = new HttpConfiguration();

                var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) =>
                {
                    request.SetConfiguration(configuration);
                    var response = request.CreateResponse(HttpStatusCode.OK, responseList);
                    return Task.FromResult(response);
                });


                var client = new HttpClient(clientHandlerStub);

                mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

                IHttpClientFactory factory = mockFactory.Object;

                var mockConfiguration = new Mock<IConfiguration>();
                var mockConfSection = new Mock<IConfigurationSection>();
                mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "BaseUrl")]).Returns("https://Mock");

                mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfSection.Object);

                mock_NewsData = new Mock<INewsFeedData>();

                mock_NewsData.Setup(x => x.GetNewsFeedDataAsync(It.IsAny<IEnumerable<string>>())).Returns(Task.FromResult(responseList));
                mock_NewsData.Setup(x => x.GetNewsFeedDetails(It.IsAny<string>())).Returns(Task.FromResult(feeds));
                newsFeedDataManager = new NewsFeedData(mockFactory.Object, mockConfiguration.Object);
                var response = await newsFeedDataManager.GetNewsFeedList("Top");
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Count > 0);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [TestMethod]
        public async Task Test_GetNewsFeedList_failure()
        {
            try
            {
                List<Feeds> responseList = new List<Feeds>();
                Feeds feeds = new Feeds();
                using (StreamReader r = new StreamReader("Mock/TopNews.json"))
                {
                    string json = r.ReadToEnd();
                    responseList = JsonConvert.DeserializeObject<List<Feeds>>(json);

                }

                using (StreamReader r = new StreamReader("Mock/Feeds.json"))
                {
                    string json = r.ReadToEnd();
                    feeds = JsonConvert.DeserializeObject<Feeds>(json);

                }
                var mockFactory = new Mock<IHttpClientFactory>();
                var configuration = new HttpConfiguration();

                var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) =>
                {
                    request.SetConfiguration(configuration);
                    var response = request.CreateResponse(HttpStatusCode.OK, responseList);
                    return Task.FromResult(response);
                });


                var client = new HttpClient(clientHandlerStub);

                mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

                IHttpClientFactory factory = mockFactory.Object;

                var mockConfiguration = new Mock<IConfiguration>();
                var mockConfSection = new Mock<IConfigurationSection>();
                mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "BaseUrl")]).Returns("https://Mock");

                mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfSection.Object);

                mock_NewsData = new Mock<INewsFeedData>();

                mock_NewsData.Setup(x => x.GetNewsFeedDataAsync(It.IsAny<IEnumerable<string>>())).Throws<Exception>();
                mock_NewsData.Setup(x => x.GetNewsFeedDetails(It.IsAny<string>())).Returns(Task.FromResult(feeds));
                newsFeedDataManager = new NewsFeedData(mockFactory.Object, mockConfiguration.Object);
                var response = await newsFeedDataManager.GetNewsFeedList("Top");

            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                throw ex;
            }

        }


        [TestMethod]
        public async Task Test_GetLatestFeedList_Success()
        {
            try
            {
                List<Feeds> responseList = new List<Feeds>();
                Feeds feeds = new Feeds();
                using (StreamReader r = new StreamReader("Mock/TopNews.json"))
                {
                    string json = r.ReadToEnd();
                    responseList = JsonConvert.DeserializeObject<List<Feeds>>(json);

                }

                using (StreamReader r = new StreamReader("Mock/Feeds.json"))
                {
                    string json = r.ReadToEnd();
                    feeds = JsonConvert.DeserializeObject<Feeds>(json);

                }
                var mockFactory = new Mock<IHttpClientFactory>();
                var configuration = new HttpConfiguration();

                var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) =>
                {
                    request.SetConfiguration(configuration);
                    var response = request.CreateResponse(HttpStatusCode.OK, responseList);
                    return Task.FromResult(response);
                });


                var client = new HttpClient(clientHandlerStub);

                mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

                IHttpClientFactory factory = mockFactory.Object;

                var mockConfiguration = new Mock<IConfiguration>();
                var mockConfSection = new Mock<IConfigurationSection>();
                mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "BaseUrl")]).Returns("https://Mock");

                mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfSection.Object);

                mock_NewsData = new Mock<INewsFeedData>();

                mock_NewsData.Setup(x => x.GetNewsFeedDataAsync(It.IsAny<IEnumerable<string>>())).Returns(Task.FromResult(responseList));
                mock_NewsData.Setup(x => x.GetNewsFeedDetails(It.IsAny<string>())).Returns(Task.FromResult(feeds));
                newsFeedDataManager = new NewsFeedData(mockFactory.Object, mockConfiguration.Object);
                var response = await newsFeedDataManager.GetNewsFeedList("Latest");
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Count > 0);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [TestMethod]
        public async Task Test_GetLatestFeedList_failure()
        {
            try
            {
                List<Feeds> responseList = new List<Feeds>();
                Feeds feeds = new Feeds();
                using (StreamReader r = new StreamReader("Mock/TopNews.json"))
                {
                    string json = r.ReadToEnd();
                    responseList = JsonConvert.DeserializeObject<List<Feeds>>(json);

                }

                using (StreamReader r = new StreamReader("Mock/Feeds.json"))
                {
                    string json = r.ReadToEnd();
                    feeds = JsonConvert.DeserializeObject<Feeds>(json);

                }
                var mockFactory = new Mock<IHttpClientFactory>();
                var configuration = new HttpConfiguration();

                var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) =>
                {
                    request.SetConfiguration(configuration);
                    var response = request.CreateResponse(HttpStatusCode.OK, responseList);
                    return Task.FromResult(response);
                });


                var client = new HttpClient(clientHandlerStub);

                mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

                IHttpClientFactory factory = mockFactory.Object;

                var mockConfiguration = new Mock<IConfiguration>();
                var mockConfSection = new Mock<IConfigurationSection>();
                mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "BaseUrl")]).Returns("https://Mock");

                mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfSection.Object);

                mock_NewsData = new Mock<INewsFeedData>();

                mock_NewsData.Setup(x => x.GetNewsFeedDataAsync(It.IsAny<IEnumerable<string>>())).Throws<Exception>();
                mock_NewsData.Setup(x => x.GetNewsFeedDetails(It.IsAny<string>())).Returns(Task.FromResult(feeds));
                newsFeedDataManager = new NewsFeedData(mockFactory.Object, mockConfiguration.Object);
                var response = await newsFeedDataManager.GetNewsFeedList("Latest");

            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                throw ex;
            }

        }

        public class DelegatingHandlerStub : DelegatingHandler
        {
            private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;
            public DelegatingHandlerStub()
            {
                _handlerFunc = (request, cancellationToken) => Task.FromResult(request.CreateResponse(HttpStatusCode.OK));
            }

            public DelegatingHandlerStub(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
            {
                _handlerFunc = handlerFunc;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return _handlerFunc(request, cancellationToken);
            }
        }
    }
}
