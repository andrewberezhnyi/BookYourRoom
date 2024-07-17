﻿using BookYourRoom.Models;
using BookYourRoom.Services.Hotels;
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
    /// Interaction logic for UpdateHotel.xaml
    /// </summary>
    public partial class UpdateHotel : Window
    {
        private readonly Hotel _hotel;
        private readonly IHotelService _hotelService;

        public UpdateHotel(Hotel hotel, IHotelService hotelService)
        {
            InitializeComponent();
            _hotel = hotel;
            _hotelService = hotelService;

            HotelNameTextBox.Text = hotel.Name;
            HotelAddressTextBox.Text = hotel.Address;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string hotelName = HotelNameTextBox.Text;
            string hotelAddress = HotelAddressTextBox.Text;

            if (string.IsNullOrEmpty(hotelName) || string.IsNullOrEmpty(hotelAddress))
            {
                MessageBox.Show("Please enter both hotel name and address.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _hotelService.UpdateHotel(new Hotel() { HotelId = _hotel.HotelId, Name = hotelName, Address = hotelAddress});
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured while updating hotel: {ex.Message}");
            }


            MessageBox.Show("Hotel updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
