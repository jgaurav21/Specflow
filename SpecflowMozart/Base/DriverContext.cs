using OpenQA.Selenium;

namespace SpecflowMozart.Base
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