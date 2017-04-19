using SpecflowMozart.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecflowMozart.Config
{
    public class Settings
    {
        public static string useLocalSettings { get; set; }

        public static int Timeout { get; set; }

        public static string IsReporting { get; set; }

        public static string TestType { get; set; }

        public static string AUT { get; set; }

        public static string BuildName { get; set; }

        public static BrowserType BrowserType { get; set; }

        public static string IsLog { get; set; }

        public static string LogPath { get; set; }

        public static string Environment { get; set; }

        public static string userName { get; set; }

        public static string password { get; set; }

        public static string customer { get; set; }


    }
}
