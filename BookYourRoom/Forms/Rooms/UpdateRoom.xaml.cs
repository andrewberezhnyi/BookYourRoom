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
    /// Interaction logic for UpdateRoom.xaml
    /// </summary>
    public partial class UpdateRoom : Window
    {
        private readonly IHotelService _hotelService;
        private readonly IRoomService _roomService;
        private readonly Room _room;

        public UpdateRoom(IHotelService hotelService, IRoomService roomService, Room room)
        {
            InitializeComponent();
            _hotelService = hotelService;
            _roomService = roomService;
            _room = room;
            LoadHotels();
            LoadRoomData();
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

        private void LoadRoomData()
        {
            RoomNumberTextBox.Text = _room.RoomNumber;
            HotelComboBox.SelectedValue = _room.HotelId;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string roomNumber = RoomNumberTextBox.Text;
            int? hotelId = HotelComboBox.SelectedValue as int?;

            if (string.IsNullOrEmpty(roomNumber) || !hotelId.HasValue)
            {
                MessageBox.Show("Please fill out all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _room.RoomNumber = roomNumber;
            _room.HotelId = hotelId.Value;

            // await _roomService.UpdateRoom(_room);

            MessageBox.Show("Room updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
