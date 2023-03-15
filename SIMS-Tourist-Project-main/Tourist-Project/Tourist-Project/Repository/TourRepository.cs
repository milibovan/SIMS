﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Model;
using Tourist_Project.Serializer;

namespace Tourist_Project.Repository
{
    public class TourRepository
    {
        private const string filePath = "../../../Data/tours.csv";
        private readonly Serializer<Tour> serializer;
        private List<Tour> tours;

        public TourRepository()
        {
            serializer = new Serializer<Tour>();
            tours = serializer.FromCSV(filePath);
        }

        public List<Tour> GetAll()
        {
            return serializer.FromCSV(filePath);
        }

        public List<Tour> GetTodaysTours()
        {
            return GetAll().FindAll(tour => tour.StartTime.Date == DateTime.Today.Date);
        }

        public void Save(Tour tour)
        {
            tour.Id = NextId();
            tours = serializer.FromCSV(filePath);
            tours.Add(tour);
            serializer.ToCSV(filePath, tours);
        }

        public int NextId()
        {
            tours = serializer.FromCSV(filePath);
            if (tours.Count < 1)
            {
                return 1;
            }
            return tours.Max(c => c.Id) + 1;
        }
    }
}