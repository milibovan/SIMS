﻿using System;
using System.Collections.Generic;
using System.Linq;
using Tourist_Project.Model;
using Tourist_Project.Serializer;

namespace Tourist_Project.Repository
{
    public class ReservationRepository
    {
        private const string FilePath = "../../../Data/reservations.csv";
        private readonly Serializer<Reservation> serializer;
        private List<Reservation> reservations;
        public ReservationRepository()
        {
            serializer = new Serializer<Reservation>();
            reservations = serializer.FromCSV(FilePath);
        }
        public List<Reservation> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }

        public Reservation Save(Reservation reservation)
        {
            reservation.Id = NextId();
            reservations = serializer.FromCSV(FilePath);
            reservations.Add(reservation);
            serializer.ToCSV(FilePath, reservations);
            return reservation;
        }

        public int NextId()
        {
            reservations = serializer.FromCSV(FilePath);
            if (reservations.Count < 1)
            {
                return 1;
            }
            return reservations.Max(c => c.Id) + 1;
        }

        public void Delete(Reservation reservation)
        {
            reservations = serializer.FromCSV(FilePath);
            Reservation founded = reservations.Find(c => c.Id == reservation.Id);
            reservations.Remove(founded);
            serializer.ToCSV(FilePath, reservations);
        }

        public Reservation Update(Reservation reservation)
        {
            reservations = serializer.FromCSV(FilePath);
            Reservation current =reservations.Find(c => c.Id == reservation.Id);
            int index = reservations.IndexOf(current);
            reservations.Remove(current);
            reservations.Insert(index, reservation);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, reservations);
            return reservation;
        }
    }
}