using Microsoft.Extensions.Configuration;
using NewsFeedAPI.Data.IData;
using NewsFeedAPI.Model;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewsFeedAPI.Data
{
    public class NewsFeedData : INewsFeedData
    {
        private readonly IHttpClientFactory _httpClientFactory;
        // private HttpClient _client;
        private IConfiguration configuration;
        public NewsFeedData(IHttpClientFactory httpClientFactory, IConfiguration _configuration)
        {
            _httpClientFactory = httpClientFactory;
            configuration = _configuration;
        }

        public async Task<List<Feeds>> GetNewsFeedList(string type)
        {
            string baseUrl = configuration.GetConnectionString("BaseUrl").ToString();
            List<Feeds> obj = new List<Feeds>();
            try
            {
                HttpRequestMessage request;
                if (type == "Top")
                {
                    request = new HttpRequestMessage(HttpMethod.Get, baseUrl + "topstories.json?print=pretty");
                }
                else
                {
                    request = new HttpRequestMessage(HttpMethod.Get, baseUrl + "newstories.json?print=pretty");
                }

                var client = _httpClientFactory.CreateClient();

                var response = await client.SendAsync(request);
                // To read the response as string
                var responseString = await response.Content.ReadAsStringAsync();
                List<string> headerList = responseString.Split('[', ']')[1].Split(",").ToList();
                obj = await GetNewsFeedDataAsync(headerList.Take(200));

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return obj;

        }

        public async Task<List<Feeds>> GetNewsFeedDataAsync(IEnumerable<string> newsCodes)
        {
            List<Feeds> newsFeedList = new List<Feeds>();
            try
            {
                var uniqueNewsCodes = newsCodes.GroupBy(code => code).Select(g => g.FirstOrDefault());

                //Create tasks to get inventory for the product codes.
                var newsCodeTasks = uniqueNewsCodes.Select(codes =>
                    GetNewsFeedDetails(codes)
                );
                //wait for all the tasks to complete
                var newsFeedDetails = await Task.WhenAll(newsCodeTasks);
                var data = JsonSerializer.Serialize(newsFeedDetails);
                //grab all items returned from the async tasks 
                foreach (var item in newsFeedDetails)
                {
                    if (item.url != "")
                    {
                        Feeds obj = new Feeds();
                        obj.by = item?.by;
                        obj.id = item.id;
                        obj.score = item.score;
                        obj.time = item.time;
                        obj.type = item?.type;
                        obj.title = item?.title;
                        obj.url = item?.url;
                        newsFeedList.Add(obj);
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


            return newsFeedList;
        }

        public async Task<Feeds> GetNewsFeedDetails(string FeedNumber)
        {

            string baseUrl = configuration.GetConnectionString("BaseUrl").ToString();
            Feeds obj = new Feeds();
            try
            {

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(baseUrl + "item/" + FeedNumber.ToString().Trim() + ".json?print=pretty"),
                    Method = HttpMethod.Get

                };

                var client = _httpClientFactory.CreateClient();
                var response = await client.SendAsync(request);
                // To read the response as string

                if (baseUrl == "https://Mock")
                {
                    using (StreamReader r = new StreamReader("Mock/Feeds.json"))
                    {
                        string json = r.ReadToEnd();
                        obj = JsonSerializer.Deserialize<Feeds>(json);
                    }

                }
                else
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    obj = JsonSerializer.Deserialize<Feeds>(responseString);
                }




            }
            catch (Exception ex)
            {

                throw ex;
            }

            return obj;

        }

    }


}
