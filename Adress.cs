using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace ConsoleApplication7
{
    class Adress
    {
        public GeoCoordinate coordinates;
        public string country;
        public string city;
        public string street;
        public Adress(double x, double y, string country, string city, string street)
        {
            coordinates = new GeoCoordinate(x, y);
            this.country = country;
            this.city = city;
            this.street = street;
        }
        public override string ToString()
        {
            return "country - " + country + "\ncity - " + city + "\nstreet - " + street;
        }
    }
}
