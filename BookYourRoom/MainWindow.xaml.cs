using BookYourRoom.Models;
using BookYourRoom.Services.Bookings;
using BookYourRoom.Services.Customers;
using BookYourRoom.Services.Hotels;
using BookYourRoom.Services.Rooms;
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
                if (selectedTab.Header.ToString() == "Hotels")
                {
                    await LoadHotels();
                }

                if (selectedTab.Header.ToString() == "Rooms")
                {
                    await LoadRooms();
                }

                if (selectedTab.Header.ToString() == "Bookings")
                {
                    await LoadBookings();
                }

                if (selectedTab.Header.ToString() == "Customers")
                {
                    await LoadCustomers();
                }
            }
        }

        private async Task LoadHotels()
        {
            var hotels = await _hotelService.GetAllHotels();
            Hotels = new ObservableCollection<Hotel>(hotels);
            HotelsDataGrid.ItemsSource = Hotels;
        }

        private async Task LoadRooms()
        {
            var rooms = await _roomService.GetAllRooms();
            Rooms = new ObservableCollection<Room>(rooms);
            RoomsDataGrid.ItemsSource = Rooms;
        }

        private async Task LoadCustomers()
        {
            var customers = await _customerService.GetAllCustomers();
            Customers = new ObservableCollection<Customer>(customers);
            CustomersDataGrid.ItemsSource = Customers;
        }

        private async Task LoadBookings()
        {
            var bookings = await _bookingService.GetAllBookings();
            Bookings = new ObservableCollection<Booking>(bookings);
            BookingsDataGrid.ItemsSource = Bookings;
        }
    }
}