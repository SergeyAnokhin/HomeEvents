namespace Common
{
    public interface IApiService
    {
        bool Ping();
        string Request(string apiPath, string postData);
    }
}