using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using SpecflowMozart.Bases;
using SpecflowMozart.Config;
using SpecflowMozart.DTO;
using SpecflowMozart.Extensions;
using SpecflowMozart.Helper;
using SpecflowMozart.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecflowMozart.Hooks
{
    public abstract class HookMethods : BaseStep
    {

        /// <summary>
        /// Open the browser
        /// </summary>
        /// <param name="browserType"></param>
        public void StartDriver(BrowserType browserType = BrowserType.FireFox)
        {
            switch (browserType)
            {
                case BrowserType.InternetExplorer:
                    DriverContext.Driver = new InternetExplorerDriver();
                    DriverContext.Browser = new Browser(DriverContext.Driver);
                    break;
                case BrowserType.FireFox:
                    DriverContext.Driver = new FirefoxDriver();
                    DriverContext.Browser = new Browser(DriverContext.Driver);
                    break;
                case BrowserType.Chrome:
                    var options = new ChromeOptions();
                    //options.BinaryLocation = @"D:\CMD\SpecflowMozart\SpecflowMozart\chromedriver.exe";

                    DriverContext.Driver = new ChromeDriver(options);
                    DriverContext.Browser = new Browser(DriverContext.Driver);
                    break;
            }

            

        }

        /// <summary>
        /// Navigate to the url
        /// </summary>
        /// <param name="url">url</param>
        public void NavigateToURL(string url)
        {
            DriverContext.Driver.Navigate().GoToUrl(url);
            DriverContext.Driver.WaitForPageLoaded();
        }

        /// <summary>
        /// Clean up the driver
        /// </summary>
        public static void CloseDriver()
        {
            if(DriverContext.Driver!=null)
            DriverContext.Driver.Quit();
            //DriverContext.Driver.Dispose();
            DriverContext.Driver = null;
        }

        /// <summary>
        /// Getting the test data based on settings
        /// </summary>
        public static LoginDTO GetTestData()
        {
            LoginDTO dtlogin = new LoginDTO();

            if (Settings.useLocalSettings.ToLower()!="yes")
            {
                
                dtlogin.userName = ExcelHelpers.ReadData(1, "username");
                dtlogin.password = ExcelHelpers.ReadData(1, "password");
                dtlogin.customer = ExcelHelpers.ReadData(1, "customer");
            }
            else
            {
                dtlogin.userName = Settings.userName;
                dtlogin.password = Settings.password;
                dtlogin.customer = Settings.customer;
            }

            return dtlogin;
        }
    }
}
