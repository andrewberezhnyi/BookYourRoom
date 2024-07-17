using BookYourRoom.Data;
using BookYourRoom.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookYourRoom.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly HotelContext _context;

        public CustomerService(HotelContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _context.Customers.ToListAsync();

        }

        public async Task CreateCustomer(Customer customer)
        {
            bool isCustomerConflicts = await IsCustomerConflicts(customer);
            if (!isCustomerConflicts)
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("There is already a customer with the same email");
            }
        }

        public async Task UpdateCustomer(Customer newCustomer)
        {
            try
            {
                var existingCustomer = await _context.Customers.FindAsync(newCustomer.CustomerId);
                if (existingCustomer != null)
                {
                    bool isCustomerConflicts = await IsCustomerConflicts(newCustomer);
                    if (!isCustomerConflicts)
                    {
                        _context.Entry(existingCustomer).CurrentValues.SetValues(newCustomer);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new ArgumentException("There is already a customer with the same email");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Customer not found.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to update the customer.", ex);
            }
        }

        public async Task DeleteCustomer(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsCustomerConflicts(Customer customer)
        {
            if (_context.Customers.ToList().Count == 0) return false;
            var customersWithSameEmail = await _context.Customers.Where(c => c.Email == customer.Email).ToListAsync();
            return customersWithSameEmail.Any();
        }
    }
}
