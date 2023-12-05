# News-feed-API
This is the github repository for API created for news feed .net Core 3.1

# Project Details
1. I have created two Projects i.e Main Project and Test Project
   # Main Project Details
   . Created new controller i.e NewsFeedController which is having dependancy injection implemented and added two method i.e GetLatestFeedList and GetNewsFeedList
   . For GetLatestFeedList I have implemented caching to cache the records for 50 seconds.
   . Created an interface service i.e INewsFeedService which will have our main method GetNewsFeedList to get the list of News.
   . Created another interface for Data Layer i.e INewsFeedData and Class NewsFeedData which is implementing the interface method having a http call to Hacker News API.

   # Test Project
   . Created Test Project which will have controller test cases and Data Layer test Cases.
   . In the Test Project I have mocked the IHttpClientFactory and IConfigurationSection to use the mock object to make a http call.
   
