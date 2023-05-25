﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.WPF.ViewModels;

namespace Tourist_Project.Applications.UseCases
{
    public class TourPointService
    {
        private static readonly Injector injector = new();

        private readonly ITourPointRepository repository = injector.CreateInstance<ITourPointRepository>();
        private readonly ITourAttendanceRepository tourAttendanceRepository = injector.CreateInstance<ITourAttendanceRepository>();

        public TourPoint GetOne(int id)
        {
            return repository.GetOne(id);
        }

        public List<TourPoint> GetAll()
        {
            return repository.GetAll();
        }

        public TourPoint Update(TourPoint tourPoint)
        {
            return repository.Update(tourPoint);
        }

        public void Save(TourPoint tourPoint)
        {
            repository.Save(tourPoint);
        }

        public void UpdateCollection(TourPoint selectedTourPoint, Tour selectedTour)
        {
            repository.Update(selectedTourPoint);
            TourLiveViewModel.TourPoints.Clear();
            foreach (TourPoint point in repository.GetAllForTour(selectedTour.Id))
            {
                TourLiveViewModel.TourPoints.Add(point);
            }
        }

        public List<TourPoint> GetAllForTour(int tourId)
        {
            return repository.GetAllForTour(tourId);
        }

        public bool EndTour()
        {
            return TourLiveViewModel.TourPoints.ToList().Find(tourPoint => tourPoint.Visited == false) == null;
        }

        public string GetCheckpointName(int userId, int tourId)
        {
           return GetOne(tourAttendanceRepository.GetAllByTourId(tourId).Find(attendance => attendance.UserId == userId)
                .CheckPointId).Name;
        }
    }
}
