using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication7
{
    class User
    {
        //fields
        public string login;
        public string password;
        public bool isAdmin = false;
        public List<Revews> revews = new List<Revews>();

        //constructors
        public User(string l, string p)
        {
            login = l;
            password = p;
        }
        public User(string l, string p, bool i)
        {
            login = l;
            password = p;
            isAdmin = i;
        }
        //methods
        public int Findrevew(string name)
        {
            int i;
            for (i = 0; i < revews.Count; i++)
            {
                if (revews[i].cafeName.Equals(name))
                    return i;
            }
            return -1;
        }
        public void AddRevew(int s, string c)
        {
            revews.Add(new Revews(s, c));
        }
        public void ChangeRevew(int s, int index)
        {
            revews[index].stars = s;
        }
        public void ChangeRevew(string name)
        {
            int i = Findrevew(name);
            if (i == -1)
                Console.WriteLine("No such revew!!");
            else
            {
                Console.WriteLine("Give new Revew!!");
                revews[i].stars = Convert.ToInt32(Console.ReadLine());
            }
        }
        static public string Passsecurity(string pass) {
            string pass1 = "";
            string key = "everybody goes to the rapture";
            for (int i = 0; i < pass.Length-1; i++)
            {
                pass1 += (char)((pass[i] + pass[i + 1])+key[i]);
            }
            return pass1;
        }
    }
}
