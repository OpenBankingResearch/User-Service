using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAPI.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CustomerAPI.MongoRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext CustomerContext;

        public CustomerRepository(IOptions<Settings> options)
        {
            CustomerContext = new CustomerContext(options);
        }

        public Task AddCustomer(Customer Customer)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await CustomerContext.Customer.Find(x => true).ToListAsync();
        }

        public async Task<Customer> GetCustomer(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;
            var filter = Builders<Customer>.Filter.Eq(s => s._id, internalId);
            var result = await CustomerContext.Customer.FindAsync<Customer>(filter);
            return await result.FirstOrDefaultAsync(); ;
        }

        public Task<bool> RemoveAllCustomers()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveCustomer(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCustomer(string id, string body)
        {
            throw new NotImplementedException();
        }
    }
}
