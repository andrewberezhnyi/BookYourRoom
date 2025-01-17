﻿using BookYourRoom.Services.Hotels;
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

namespace BookYourRoom.Forms.Hotels
{
    /// <summary>
    /// Interaction logic for AddHotel.xaml
    /// </summary>
    public partial class AddHotel : Window
    {
        private readonly IHotelService _hotelService;

        public AddHotel(IHotelService hotelService)
        {
            _hotelService = hotelService;
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string hotelName = HotelNameTextBox.Text;
            string hotelAddress = HotelAddressTextBox.Text;

            if (string.IsNullOrEmpty(hotelName) || string.IsNullOrEmpty(hotelAddress))
            {
                MessageBox.Show("Please enter both hotel name and address.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string? validationError = ValidateHotelData(hotelName, hotelAddress);

            if (validationError != null)
            {
                MessageBox.Show(validationError, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            try
            {
                await _hotelService.CreateHotel(new Models.Hotel() { Address = hotelAddress, Name = hotelName });
                MessageBox.Show("Hotel added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured while creating hotel: {ex.Message}");
            }

            this.Close();
        }

        private string? ValidateHotelData(string hotelName, string hotelAddress)
        {
            if (hotelName.Length < 4) return "Hotel name should be at least 4 symbols long";
            if (hotelAddress.Length < 8) return "Hotel address should be at least 8 symbols long";
            return null;
        }
    }
}
