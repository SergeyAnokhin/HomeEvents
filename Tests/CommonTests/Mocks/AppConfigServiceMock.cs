using System.Collections.Generic;
using System.Linq;
using Common.Config;

namespace CommonTests.Mocks
{
    public class AppConfigServiceMock : IAppConfigService
    {
        public List<object> Configs { get; set; }

        public AppConfigServiceMock()
        {
            Configs = new List<object>();
        }

        public T GetModuleConfig<T>()
        {
            return Configs.OfType<T>().First();
        }
    }
}
