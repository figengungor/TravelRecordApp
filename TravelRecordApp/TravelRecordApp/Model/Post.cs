using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecordApp.Model
{
    public class Post
    {
        [PrimaryKey, AutoIncrement]  //Sqlite Attributes
        public int Id { get; set; }

        [MaxLength(250)] //if Experince is longer than 250 chars, it will throw an error and it will not be saved to db
        public string Experience { get; set; }

        public string VenueName { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Distance { get; set; }

    }
}
