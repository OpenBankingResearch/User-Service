using Microsoft.Extensions.Caching.Distributed;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace UserAPI.Cache
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDistributedCache distributedCache;

        public CacheRepository(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public async Task<object> GetAsync(string key)
        {
            var cachedValue = await distributedCache.GetAsync(key);
            if (cachedValue != null)
            {
                var value = ByteArrayToObject(cachedValue);
                return await Task.FromResult(value);
            }
            return null;
        }

        public async Task SetAsync(string key, object value)
        {
            await distributedCache.SetAsync(key, ObjectToByteArray(value));
        }

        private static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        private static Object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }
    }
}
