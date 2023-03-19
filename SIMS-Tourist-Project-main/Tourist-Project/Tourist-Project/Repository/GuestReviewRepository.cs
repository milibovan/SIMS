﻿using System.Collections.Generic;
using System.Linq;
using Tourist_Project.Model;
using Tourist_Project.Serializer;

namespace Tourist_Project.Repository
{
    public class GuestReviewRepository
    {
        private const string FilePath = "../../../Data/guestReviews.csv";
        private readonly Serializer<GuestReview> serializer;
        private List<GuestReview> guestReviews;
        public GuestReviewRepository()
        {
            serializer = new Serializer<GuestReview>();
            guestReviews = serializer.FromCSV(FilePath);
        }
        public List<GuestReview> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }

        public GuestReview Save(GuestReview guestReview)
        {
            guestReview.Id = NextId();
            guestReviews = serializer.FromCSV(FilePath);
            guestReviews.Add(guestReview);
            serializer.ToCSV(FilePath, guestReviews);
            return guestReview;
        }

        public int NextId()
        {
            guestReviews = serializer.FromCSV(FilePath);
            if (guestReviews.Count < 1)
            {
                return 1;
            }
            return guestReviews.Max(c => c.Id) + 1;
        }

        public void Delete(GuestReview guestReview)
        {
            guestReviews = serializer.FromCSV(FilePath);
            GuestReview founded = guestReviews.Find(c => c.Id == guestReview.Id);
            guestReviews.Remove(founded);
            serializer.ToCSV(FilePath, guestReviews);
        }

        public GuestReview Update(GuestReview guestReview)
        {
            guestReviews = serializer.FromCSV(FilePath);
            GuestReview current = guestReviews.Find(c => c.Id == guestReview.Id);
            int index = guestReviews.IndexOf(current);
            guestReviews.Remove(current);
            guestReviews.Insert(index, guestReview);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, guestReviews);
            return guestReview;
        }
    }
}