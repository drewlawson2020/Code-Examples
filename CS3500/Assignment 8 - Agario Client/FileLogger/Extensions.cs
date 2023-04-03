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
/// This file contains an extension method used to append the date, time, and calling thread to a log file.
/// 
///</summary>

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLogger
{
    public static class Extensions
    {

        /// <summary>
        /// Returns a string containing date, time, and thread that called the method. Used to append
        /// this information to a log file.
        /// </summary>
        /// <param name="logger">The logger that called the method</param>
        /// <returns>A string containing the date, time, and currently managed thread</returns>
        public static string AppendDatetimeThread(this ILogger logger)
        {
            return $"{DateTime.Now} ({Thread.CurrentThread.ManagedThreadId}) - ";
        }

    }
}
