﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{
    public class TourReviewService
    {
        private static readonly Injector injector = new();

        private readonly ITourReviewRepository repository = injector.CreateInstance<ITourReviewRepository>();
        public TourReviewService()
        {
        }

        public TourReview Create(TourReview tourReview)
        {
            return repository.Save(tourReview);
        }

        public TourReview Update(TourReview tourReview)
        {
            return repository.Update(tourReview);
        }

        public TourReview GetOne(int id)
        {
            return repository.GetOne(id);
        }

        public List<TourReview> GetAllByTourId(int tourId)
        {
            return repository.GetAllByTourId(tourId);
        }
    }
}
