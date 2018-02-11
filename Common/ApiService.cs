using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Common
{
    public class ApiService : IApiService
    {
        private readonly ILogService log;
        private ApiServiceConfig config;
        public string EntryPoint { get; set; }

        public ApiService(ILogService logService)
        {
            this.log = logService.Init(GetType(), "API");
        }

        public void Config(ApiServiceConfig config)
        {
            this.config = config;
        }

        public bool Ping()
        {
            var activeEntryPoints = new List<string>();
            var client = new WebClient();
            foreach (var entryPoint in config.EntryPoints)
            {
                try
                {
                    var result = client.DownloadString(entryPoint + "/Hello");
                    if(result == "OK")
                        activeEntryPoints.Add(entryPoint);
                }
                catch (Exception e)
                {
                    log.Debug($"Can't connect to <a href='{entryPoint}/Hello'>{entryPoint}/Hello</a> : " + e.Message);
                }
            }

            if (!activeEntryPoints.Any())
            {
                log.Warn($"Can't connect to API : <b>{config.ApiName}</b> used: {config.EntryPoints.StringJoin()}");
                return false;
            }

            EntryPoint = activeEntryPoints.First();
            log.Info($"ApiAdapter {config.ApiName} will use <a href='{EntryPoint}'>{EntryPoint}</a>");
            return true;
        }

        public string Request(string apiPath, string postData)
        {
            var fullPath = EntryPoint + "/" + apiPath;
            log.Info($"Request to : <a href='{fullPath}'>{fullPath}</a>");
            log.Debug("Post data : " + postData.Crop(500));
            byte[] byteResult;
            using (var wc = new WebClient())
            {
                wc.Headers.Add("Content-Type", "application/json");
                var byteArray = Encoding.ASCII.GetBytes(postData);
                byteResult = wc.UploadData(fullPath, "POST", byteArray);
            }
            var response = Encoding.ASCII.GetString(byteResult);
            log.Debug("Response data : " + response.Crop(500));
            return response;
        }
    }

    public class ApiServiceConfig
    {
        public string ApiName { get; set; }
        public string[] EntryPoints { get; set; }
    }
}
