
namespace Common.Config
{
    public interface IAppConfigService : IService
    {
        T GetModuleConfig<T>();
    }
}
