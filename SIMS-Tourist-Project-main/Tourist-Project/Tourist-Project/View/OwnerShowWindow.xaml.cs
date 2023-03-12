﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Tourist_Project.Controller;
using Tourist_Project.Model;
using Tourist_Project.Observer;

namespace Tourist_Project
{
    /// <summary>
    /// Interaction logic for OwnerShowWindow.xaml
    /// </summary>
    public partial class OwnerShowWindow : Window, IObserver
    {
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation selectedAccommodation { get; set; }
        //public User LoggedInUser { get; set; }
        public static AccommodationController accommodationController;
        public OwnerShowWindow()
        {
            InitializeComponent();
            DataContext = this;
            //LoggedInUser = user;

            accommodationController = new AccommodationController();
            accommodationController.Subscribe(this);
            Accommodations = new ObservableCollection<Accommodation>(accommodationController.GetAll());
        }

        private void ShowCreateAccommodationForm(object sender, RoutedEventArgs e)
        {
            var createWindow = new AccommodationForm(accommodationController);
            createWindow.Show();
        }
        private void ShowViewAccommodationForm(object sender, RoutedEventArgs e)
        {
            var viewWindow = new AccommodationForm(accommodationController, selectedAccommodation);
            viewWindow.Show();
        }
        private void ShowUpdateAccommodationForm(object sender, RoutedEventArgs e)
        {
            var updateWindow = new AccommodationForm(selectedAccommodation, accommodationController);
            updateWindow.Show();
        }
        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (selectedAccommodation != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Delete comment",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    accommodationController.Delete(selectedAccommodation);
                }
            }
        }

        public void Update()
        {
            Accommodations.Clear();
            foreach(var accommodation in accommodationController.GetAll())
                accommodationController.Add(accommodation);
        }
    }
}
