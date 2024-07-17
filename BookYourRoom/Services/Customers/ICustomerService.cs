using BookYourRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookYourRoom.Services.Customers
{
    public interface ICustomerService
    {
        public Task<IEnumerable<Customer>> GetAllCustomers();

        public Task<Customer?> GetCustomerById(int customerId);

        public Task CreateCustomer(Customer customer);

        public Task UpdateCustomer(Customer newCustomer);

        public Task DeleteCustomer(int customerId);
    }
}
