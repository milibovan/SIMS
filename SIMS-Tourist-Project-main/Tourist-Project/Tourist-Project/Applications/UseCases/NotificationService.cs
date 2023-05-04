﻿using System;
using System.Collections.Generic;
using System.Linq;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{
    public class NotificationService
    {
        private static readonly Injector injector = new();

        private readonly INotificationRepository notificationRepository = injector.CreateInstance<INotificationRepository>();
        private readonly IAccommodationRatingRepository accommodationRatingRepository = injector.CreateInstance<IAccommodationRatingRepository>();
        private readonly IGuestRateRepository guestRateRepository = injector.CreateInstance<IGuestRateRepository>();
        private readonly IReservationRepository reservationRepository = injector.CreateInstance<IReservationRepository>();
        private readonly GuestRateService guestRateService = new();
        public NotificationService()
        {
        }

        public Notification Create(Notification notification)
        {
            return notificationRepository.Save(notification);
        }

        public List<Notification> GetAll()
        {
            return notificationRepository.GetAll();
        }

        public List<Notification> GetAllByType(string type)
        {
            return notificationRepository.GetAllByType(type);
        }

        public Notification Get(int id)
        {
            return notificationRepository.GetById(id);
        }

        public Notification Update(Notification notification)
        {
            return notificationRepository.Update(notification);
        }

        public void Delete(int id)
        {
            notificationRepository.Delete(id);
        }

        public bool HasReviews()
        {
            if (accommodationRatingRepository.GetAll().Count == 0) return false;
            foreach (var accommodationRating in accommodationRatingRepository.GetAll())
            {
                foreach (var notification in GetAll())
                {
                    if (notification.TypeId == accommodationRating.Id && !notification.Notified)
                        Create(new Notification("Reviews", false, accommodationRating.Id));
                }
            }
            return true;
        }
        public void HasUnratedGuests()
        {
            guestRateService.HasNewRatings();
            foreach (var guestRate in guestRateRepository.GetAll())
            {
                foreach (var reservation in reservationRepository.GetAll())
                {
                    var daysSinceCheckOut = DateTime.Now - reservation.CheckOut;
                    if ((notificationRepository.GetAll().Count == 0 || notificationRepository.GetAll().All(c => c.TypeId != guestRate.Id)) && !guestRate.IsReviewed() && Math.Abs(daysSinceCheckOut.Days) < 5 && guestRate.ReservationId == reservation.Id)
                    {
                        Create(new Notification("GuestRate", true, guestRate.Id));
                    }
                }
            }
        }
    }

}