using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Common.Config
{
    public class AppConfigService : IAppConfigService
    {
        private ILogService log;
        private PrivateConfig privateConfig;
        private Dictionary<string, object> privateProperties = new Dictionary<string, object>();

        public AppConfigService(ILogService log)
        {
            this.log = log.Init(GetType(), "CONFIG");

            privateConfig = GetConfig<PrivateConfig>("PrivateConfig", true);
            if(privateConfig != null)
            {
                foreach(var property in privateConfig.GetType().GetProperties())
                {
                    privateProperties[property.Name] = property.GetMethod.Invoke(privateConfig, null);
                }
            }
            else
            {
                log.Warn("Private config not found. OverrideIfExist will not working. create PrivateConfig.config.json for overrided properties");
            }
        }

        public T GetModuleConfig<T>()
        {
            var name = typeof(T).Assembly.GetName().Name;
            return GetConfig<T>(name);
        }

        private T GetConfig<T>(string filename, bool ignoreIfNotExist = false)
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
            ApplyPrivateConfig(config, file.FullName);

            return config;

        }

        private void ApplyPrivateConfig(object config, string fileName)
        {
            var typeOfConfig = config.GetType();
            foreach (var property in typeOfConfig.GetProperties())
            {
                var attrs = property.GetCustomAttributes(typeof(CanPrivateOverride), false);
                if (property.PropertyType.Assembly == typeOfConfig.Assembly)
                {
                    var subConfig = property.GetMethod.Invoke(config, null);
                    if (subConfig == null)
                    {
                        log.Warn($"Can't found in <a>{fileName}</a> config : <b>{property.GetMethod}</b>");
                    }else
                        ApplyPrivateConfig(subConfig, fileName);
                }
                if (!attrs.Any()) continue;

                var attr = attrs.Cast<CanPrivateOverride>().First();
                var name = attr.PropertyName;
                if (!privateProperties.ContainsKey(name)) {
                    log.Warn($"Value nor found in private config : {name}");
                    continue;
                };
                var overrideValue = privateProperties[name];

                property.SetMethod.Invoke(config, new[] { overrideValue });
            }
        }
    }
}
