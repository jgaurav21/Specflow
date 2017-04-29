using OpenQA.Selenium;

namespace SpecflowMozart.Bases
{
    public class DriverContext : Base
    {
        public static Browser Browser { get; set; }

        private static IWebDriver _driver;

        public static IWebDriver Driver
        {
            get
            {
                return _driver;
            }
            set
            {
                _driver = value;
            }
        }
    }
}