using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using System.Net.Http;
using System.Timers;


namespace PrintServer
{
    /// <summary>
    /// http://www.asp.net/web-api/overview/hosting-aspnet-web-api/use-owin-to-self-host-web-api
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";

            Console.Write("PrintServer URL: ");
            var url = Console.ReadLine();
            if (!string.IsNullOrEmpty(url))
            {
                Uri uri = null;
                if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    Uri.TryCreate(url, UriKind.Absolute, out uri);
                }
                if (uri != null)
                {
                    baseAddress = url;
                }
            }
            
            var app = WebApp.Start<Startup>(url: baseAddress);
            Console.WriteLine("PrintServer started");
            Console.WriteLine("Listening on " + baseAddress);
            Console.ReadLine();
            app.Dispose();
        }
    }
}
