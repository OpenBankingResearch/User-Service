using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using CustomerAPI.Model;
using CustomerAPI.MongoRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Polly;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository CustomerRepository;

        private readonly IDistributedCache DistributedCache;

        static Policy policy = Policy.Handle<Exception>().CircuitBreakerAsync(2, TimeSpan.FromSeconds(40), onBreak: (ex ,time) => AuditHandler.LogAudit("AuditOnBreak", "Data Source not found"), onReset: () => AuditHandler.LogAudit("AuditOnReset", "Circuit Closed"));

        public CustomerController(ICustomerRepository CustomerRepository, IDistributedCache DistributedCache)
        {
            this.CustomerRepository = CustomerRepository;
            this.DistributedCache = DistributedCache;
            
        }
        
        [HttpGet]
        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await policy.ExecuteAsync(() => CustomerRepository.GetAllCustomers()); ;
        }

        [HttpGet]
        public async Task<Customer> Get(string id)
        {
            var cacheKey = id;
            var cachedValue = await DistributedCache.GetAsync(cacheKey);
            if (cachedValue != null)
            {
                return ByteArrayToObject(cachedValue) as Customer; 
            }
            else
            {
                var val  = await CustomerRepository.GetCustomer(id);
                await DistributedCache.SetAsync(cacheKey, ObjectToByteArray(val));
                return val;
            }
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
