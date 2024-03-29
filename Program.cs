﻿/************************************************/
/*************   Trade Secret    ****************/
/************************************************/

using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

#region ChangeComments
/* IssueNo - DeveloperName - Date : Comment    */
#endregion

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://localhost:4000")
                .Build();
    }
}
