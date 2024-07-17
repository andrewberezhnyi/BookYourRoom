using BookYourRoom.Models;
using BookYourRoom.Services.Customers;
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

namespace BookYourRoom.Forms.Customers
{
    /// <summary>
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        private readonly ICustomerService _customerService;

        public AddCustomer(ICustomerService customerService)
        {
            _customerService = customerService;
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
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
                await _customerService.CreateCustomer(new Customer() { FirstName = firstName, LastName = lastName, Email = email });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured while creating customer: {ex.Message}");
                return;
            }

            MessageBox.Show("Customer added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private string? ValidateCustomerData(string firstName, string lastName, string email)
        {
            if (firstName.Length < 2) return "First name should be at least 2 symbols long";
            if (lastName.Length < 2) return "Last name should be at least 2 symbols long";
            if (!email.Contains("@") || !email.Contains(".")) return "Wrong email format";
            return null;
        }
    }
}
