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
/// This file contains an implementation of the ILogger interface. It is used to define a custom file logger that adds date, time,
/// and thread information along with a log message to a log file. Based heavily on lab example.
/// 
///</summary>

using Microsoft.Extensions.Logging;

namespace FileLogger
{
    public class CustomLogger : ILogger
    {
        // File name for lod file
        private readonly string _file_name;
        // What class is creating the logger
        private readonly string _category_name;

        /// <summary>
        /// Constructor method for a new custom logger object.
        /// </summary>
        /// <param name="categoryName">The class creating the logger</param>
        public CustomLogger(string categoryName)
        {
            _category_name = categoryName;
            _file_name = $"Log_CS3500_{categoryName}_Assignment8";
        }

        // This method is used to add more information to log messages. For example, can
        // add tags describing what is being done. Can nest tags with several differrent
        // levels of processes if called multiple times. Info from: https://nblumhardt.com/2016/11/ilogger-beginscope/
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Custom log method for writing log messages to file. Writes date, time, thread, and the passed in message when called.
        /// </summary>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            // Calls extension to add date, time, and thread to start of message
            File.AppendAllText(_file_name, this.AppendDatetimeThread());
            File.AppendAllText(_file_name, $"{logLevel.ToString()[0..5]} - ");
            File.AppendAllText(_file_name, formatter(state, exception));
        }

    }
}