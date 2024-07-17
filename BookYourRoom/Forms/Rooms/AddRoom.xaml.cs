using BookYourRoom.Models;
using BookYourRoom.Services.Hotels;
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

namespace BookYourRoom.Forms.Rooms
{
    /// <summary>
    /// Interaction logic for AddRoom.xaml
    /// </summary>
    public partial class AddRoom : Window
    {
        private readonly IHotelService _hotelService;
        private readonly IRoomService _roomService;

        public AddRoom(IHotelService hotelService, IRoomService roomService)
        {
            InitializeComponent();
            _hotelService = hotelService;
            _roomService = roomService;
            LoadHotels();
        }

        private async void LoadHotels()
        {
            try
            {
                var hotels = await _hotelService.GetAllHotels();
                HotelComboBox.ItemsSource = hotels;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading hotels: {ex.Message}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string roomNumber = RoomNumberTextBox.Text;
            int? hotelId = HotelComboBox.SelectedValue as int?;

            if (string.IsNullOrEmpty(roomNumber) || !hotelId.HasValue)
            {
                MessageBox.Show("Please fill out all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                await _roomService.CreateRoom(new Room { RoomNumber = roomNumber, HotelId = hotelId.Value });
                MessageBox.Show("Room added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured while creating room: {ex.Message}");
            }

            this.Close();
        }
    }
}
