using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using System.IO;
using static System.Net.Mime.MediaTypeNames;


namespace ConsoleApplication7
{
    class Program
    {

        ////////////////////////////////////////////////////////////////////////////for login
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
        static string Pasword(int t, string b = "")

        {
            string pass = "";
            if (t != 1)
                Console.Write("Enter your password: ");
            ConsoleKeyInfo key;
            int a = Console.CursorTop;
            int i = Console.CursorLeft;
            int m = i;
            int s = 1;
            bool incorect = true;
            do
            {
                if (s == 0)
                {
                    Console.SetCursorPosition(i, a);
                    s = 1;
                }
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Escape && key.Key != ConsoleKey.LeftArrow && key.Key != ConsoleKey.RightArrow && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    a = Console.CursorTop;

                    Console.Write("*");
                    i++;
                }
                else if (key.Key == ConsoleKey.Escape)
                {

                    Environment.Exit(0);
                }
                else if (key.Key == ConsoleKey.LeftArrow && i != m)
                {
                    i--;
                    Console.SetCursorPosition(i, a);
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    i++;
                    Console.SetCursorPosition(i, a);
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        i--;
                        Console.Write("\b \b");
                    }
                    Console.Write("\b");
                }
                if ((pass.Length > 16 || pass.Length < 8))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(2, a + 1);
                    Console.Write("Your pass must be have length 8 - 16");
                    incorect = true;
                    s = 0;
                }
                else if ((!b.Contains(pass) && t == 1 && !incorect) || (!b.Equals(pass) && t == 1 && !incorect))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(2, a + 1);
                    Console.Write("Your passes not equels");
                    incorect = true;
                    s = 0;
                }
                else
                {
                    Console.SetCursorPosition(2, a + 1);
                    ClearCurrentConsoleLine();
                    incorect = false;
                    s = 0;
                }
                Console.ResetColor();
            }
            while (key.Key != ConsoleKey.Enter || incorect);
            return pass;
        }

        ////////////////////////////////////////////////////////////////////////////
        static string userPath = @"../../All Users.txt";
        static string cafePath = @"../../All Cafe's.txt";
        static string[] lines;
        static int userIndex = -1;
        static int cafeIndex = -1;
        static List<User> user = new List<User>();
        static List<Cafe> cafe = new List<Cafe>();
        //string userInfo = @"C:\Users\movse\Desktop\All Users.txt";
        //string cafeInfo = @"C:\Users\movse\Desktop\All Cafe's.txt";

        static int FindUser(string name)
        {
            int i;
            for (i = 0; i < user.Count; i++)
            {
                if (user[i].login.Equals(name))
                    return i;
            }
            return -1;
        }

        static int FindCafe(string name)
        {
            int i;
            List<Cafe> c = new List<Cafe>();
            List<int> ci = new List<int>();
            for (i = 0; i < cafe.Count; i++)
            {
                if (cafe[i].name.Length >= name.Length)
                {
                    if (cafe[i].name.Equals(name))
                        return i;
                    if (cafe[i].name.Substring(0, name.Length).Equals(name))
                    {
                        c.Add(cafe[i]);
                        ci.Add(i);
                    }
                }
            }
            if (c.Count == 0)
            {
                Console.WriteLine("No results found");
                return -1;
            }
            for (i = 0; i < c.Count; i++)
            {
                Console.WriteLine("Enter " + (i + 1) + " for " + c[i].name);
            }
            while (true)
            {
                i = Convert.ToInt32(Console.ReadLine());
                if (i <= 0 || i > c.Count) Console.WriteLine("Nope");
                else break;
            }
            Console.WriteLine("You chose " + c[i - 1].name);
            return ci[i - 1];
        }
        static void LogIn()
        {
            Console.Clear();
            int i;
            while (true)
            {
                Console.Write("Insert username :");
                i = FindUser(Console.ReadLine());
                if (i == -1) Console.WriteLine("No Such user!! \nRepeat?(Yes or No)");
                else break;
                if (Console.ReadLine().Equals("No")) break;
                Console.WriteLine();
            }
            if (i == -1)
                return;
            int ada = 0;
            while (true)
            {
                //Console.WriteLine("Enter password!!");
                string a = Pasword(0);
                if (user[i].password.Equals(User.Passsecurity(a)))
                {
                    userIndex = i;
                    break;
                }
                if (ada == 0) Console.WriteLine("Password incorrect \nYou can scoll up and see your password, But noooo!!\n");
                if (ada == 1) Console.WriteLine("Password incorrect \nAgain??\n");
                if (ada == 2) Console.WriteLine("Password incorrect \nWhats wrong with you??\n");
                if (ada == 3) Console.WriteLine("Password incorrect \nYou'r hopless @@\n");
                ada++;
            }
        }
        static void LogOut()
        {
            if (userIndex == -1)
                Console.WriteLine("Not logged in");
            else
            {
                userIndex = -1;
                Console.WriteLine("Done!!))");
            }
        }
       
        public static void Main()
        {
            //ConsoleKeyInfo cki;
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            lines = File.ReadAllLines(userPath);
            if (lines.Length > 0)
            {
                for (int i = 0; i < lines.Length - 3; i += 4)
                {
                    if (lines[i + 3] == "")
                        user.Add(new User(lines[i], lines[i + 1]));
                    else
                        user.Add(new User(lines[i], lines[i + 1], true));
                    string[] revs = lines[i + 2].Split();
                    if (revs.Length != 1)
                    {
                        for (int j = 0; j < revs.Length; j += 2)
                            user[i / 3].revews.Add(new Revews(Convert.ToInt32(revs[j]), revs[j + 1]));
                    }
                }
            }
            else
            {
                user.Add(new User("Admin", User.Passsecurity("password1"), true));
            }
            lines = File.ReadAllLines(cafePath);
            for (int i = 0; i < lines.Length; i += 8)
            {
                cafe.Add(new Cafe(lines[i], lines[i + 1], Convert.ToDouble(lines[i + 2]), Convert.ToDouble(lines[i + 3]), lines[i + 4], lines[i + 5], lines[i + 6]));
                string[] rev = lines[i + 7].Split();
                cafe[i / 8].reviews[0] = Convert.ToInt32(rev[0]);
                cafe[i / 8].reviews[1] = Convert.ToInt32(rev[1]);
                cafe[i / 8].reviews[2] = Convert.ToInt32(rev[2]);
                cafe[i / 8].reviews[3] = Convert.ToInt32(rev[3]);
                cafe[i / 8].reviews[4] = Convert.ToInt32(rev[4]);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            int a;
            while (true)
            {
                if (userIndex == -1)
                    Console.WriteLine("You should type those numbers to...\n 1)Register\n 2)search\n 3)Give populars\n 4)Print info of " + (cafeIndex == -1 ? "(cafe name)" : "") + "\n 5)Give nearby " + (cafeIndex == -1 ? "(cafe name)" : "") + "\n 6)Give near (insert meters) " + (cafeIndex == -1 ? "(cafe name)" : "") + "\n 7)exit\n 8)Log in");
                else if (user[userIndex].isAdmin)
                    Console.WriteLine("You should type those numbers to...\n 1)Register\n 2)search\n 3)Give populars\n 4)Print info of " + (cafeIndex == -1 ? "(cafe name)" : "") + "\n 5)Give nearby " + (cafeIndex == -1 ? "(cafe name)" : "") + "\n 6)Give near (insert meters) " + (cafeIndex == -1 ? "(cafe name)" : "") + "\n 7)exit\n 9)Register Admin\n 10)Make admin (username)\n 11)Add cafe\n 12)Add revew to " + (cafeIndex == -1 ? "(cafe name)" : "") + "\n 13)Log out\nLogged in as Admin " + user[userIndex].login);
                else
                    Console.WriteLine("You should type those numbers to...\n 1)Register\n 2)search\n 3)Give populars\n 4)Print info of " + (cafeIndex == -1 ? "(cafe name)" : "") + "\n 5)Give nearby " + (cafeIndex == -1 ? "(cafe name)" : "") + "\n 6)Give near (insert meters) " + (cafeIndex == -1 ? "(cafe name)" : "") + "\n 7)exit\n 12)Add revew to " + (cafeIndex == -1 ? "(cafe name)" : "") + "\n 13)Log out\nLogged in as " + user[userIndex].login);
                if (cafeIndex != -1) Console.WriteLine("Search result " + cafe[cafeIndex].name);
                a = Convert.ToInt32(Console.ReadLine());
                if (a == 2)
                {
                    Console.WriteLine("Enter cafe name!!");
                    int i = FindCafe(Console.ReadLine());
                    if (i != -1) cafeIndex = i;
                }
                if (a == 8)
                {
                    Console.Clear();
                    if (userIndex != -1)
                    {
                        Console.WriteLine("Already logged in");
                    }
                    else LogIn();
                }
                if (a == 13) LogOut();
                if (a == 1 || a == 9)
                {
                    Console.Clear();
                    bool e = true;
                    string username = "";
                    string[] password = new string[2];
                    int left = Console.CursorLeft;
                    int top = Console.CursorTop;
                    while (true)
                    {
                        Console.Write("Insert username : ");
                        left = Console.CursorLeft;
                        username = Console.ReadLine();

                        top = Console.CursorTop;
                        int t = username.Length;
                        if (FindUser(username) != -1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(2, top);
                            Console.Write("Username already taken");
                            Console.ResetColor();
                        }

                        else
                        {
                            ClearCurrentConsoleLine();
                            break;
                        }
                        Console.SetCursorPosition(left + t, 0);
                        ClearCurrentConsoleLine();


                    }

                    password[0] = Pasword(0);

                    Console.Write("Repeat : ");
                    password[1] = Pasword(1, password[0]);
                    e = true;



                    user.Add(new User(username, User.Passsecurity(password[0])));
                    if (a == 9)
                        user[user.Count - 1].isAdmin = true;
                    if (e)
                        Console.WriteLine("Finally!! what took you so long??");
                    
                    Console.Clear();
                }
                if (a == 10)
                {
                    if (user[userIndex].isAdmin)
                    {
                        while (true)
                        {
                            Console.WriteLine("Enter that lucky guy's name!!");
                            int i = FindUser(Console.ReadLine());
                            if (i == -1)
                                Console.WriteLine("Nope, try again!! or Quit!!!!");
                            else
                            {
                                user[i].isAdmin = true;
                                Console.WriteLine("Now " + user[i].login + " is Admin");
                                break;
                            }
                            if (Console.ReadLine().ToLower().Equals("quit"))
                            {
                                Console.WriteLine("Quitter!!");
                                break;
                            }
                        }
                    }
                    else Console.WriteLine("You'r not admin, stay away!!");
                }
                if (a == 11)
                {
                    if (userIndex == -1)
                        Console.WriteLine("Not logged in as Admin");
                    else if (userIndex == 0)
                    {
                        //while (true) {
                            try
                            {
                                Console.WriteLine("Enter name!! ");
                                string n = Console.ReadLine();
                                Console.WriteLine("Enter phone!! ");
                                string p = Console.ReadLine();
                                Console.WriteLine("Enter coordinates!! ");
                                double x = Convert.ToDouble(Console.ReadLine());
                                double y = Convert.ToDouble(Console.ReadLine());
                                Console.WriteLine("country!! ");
                                string country = Console.ReadLine();
                                Console.WriteLine("CITY!!!!! ");
                                string city = Console.ReadLine();
                                Console.WriteLine("street please ");
                                string street = Console.ReadLine();
                                cafe.Add(new Cafe(n, p, x, y, country, city, street));
                            }
                            catch (Exception e){ Console.WriteLine("    Please try again\n"); } }
                    //}
                    else
                        Console.WriteLine("WHO DO YOU THINK YOU ARE??\nNOT Admin thats for sure");
                }
                if (a == 4)
                {
                    if (cafeIndex == -1)
                    {
                        Console.WriteLine("Enter cafe name!!");
                        string c = Console.ReadLine();
                        int i;
                        for (i = 0; i < cafe.Count; i++)
                        {
                            if (cafe[i].name.Equals(c))
                            {
                                Console.WriteLine(cafe[i].ToString());
                                break;
                            }
                        }
                        if (i == cafe.Count)
                            Console.WriteLine("No such cafe!!");
                    }
                    else Console.WriteLine(cafe[cafeIndex].ToString());
                }
                if (a == 12)
                {
                    if (userIndex != -1)
                    {
                        Console.WriteLine("Enter cafe name!!");
                        int i;
                        string name = Console.ReadLine();
                        for (i = 0; i < cafe.Count; i++)
                        {
                            if (cafe[i].name.Equals(name))
                                break;
                        }
                        if (i == cafe.Count)
                            Console.WriteLine("No such cafe!!");
                        else
                        {
                            Console.WriteLine("enter you'r revew!!(biger than 0 and smaller than 6!!) ");
                            int revew = Convert.ToInt32(Console.ReadLine());
                            cafe[i].AddReview(revew);
                            int index = user[userIndex].Findrevew(name);
                            if (index == -1)
                                user[userIndex].AddRevew(revew, cafe[i].name);
                            else
                            {
                                cafe[i].reviews[user[userIndex].revews[index].stars - 1]--;
                                user[userIndex].ChangeRevew(revew, index);
                            }
                            Console.WriteLine("DONE))");
                        }
                    }
                    else
                        Console.WriteLine("Not loged in!! \nHow were you going to do that??");
                }
                if (a == 5)
                {
                    if (cafeIndex == -1)
                    {
                        Console.WriteLine("Enter cafe name!!");
                        int i = FindCafe(Console.ReadLine());
                        if (i != -1)
                            cafe[i].Nearby(cafe);
                        else Console.WriteLine("No such cafe");
                    }
                    else cafe[cafeIndex].Nearby(cafe);
                }
                if (a == 6)
                {
                    Console.Write("Enter distance ");
                    if (cafeIndex == -1)
                    {
                        Console.Write("and name!!");
                        string[] n = Console.ReadLine().Split();
                        int i = FindCafe(n[1]);
                        if (i == -1) Console.WriteLine("No such cafe!!");
                        else
                        {
                            Console.WriteLine("here you go!!");
                            cafe[i].Nearby(cafe, Convert.ToDouble(n[0]));
                        }
                    }
                    else
                    {
                        double d = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("here you go!!");
                        cafe[cafeIndex].Nearby(cafe, d);
                    }
                }
                if (a == 3)
                    Cafe.GiveMostPopulars(cafe.ToList());
                if (a == 7)
                {
                    StreamWriter file1 = new StreamWriter(userPath);
                    StreamWriter file2 = new StreamWriter(cafePath);
                    foreach (User u in user)
                    {
                        file1.WriteLine(u.login);
                        file1.WriteLine(u.password);
                        foreach (Revews r in u.revews)
                        {
                            file1.Write(r.stars + " " + r.cafeName);
                        }
                        file1.WriteLine();
                        file1.WriteLine(u.isAdmin ? "Hi's ADMIN" : "");
                    }
                    file1.Flush();
                    file1.Close();
                    foreach (Cafe c in cafe)
                    {
                        file2.WriteLine(c.name);
                        file2.WriteLine(c.phone);
                        file2.WriteLine(c.adress.coordinates.Latitude);
                        file2.WriteLine(c.adress.coordinates.Longitude);
                        file2.WriteLine(c.adress.country);
                        file2.WriteLine(c.adress.city);
                        file2.WriteLine(c.adress.street);
                        file2.WriteLine(c.reviews[0] + " " + c.reviews[1] + " " + c.reviews[2] + " " + c.reviews[3] + " " + c.reviews[4]);
                    }
                    file2.Flush();
                    file2.Close();
                    return;
                }
            }
        }
    }
}