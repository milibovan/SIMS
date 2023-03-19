﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tourist_Project.Serializer;

namespace Tourist_Project.Model
{
    public class Image : ISerializable
    {
        private int id;
        public int Id
        {

            get => id;
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }
        private string url;
        public string Url
        {
            get => url;
            set
            {
                if (value != url)
                {
                    url = value;
                    OnPropertyChanged();
                }
            }
        }

        public Image()
        {
        }
        public Image(string url)
        {
            this.Url = url;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Url
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Url = values[1];
        }
    }
}