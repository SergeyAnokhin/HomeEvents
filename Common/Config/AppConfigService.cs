using System;
using System.IO;
using System.Linq;
using Common.Config;
using Newtonsoft.Json;

namespace Common
{
    public class AppConfigService : IAppConfigService
    {
        private ILogService log;
        private PrivateConfig privateConfig;

        public AppConfigService(ILogService log)
        {
            this.log = log.Init(GetType());

            privateConfig = GetConfig<Config.PrivateConfig>("PrivateConfig", true);
        }

        public T GetModuleConfig<T>()
        {
            var name = typeof(T).Assembly.GetName().Name;
            return GetConfig<T>(name);
        }

        private static T GetConfig<T>(string filename, bool ignoreIfNotExist = false)
        {
            var directory = new DirectoryInfo(Environment.CurrentDirectory);
            var pattern = $"{filename}.config.json";

            var files = directory.GetFiles(pattern);
            if (!files.Any())
            {
                if (ignoreIfNotExist) return default(T);
                throw new FileNotFoundException("Files not found : " + pattern);
            }

            var file = files.First();
            var body = File.ReadAllText(file.FullName);

            var config = JsonConvert.DeserializeObject<T>(body);
            ApplyPrivateConfig(config);

            return config;

        }

        private static void ApplyPrivateConfig<T>(T config)
        {
            foreach(var property in typeof(T).GetProperties())
            {
                var attr = property.GetCustomAttributes(typeof(OverrideIfExistAttribute), false);
            }
        }
    }
}
