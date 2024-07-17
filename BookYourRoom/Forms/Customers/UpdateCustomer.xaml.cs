using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookYourRoom.Models;
using BookYourRoom.Services.Customers;

namespace BookYourRoom.Forms.Customers
{
    /// <summary>
    /// Interaction logic for UpdateCustomer.xaml
    /// </summary>
    public partial class UpdateCustomer : Window
    {
        private readonly Customer _customer;
        private readonly ICustomerService _customerService;

        public UpdateCustomer(Customer customer, ICustomerService customerService)
        {
            InitializeComponent();
            _customer = customer;
            _customerService = customerService;

            FirstNameTextBox.Text = customer.FirstName;
            LastNameTextBox.Text = customer.LastName;
            EmailTextBox.Text = customer.Email;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string email = EmailTextBox.Text;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please fill out all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string? validationError = ValidateCustomerData(firstName, lastName, email);
            if (validationError != null)
            {
                MessageBox.Show(validationError, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                await _customerService.UpdateCustomer(new Customer() { CustomerId = _customer.CustomerId, FirstName = firstName, LastName = lastName, Email = email });
                MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured while updating customer: {ex.Message}");
            }


            this.Close();
        }

        private string? ValidateCustomerData(string firstName, string lastName, string email)
        {
            if (firstName.Length < 2) return "Hotel name should be at least 2 symbols long";
            if (lastName.Length < 2) return "Hotel address should be at least 2 symbols long";
            if (!email.Contains("@") || !email.Contains(".")) return "Wrong email format";
            return null;
        }
    }
}
