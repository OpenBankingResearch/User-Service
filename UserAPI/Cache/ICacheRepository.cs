using System.Threading.Tasks;

namespace UserAPI.Cache
{
    public interface ICacheRepository
    {
        Task<object> GetAsync(string key);

        Task SetAsync(string key, object value);
    }
}
