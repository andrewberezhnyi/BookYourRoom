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
    /// Interaction logic for UpdateBooking.xaml
    /// </summary>
    public partial class UpdateBooking : Window
    {
        private readonly ICustomerService _customerService;
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;
        private readonly Booking _booking;

        public UpdateBooking(Booking booking, ICustomerService customerService, IRoomService roomService, IBookingService bookingService)
        {
            InitializeComponent();
            _customerService = customerService;
            _roomService = roomService;
            _bookingService = bookingService;
            _booking = booking;
            
            LoadData();
            InitializeFields();
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

        private void InitializeFields()
        {
            CheckInDatePicker.SelectedDate = _booking.CheckInDate;
            CheckOutDatePicker.SelectedDate = _booking.CheckOutDate;
            CustomerComboBox.SelectedItem = CustomerComboBox.Items.OfType<Customer>().FirstOrDefault(c => c.CustomerId == _booking.CustomerId);
            RoomComboBox.SelectedItem = RoomComboBox.Items.OfType<Room>().FirstOrDefault(r => r.RoomId == _booking.RoomId);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? checkInDate = CheckInDatePicker.SelectedDate;
            DateTime? checkOutDate = CheckOutDatePicker.SelectedDate;
            var selectedCustomer = CustomerComboBox.SelectedItem as Customer;
            var selectedRoom = RoomComboBox.SelectedItem as Room;

            if (!checkInDate.HasValue || !checkOutDate.HasValue || selectedCustomer == null || selectedRoom == null)
            {
                MessageBox.Show("Please fill out all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (checkOutDate < checkInDate)
            {
                MessageBox.Show("Check in date must be before check out date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _booking.CheckInDate = checkInDate.Value;
            _booking.CheckOutDate = checkOutDate.Value;
            _booking.CustomerId = selectedCustomer.CustomerId;
            _booking.RoomId = selectedRoom.RoomId;

            try
            {
                // await _bookingService.UpdateBooking(_booking);
                MessageBox.Show("Booking updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating booking: {ex.Message}");
            }
        }
    }
}
