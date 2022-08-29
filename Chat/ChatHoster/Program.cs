using System;
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
