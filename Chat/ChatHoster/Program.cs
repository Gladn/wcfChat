using Chat;
using System;
using System.ServiceModel;

namespace ChatHoster
{
    internal class Program
    {
        static ServiceHost host = null;
        static Service service = null;
        static void Main(string[] args)
        {

            service = new Service();
            service.RepositoryDirectory = "storage";

            service.FileRequested += new FileEventHandler(Service_FileRequested);
            service.FileUploaded += new FileEventHandler(Service_FileUploaded);
            service.FileDeleted += new FileEventHandler(Service_FileDeleted);

            host = new ServiceHost(service);
            host.Faulted += new EventHandler(Host_Faulted);

            try
            {
                host.Open();
                Console.WriteLine("Press a key to close the service");
                Console.ReadKey();
            }
            finally
            {
                host.Close();
            }
        }

        static void Host_Faulted(object sender, EventArgs e)
        {
            Console.WriteLine("Host faulted; reinitialising host");
            host.Abort();
        }

        static void Service_FileRequested(object sender, FileEventArgs e)
        {
            Console.WriteLine(string.Format("File access\t{0}\t{1}", e.VirtualPath, DateTime.Now));
        }

        static void Service_FileUploaded(object sender, FileEventArgs e)
        {
            Console.WriteLine(string.Format("File upload\t{0}\t{1}", e.VirtualPath, DateTime.Now));
        }

        static void Service_FileDeleted(object sender, FileEventArgs e)
        {
            Console.WriteLine(string.Format("File deleted\t{0}\t{1}", e.VirtualPath, DateTime.Now));
        }

    }
}
