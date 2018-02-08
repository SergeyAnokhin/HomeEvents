using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Common
{
    class ApiService : IApiService
    {
        private readonly ILogService log;
        private readonly ApiServiceConfig config;
        private string entryPoint = string.Empty;

        public ApiService(ILogService logService, ApiServiceConfig config)
        {
            this.log = logService.Init(GetType());
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
                    log.Debug($"Can't connect to {entryPoint}/Hello : " + e.Message);
                }
            }

            if (!activeEntryPoints.Any())
            {
                log.Warn($"Can't connect to API : {config.ApiName} used: {config.EntryPoints.StringJoin()}");
                return false;
            }

            log.Info($"ApiAdapter {config.ApiName} will use {entryPoint}");
            entryPoint = activeEntryPoints.First();
            return true;
        }

        public string Request(string apiPath, string postData)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ApiServiceConfig
    {
        public string ApiName { get; set; }
        public string[] EntryPoints { get; set; }
    }
}
