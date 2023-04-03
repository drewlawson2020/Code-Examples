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
/// This file contains an implementation of the ILoggerProvider interface. It is used to provide a logger object through
/// dependency injection when needed. Based heavily on lab example.
/// 
///</summary>

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLogger
{
    public class CustomFileLogProvider : ILoggerProvider
    {
        private CustomLogger? _logger;

        /// <summary>
        /// Method used to make a new custom logger object through constructor for CustomLogger.
        /// </summary>
        /// <param name="categoryName">The log category</param>
        /// <returns>A logger object</returns>
        public ILogger CreateLogger(string categoryName)
        {
            _logger = new CustomLogger(categoryName);
            return _logger;
        }

        /// <summary>
        /// Left unimplemented like in lab. We used the "easy" method for the logger and did not
        /// want to accidentally break anything by implementing it.
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
