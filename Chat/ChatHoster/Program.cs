using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ChatHoster
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(Chat.Service)))
            {
                host.Open();
                Console.WriteLine("Хост стартовал!");
                Console.ReadLine();
            }
        }
    }
}
