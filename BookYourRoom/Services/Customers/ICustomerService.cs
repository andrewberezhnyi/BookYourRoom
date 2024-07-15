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
        public IEnumerable<Customer> GetAllCustomers();

        public Customer? GetCustomerById(int customerId);

        public void CreateCustomer(Customer customer);

        public void UpdateCustomer(Customer customer);

        public void DeleteCustomer(int customerId);
    }
}
