using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using SpecflowMozart.Config;

namespace SpecflowMozart.Helper
{
    public class LogHelpers
    {
        //Global Declaration
        private static string _logFileName = string.Format("{0:yyyymmddhhmmss}", DateTime.Now);
        private static StreamWriter _streamw = null;

        //Create a file which can store the log information
        public static void CreateLogFile(string name)
        {
            string dir = Settings.LogPath;
            if (Directory.Exists(dir))
            {
                _streamw = File.AppendText($"{dir}{name}_{_logFileName}.log");
            }
            else
            {
                Directory.CreateDirectory(dir);
                _streamw = File.AppendText($"{dir}{name}_{_logFileName}.log");
            }
        }



        //Create a method which can write the text in the log file
        public static void Write(string logMessage)
        {
            _streamw.Write("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            _streamw.WriteLine("    {0}", logMessage);
            _streamw.Flush();
        }


    }
}
