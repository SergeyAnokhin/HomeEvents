
namespace Common
{
    public interface IAppConfigService : IService
    {
        T GetModuleConfig<T>();
    }
}
