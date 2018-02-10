namespace Common
{
    public interface IApiService : IService
    {
        void Config(ApiServiceConfig config);
        bool Ping();
        string Request(string apiPath, string postData);
        string EntryPoint { get; set; }
    }
}