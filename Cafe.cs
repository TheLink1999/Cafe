using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication7
{
    class Cafe : IComparable
    {
        public string name;
        public string phone;
        public Adress adress;
        public int[] reviews = new int[5];

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Cafe otherTemperature = obj as Cafe;
            if (otherTemperature != null)
                return this.CountMijin().CompareTo(otherTemperature.CountMijin());
            else
                throw new ArgumentException("Object is not a Temperature");
        }
        public Cafe(string n, string p, double x, double y, string country, string city, string street)
        {
            name = n;
            phone = p;
            adress = new Adress(x, y, country, city, street);
        }
        public void AddReview(int r)
        {
            reviews[r - 1]++;
            //qanak++;
        }
        public int CountMijin()
        {
            if (reviews[0] + reviews[1] + reviews[2] + reviews[3] + reviews[4] == 0)
                return 0;
            return (reviews[0] + reviews[1] * 2 + reviews[2] * 3 + reviews[3] * 4 + reviews[4] * 5) / (reviews[0] + reviews[1] + reviews[2] + reviews[3] + reviews[4]);
        }
        /*enum days
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }*/
        public void Nearby(List<Cafe> cafe)
        {
            Nearby(cafe, 1000);
        }
        public void Nearby(List<Cafe> cafe, double radius)
        {
            for (int i = 0; i < cafe.Count; i++)
            {
                if (cafe[i].adress.coordinates.GetDistanceTo(adress.coordinates) <= radius && !cafe[i].name.Equals(name))
                    Console.WriteLine(cafe[i].name);
            }
        }
        public static void GiveMostPopulars(List<Cafe> cafe)
        {
            cafe.Sort();
            for (int i = cafe.Count() - 1; i >= 0; i--)
            {
                Console.WriteLine(cafe[i].name);
            }
        }
        public override string ToString()
        {
            return "Name - " + name + "\nphone - " + phone + "\n" + adress.ToString() + "\nrevews\n5 - " + reviews[4] + "\n4 - " + reviews[3] + "\n3 - " + reviews[2] + "\n2 - " + reviews[1] + "\n1 - " + reviews[0];
        }
    }
}
