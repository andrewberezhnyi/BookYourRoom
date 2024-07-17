using BookYourRoom.Models;
using BookYourRoom.Services.Bookings;
using BookYourRoom.Services.Customers;
using BookYourRoom.Services.Hotels;
using BookYourRoom.Services.Rooms;
using BookYourRoom.Forms.Hotels;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookYourRoom.Forms.Customers;
using BookYourRoom.Forms.Rooms;
using BookYourRoom.Forms.Bookings;

namespace BookYourRoom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Hotel> Hotels { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }
        public ObservableCollection<Customer> Customers { get; set; }
        public ObservableCollection<Booking> Bookings { get; set; }

        private readonly IHotelService _hotelService;
        private readonly IRoomService _roomService;
        private readonly ICustomerService _customerService;
        private readonly IBookingService _bookingService;

        public MainWindow(IHotelService hotelService, IRoomService roomService, ICustomerService customerService, IBookingService bookingService)
        {
            InitializeComponent();
            _hotelService = hotelService;
            _roomService = roomService;
            _customerService = customerService;
            _bookingService = bookingService;
            DataContext = this;
        }

        protected override async void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            await LoadHotels();
        }

        private async void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is TabItem selectedTab)
            {
                switch (selectedTab.Header.ToString())
                {
                    case "Hotels":
                        await LoadHotels();
                        break;
                    case "Rooms":
                        await LoadRooms();
                        break;
                    case "Bookings":
                        await LoadBookings();
                        break;
                    case "Customers":
                        await LoadCustomers();
                        break;
                }
            }
        }

        private async Task LoadHotels()
        {

            try
            {
                var hotels = await _hotelService.GetAllHotels();
                Hotels = new ObservableCollection<Hotel>(hotels);
                HotelsDataGrid.ItemsSource = Hotels;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading hotels: {ex.Message}");
            }

        }

        private async Task LoadRooms()
        {
            try
            {
                var rooms = await _roomService.GetAllRooms();
                Rooms = new ObservableCollection<Room>(rooms);
                RoomsDataGrid.ItemsSource = Rooms;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading rooms: {ex.Message}");
            }
        }

        private async Task LoadCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomers();
                Customers = new ObservableCollection<Customer>(customers);
                CustomersDataGrid.ItemsSource = Customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}");
            }
        }

        private async Task LoadBookings()
        {
            try
            {
                var bookings = await _bookingService.GetAllBookings();
                Bookings = new ObservableCollection<Booking>(bookings);
                BookingsDataGrid.ItemsSource = Bookings;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bookings: {ex.Message}");
            }
        }

        private void AddHotelButton_Click(object sender, RoutedEventArgs e)
        {
            AddHotel addHotelWindow = new AddHotel(_hotelService);
            addHotelWindow.ShowDialog();
            LoadHotels();
        }

        private void EditHotelButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedHotel = (Hotel)HotelsDataGrid.SelectedItem;
            if (selectedHotel == null)
            {
                MessageBox.Show("Please select a hotel to edit.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            UpdateHotel updateHotelWindow = new UpdateHotel(selectedHotel, _hotelService);
            updateHotelWindow.ShowDialog();
            LoadHotels();
        }

        private async void DeleteHotelButton_Click(Object sender, RoutedEventArgs e)
        {
            var selectedHotel = (Hotel)HotelsDataGrid.SelectedItem;
            if (selectedHotel == null)
            {
                MessageBox.Show("Please select a hotel to delete.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                await _hotelService.DeleteHotel(selectedHotel.HotelId);
                await LoadHotels();

                MessageBox.Show("Hotel successfully deleted!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured while deleting hotel: {ex.Message}");
            }
        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            AddCustomer addCustomerWindow = new AddCustomer(_customerService);
            addCustomerWindow.ShowDialog();
            LoadCustomers();

        }

        private void EditCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCustomer = (Customer)CustomersDataGrid.SelectedItem;
            if (selectedCustomer == null)
            {
                MessageBox.Show("Please select a customer to edit.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            UpdateCustomer updateCustomerWindow = new UpdateCustomer(selectedCustomer, _customerService);
            updateCustomerWindow.ShowDialog();
            LoadCustomers();
        }

        private void AddRoomButton_Click(object sender, RoutedEventArgs e)
        {
            AddRoom addRoomWindow = new AddRoom(_hotelService, _roomService);
            addRoomWindow.ShowDialog();
            LoadRooms();
        }

        private async void EditRoomButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoomsDataGrid.SelectedItem is Room selectedRoom)
            {
                UpdateRoom updateRoomWindow = new UpdateRoom(_hotelService, _roomService, selectedRoom);
                updateRoomWindow.ShowDialog();
                LoadRooms();
            }
        }

        private void AddBookingButton_Click(object sender, RoutedEventArgs e)
        {
            AddBooking addBookingWindow = new AddBooking(_customerService, _roomService, _bookingService);
            addBookingWindow.ShowDialog();
            LoadBookings();
        }

        private void EditBookingButton_Click(object sender, RoutedEventArgs e)
        {
            if (BookingsDataGrid.SelectedItem is Booking selectedBooking)
            {
                UpdateBooking updateBookingWindow = new UpdateBooking(selectedBooking, _customerService, _roomService, _bookingService);
                updateBookingWindow.ShowDialog();
                LoadBookings();
            }
        }
    }
}