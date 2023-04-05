﻿using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tourist_Project.Repository;
using Tourist_Project.Model;
using Tourist_Project.DTO;
using System.Collections.Generic;
using System;


namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for GuestOne.xaml
    /// </summary>
    public partial class GuestOne : Window
    {

        public ObservableCollection<AccommodationDTO> SearchResults { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public List<AccommodationDTO> AccommodationDTOs { get; set; }

        public AccommodationDTO SelectedAccommodationDTO { get; set; }

        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> Cities { get; set; }

        public ObservableCollection<Tourist_Project.Model.Image> Images { get; set; }

        //public Location Location { get; set; }
        public ObservableCollection<Location> Locations { get; set; }

        public ObservableCollection<string> FullLocations { get; set; }

        private readonly AccommodationRepository accommodationRepository;

        private readonly LocationRepository locationRepository;

        private readonly ImageRepository imageRepository;

        private readonly AccommodationDTORepository accommodationDTORepository;
        public string SelectedCountry { get; set; }
        public string SelectedCity { get; set; }

        public Tourist_Project.Model.AccommodationType AccomodationType { get; set; }
        public string SearchedName { get; set; }
        public string SearchedLocation { get; set; }

        public int SearchedGuestsNumber { get; set; }

        public int SearchedCancelationThreshold { get; set; }
        public string SelectedType { get; set; }

        public ObservableCollection<string> AccommodationTypes { get; set; }

        public User LoggedInUser { get; set; }
        public GuestOne(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;

            locationRepository = new LocationRepository();
            accommodationRepository = new AccommodationRepository();
            imageRepository = new ImageRepository();
            accommodationDTORepository = new AccommodationDTORepository();

            AccommodationDTOs = new List<AccommodationDTO>();


            Countries = new ObservableCollection<string>();
            Cities = new ObservableCollection<string>();
            AccommodationTypes = new ObservableCollection<string>();
            SearchResults = new ObservableCollection<AccommodationDTO>(AccommodationDTOs);
            FullLocations = new ObservableCollection<string>();


            Accommodations = new ObservableCollection<Accommodation>(accommodationRepository.GetAll());
            Locations = new ObservableCollection<Location>(locationRepository.GetAll());
            Images = new ObservableCollection<Tourist_Project.Model.Image>(imageRepository.GetAll());

            AccommodationDTOs = accommodationDTORepository.LoadAll(Accommodations, Locations, Images);


            GetCountries();
            GetCities();
            GetAccommodationTypes();
            GetFullLocationNames();
            HandleAny();
        }

        public void HandleAny()
        {
            AccommodationTypes.Add("Any");
        }

        public ObservableCollection<string> GetAccommodationTypes()
        private void SearchByNameClick(object sender, RoutedEventArgs e)
        {
            foreach (string type in Enum.GetNames(typeof(Tourist_Project.Model.AccommodationType)))
            {
                type.ToString();
                AccommodationTypes.Add(type);
            }
            return AccommodationTypes;
        }

        public ObservableCollection<string> GetFullLocationNames()
        {
            foreach (AccommodationDTO accommodationDTO in AccommodationDTOs)
            {
                FullLocations.Add(accommodationDTO.LocationFullName);
            }
            return FullLocations;
        }
        public ObservableCollection<string> GetCountries()
        {
            foreach (Location location in Locations)
            {
                if (Countries.Contains(location.Country))
                {
                    continue;
                }
                else
                {
                    Countries.Add(location.Country);
                }
            }
            return Countries;
        }

        public ObservableCollection<string> GetCities()
        {
            foreach (Location location in Locations)
            {
                Cities.Add(location.City);
            }
            return Cities;
        }
        private void SelectedCountryChanged(object sender, SelectionChangedEventArgs e)
        {

            Cities.Clear();
            foreach (Location location in Locations)
            {
                if (location.Country == SelectedCountry)
                {
                    Cities.Add(location.City);
                }
            }
        }

        private void SelectedCityChanged(object sender, SelectionChangedEventArgs e)
        {

            foreach (Location location in Locations)
            {
                if (location.City == SelectedCity)
                {
                    SelectedCountry = location.Country;
                }
            }
        }


        public void HandleEmptyStrings()
        {
            if (SearchedName == null)
            {
                SearchedName = string.Empty;
            }

            if (SelectedCountry == null)
            {
                SelectedCountry = string.Empty;
            }

            if (SelectedCity == null)
            {
                SelectedCity = string.Empty;
            }

            if (SearchedGuestsNumber == 0)
            {
                SearchedGuestsNumber = 1;
            }

            if (SearchedCancelationThreshold == 0)
            {
                foreach (AccommodationDTO accommodationDTO in AccommodationDTOs)
                {
                    SearchedCancelationThreshold = accommodationDTO.CancellationThreshold;
                }
            }

            if(SelectedType == null || SelectedType == "Any")
            {
                SelectedType = string.Empty;
            }
        }

        public void SearchLogic()
        {
            SearchResults.Clear();
            HandleEmptyStrings();
            foreach (AccommodationDTO accommodationDTO in AccommodationDTOs)
            {
                if (accommodationDTO.Name.ToLower().Contains(SearchedName.ToLower()) && accommodationDTO.MaxGuestNum >= SearchedGuestsNumber
                    && accommodationDTO.MinStayingDays >= SearchedCancelationThreshold &&
                    accommodationDTO.AccommodationType.ToString().Contains(SelectedType) &&
                    accommodationDTO.LocationFullName.Contains(SelectedCity + " " + SelectedCountry)) { 
                        SearchResults.Add(accommodationDTO);
                }
            }
            
        }


        public void ResetParameters()
        {
            SearchedGuestsNumber = 0;
            SearchedCancelationThreshold = 0;
            SelectedType = "Any";
            SelectedCountry = string.Empty;
            SelectedCity = string.Empty;
            SearchedName = string.Empty;
        }

        public void SearchClick(object sender, RoutedEventArgs e)
        {
            SearchLogic();
            DataGrid.ItemsSource = SearchResults;
        }

        public void ShowAllClick(object sender, RoutedEventArgs e)
        {
            ResetParameters();
            DataGrid.ItemsSource = AccommodationDTOs;
        }

        public void ReserveClick(object sender, RoutedEventArgs e)
        {
           // var BookAccommodation = new BookAccommodationWindow(SelectedAccommodationDTO);
            //BookAccommodation.Show();
        }
    }
}
