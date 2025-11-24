using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AppDocumentManagment.NetworkHost
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var Host = new ServiceHost(typeof(Net.DocumentManagmentService)))
            {
                Host.Open();
                Console.WriteLine("Выполнен запуск хоста AppDocumentManagment");
                Console.ReadLine();
            }
        }
    }
}
