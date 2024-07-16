using BookYourRoom.Models;
using BookYourRoom.Services.Hotels;
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

        private readonly IHotelService _hotelService;

        public MainWindow(IHotelService hotelService)
        {
            InitializeComponent();
            _hotelService = hotelService;
            DataContext = this;
        }

        protected override async void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            await LoadHotels();
        }

        private async Task LoadHotels()
        {
            var hotels = await _hotelService.GetAllHotels();
            Hotels = new ObservableCollection<Hotel>(hotels);
            HotelsDataGrid.ItemsSource = Hotels;
        }
    }
}