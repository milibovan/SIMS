﻿using System.Collections.Generic;
using System.Linq;
using Tourist_Project.Model;
using Tourist_Project.Serializer;

namespace Tourist_Project.Repository
{
    public class ImageRepository
    {
        private const string FilePath = "../../../Data/images.csv";
        private readonly Serializer<Image> serializer;
        private List<Image> images;
        public ImageRepository()
        {
            serializer = new Serializer<Image>();
            images = serializer.FromCSV(FilePath);
        }
        public List<Image> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }

        public Image Save(Image image)
        {
            image.Id = NextId();
            images = serializer.FromCSV(FilePath);
            images.Add(image);
            serializer.ToCSV(FilePath, images);
            return image;
        }

        public int NextId()
        {
            images = serializer.FromCSV(FilePath);
            if (images.Count < 1)
            {
                return 1;
            }
            return images.Max(c => c.Id) + 1;
        }

        public void Delete(Image image)
        {
            images = serializer.FromCSV(FilePath);
            Image founded = images.Find(c => c.Id == image.Id);
            images.Remove(founded);
            serializer.ToCSV(FilePath, images);
        }

        public Image Update(Image image)
        {
            images = serializer.FromCSV(FilePath);
            Image current = images.Find(c => c.Id == image.Id);
            int index = images.IndexOf(current);
            images.Remove(current);
            images.Insert(index, image);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, images);
            return image;
        }
        public Image GetImage(int id)
        {
            return images.Find(c => c.Id == id);
        }
    }
}