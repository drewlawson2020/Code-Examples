/// <summary>
/// Author: Drew Lawson and Peter Bruns
/// Team: NoSleep
/// Date: April 4, 2022
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright CS 3500, Drew Lawson, and Peter Bruns - This work may not be copied for use in Academic Coursework
/// 
/// We, Drew Lawson and Peter Bruns, certify that I wrote this code from scratch and did not copy it in part or whole from
/// another source. All references used in the completion of the assignment are cited in my README file.
/// 
/// File Contents
/// 
/// File used to initialize a ChatClientGUI and add the CustomFileLogger to services to allow for dependency injection.
/// 
///</summary>

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FileLogger;
using System;

namespace ChatClient
{
    internal static class Program
    {

        /// <summary>
        /// Method used to add a custom file logger to the services for dependency injection. Method based on
        /// the lab example.
        /// </summary>
        /// <param name="services">The services collection for the object to which the logger will be added</param>
        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(configure =>
            {
                configure.AddConsole();
                configure.AddDebug();
                // Add the custom file logger defined in FileLogger to the services
                configure.AddProvider(new CustomFileLogProvider());
                configure.SetMinimumLevel(LogLevel.Debug);
            });
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            services.AddScoped<ChatClientGUI>();
            // Get the required serviced for dependency injection of the logger
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var gui = serviceProvider.GetRequiredService<ChatClientGUI>();
                Application.Run(gui);
            }
        }
    }
}