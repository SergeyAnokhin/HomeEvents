using System.Collections.Generic;
using Common;

namespace CommonTests.Mocks
{
    public class ApiServiceMock : IApiService
    {
        public Dictionary<string, Stack<string>> MockResponseData { get; set; }
        public Dictionary<string, Stack<string>> MockPostData { get; set; }

        public bool Ping()
        {
            return true;
        }

        public string Request(string apiPath, string postData)
        {
            if (MockPostData[apiPath] == null)
            {
                MockPostData[apiPath] = new Stack<string>();
            }
            MockPostData[apiPath].Push(postData);
            return MockResponseData[apiPath].Pop();
        }
    }
}
