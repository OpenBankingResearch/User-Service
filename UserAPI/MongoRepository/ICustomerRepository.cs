using CustomerAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerAPI.MongoRepository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomers();

        Task<Customer> GetCustomer(string id);

        Task AddCustomer(Customer Customer);

        Task<bool> RemoveCustomer(string id);

        Task<bool> UpdateCustomer(string id, string body);

        Task<bool> RemoveAllCustomers();
    }
}
