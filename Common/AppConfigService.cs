using System;
using System.IO;
using System.Linq;
using Interfaces;
using Newtonsoft.Json;

namespace Common
{
    public class AppConfigService : IAppConfigService
    {
        public T GetModuleConfig<T>()
        {
            var name = typeof(T).Assembly.GetName().Name;

            var directory = new DirectoryInfo(Environment.CurrentDirectory);

            var pattern = $"{name}.config.json";

            var files = directory.GetFiles(pattern);
            if(!files.Any())
                throw new FileNotFoundException("Files not found : " + pattern);

            var file = files.First();

            var body = File.ReadAllText(file.FullName);

            T config = JsonConvert.DeserializeObject<T>(body);

            return config;
        }
    }
}
