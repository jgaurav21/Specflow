using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using SpecflowMozart.Base;
using SpecflowMozart.Config;
using SpecflowMozart.DTO;
using SpecflowMozart.Extensions;
using SpecflowMozart.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecflowMozart.Hooks
{
    public abstract class HookMethods: ConfigReader
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
                    options.BinaryLocation = @"D:\CMD\SpecflowMozart\SpecflowMozart\chromedriver.exe";

                    DriverContext.Driver = new ChromeDriver(options);
                    DriverContext.Browser = new Browser(DriverContext.Driver);
                    break;
            }

            

        }

        public void NavigateToURL(string url)
        {
            DriverContext.Driver.Navigate().GoToUrl(url);
            DriverContext.Driver.WaitForPageLoaded();
        }

        public void CloseDriver()
        {
            DriverContext.Driver.Close();
        }

        public void GetTestData()
        {
            if(Settings.useLocalSettings.ToLower()!="yes")
            {
                LoginDTO login = new LoginDTO();
                ExcelHelpers.PopulateInCollection("Data.xlsx");

                login.userName = ExcelHelpers.ReadData(1, "username");
            }
        }
    }
}
