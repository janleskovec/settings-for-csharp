using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings.SetValue("resolution", "1920x1080");
            Console.WriteLine("resolution: " + Settings.GetValue("resolution"));
            Settings.SetValue("resolution", "2560x1440");
            Console.WriteLine("resolution: " + Settings.GetValue("resolution"));
            Settings.SetValue("A", "5");
            Console.WriteLine("A: " + Settings.GetValue("A"));
            Console.ReadLine();
        }
    }
}
