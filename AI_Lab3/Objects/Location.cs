using System;
using System.Collections.Generic;

namespace CustomObjects 
{
    public class Location 
    {
        public int ID { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        
        public bool isVisited { get; set; }

        public Location() { this.ID = 0; this.X = 0; this.Y = 0; this.isVisited = false; }

        public Location(int id, double x, double y) { this.ID = id; this.X = x; this.Y = y; this.isVisited = false; }
    }


    public class Trevel 
    {
        public List<Location> listOfCities { get; set; }

        public Trevel() { this.listOfCities = new List<Location>(); }
    }
}