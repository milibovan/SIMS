﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.WPF.ViewModels
{
    public class AccommodationRatingViewModel : INotifyPropertyChanged
    {
        private Reservation reservation;
        public Reservation Reservation
        {
            get => reservation;
            set
            {
                if(value == reservation) return;
                reservation = value;
                OnPropertyChanged("Reservation");
            }
        }

        private User owner;
        public User Owner
        {
            get => owner;
            set
            {
                if(value == owner) return;
                owner = value;
                OnPropertyChanged("Owner");
            }
        }

        private User guest;
        public User Guest
        {
            get => guest;
            set
            {
                if (value == guest) return;
                guest = value;
                OnPropertyChanged("Guest");
            }
        }

        private Accommodation accommodation;
        public Accommodation Accommodation
        {
            get => accommodation;
            set
            {
                if (value == accommodation) return;
                accommodation = value;
                OnPropertyChanged("Accommodation");
            }
        }

        private Image image;
        public Image Image
        {
            get => image;
            set
            {
                if (value == image) return;
                image = value;
                OnPropertyChanged("Image");
            }
        }

        private AccommodationRating accommodationRating;
        public AccommodationRating AccommodationRating
        {
            get => accommodationRating;
            set
            {
                if (value == accommodationRating) return;
                accommodationRating = value;
                OnPropertyChanged("AccommodationRating");
            }
        }

        private static ReservationService reservationService = new();
        private static UserService userService = new();
        private static AccommodationService accommodationService = new();
        private static ImageService imageService = new();

        public AccommodationRatingViewModel(AccommodationRating accommodationRating)
        {
            AccommodationRating = accommodationRating;
            Reservation = reservationService.Get(AccommodationRating.ReservationId);
            Owner = userService.Get(AccommodationRating.UserId);
            Guest = userService.Get(Reservation.GuestId);
            Accommodation = accommodationService.Get(Reservation.AccommodationId);
            Image = imageService.Get(AccommodationRating.ImageId);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    } 
}
