using System.Collections.Generic;
using System.IO;
using Common;

namespace CommonTests.Mocks
{
    public class ApiServiceMock : IApiService
    {
        public Dictionary<string, Stack<string>> MockResponseData { get; set; }
        public Dictionary<string, Stack<string>> MockPostData { get; set; }

        public ApiServiceMock()
        {
            MockPostData = new Dictionary<string, Stack<string>>();
            MockResponseData = new Dictionary<string, Stack<string>>();
        }

        public void Config(ApiServiceConfig config)
        {}

        public bool Ping()
        {
            return true;
        }

        public string Request(string apiPath, string postData)
        {
            if (!MockPostData.ContainsKey(apiPath))
            {
                MockPostData[apiPath] = new Stack<string>();
            }
            MockPostData[apiPath].Push(postData);
            var file = MockResponseData[apiPath].Pop();
            return File.ReadAllText(file);

        }

        public string EntryPoint { get; set; }
    }
}
