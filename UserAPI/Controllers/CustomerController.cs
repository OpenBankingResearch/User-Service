using System;
using CustomerAPI.Model;
using CustomerAPI.MongoRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Polly;
using UserAPI.Cache;
using UserAPI.Audit;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;

        private readonly ICacheRepository cacheRepository;

        private readonly IAuditHandler auditHandler;

        static Policy policy = Policy.Handle<Exception>().CircuitBreakerAsync(2, TimeSpan.FromSeconds(40));

        public CustomerController(ICustomerRepository customerRepository, ICacheRepository cacheRepository, IAuditHandler auditHandler)
        {
            this.customerRepository = customerRepository;
            this.cacheRepository = cacheRepository;
            this.auditHandler = auditHandler;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await policy.ExecuteAsync(() => customerRepository.GetAllCustomers()); ;
        }

        [HttpGet]
        public async Task<Customer> Get(string id)
        {
            var cacheCustomer = await cacheRepository.GetAsync(id);
            if (cacheCustomer == null)
            {
                var customer = await customerRepository.GetCustomer(id);
                await policy.ExecuteAsync(() => cacheRepository.SetAsync(id, customer));
                GenerateAuditEvent(id, 1, "Return customer from database");
                return customer;
            }
            GenerateAuditEvent(id, 2, "Return customer from cache");
            return cacheRepository as Customer;
        }

        private void GenerateAuditEvent(string id, int descriptionId, string description)
        {
            var audit = new Audit
            {
                category = "System call audit",
                description = description,
                fullyQualifiedClassName = "CustomerController",
                descriptionId = descriptionId.ToString(),
                id = id,
                methodName = "Get",
                severity = "Information",
                timeStamp = DateTime.Now.ToString(),
                user = id
            };
            auditHandler.Post(audit);
        }
    }
}
