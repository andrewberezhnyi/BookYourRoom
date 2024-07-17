using BookYourRoom.Models;
using BookYourRoom.Services.Bookings;
using BookYourRoom.Services.Customers;
using BookYourRoom.Services.Rooms;
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

namespace BookYourRoom.Forms.Bookings
{
    /// <summary>
    /// Interaction logic for AddBooking.xaml
    /// </summary>
    public partial class AddBooking : Window
    {
        private readonly ICustomerService _customerService;
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;

        public AddBooking(ICustomerService customerService, IRoomService roomService, IBookingService bookingService)
        {
            InitializeComponent();
            _customerService = customerService;
            _roomService = roomService;
            _bookingService = bookingService;
            LoadData();
        }

        private async Task LoadData()
        {
            await LoadCustomers();
            await LoadRooms();
        }

        private async Task LoadCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomers();
                CustomerComboBox.ItemsSource = customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}");
            }
        }

        private async Task LoadRooms()
        {
            try
            {
                var rooms = await _roomService.GetAllRooms();
                RoomComboBox.ItemsSource = rooms;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading rooms: {ex.Message}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? checkInDate = CheckInDatePicker.SelectedDate;
            DateTime? checkOutDate = CheckOutDatePicker.SelectedDate;
            int? customerId = CustomerComboBox.SelectedValue as int?;
            int? roomId = RoomComboBox.SelectedValue as int?;

            if (!checkInDate.HasValue || !checkOutDate.HasValue || !customerId.HasValue || !roomId.HasValue)
            {
                MessageBox.Show("Please fill out all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (checkOutDate < checkInDate)
            {
                MessageBox.Show("Check in date must be before check out date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Booking newBooking = new Booking
            {
                CheckInDate = checkInDate.Value,
                CheckOutDate = checkOutDate.Value,
                CustomerId = customerId.Value,
                RoomId = roomId.Value
            };

            try
            {
                await _bookingService.CreateBooking(newBooking);
                MessageBox.Show("Booking added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured while creating booking: {ex.Message}");
            }

            this.Close();
        }
    }
}
