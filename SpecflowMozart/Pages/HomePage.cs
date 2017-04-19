using OpenQA.Selenium;
using SpecflowMozart.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecflowMozart.Pages
{
    public class HomePage : BasePage
    {

        #region Elements

        // menu button on left top corner
        private IWebElement btnMenu => DriverContext.Driver.FindElement(By.Id("btnMenu"));

        // Snaphot menu button
        private IWebElement btnSnapshot => DriverContext.Driver.FindElement(By.Id("sideDashboard"));

        // Forecast menu button
        private IWebElement btnForecast => DriverContext.Driver.FindElement(By.Id("sideForecast"));

        // Analyze menu button
        private IWebElement btnAnalyze => DriverContext.Driver.FindElement(By.Id("sideAnalyze"));

        // Leads menu button
        private IWebElement btnLeads => DriverContext.Driver.FindElement(By.Id("sideIdentify"));

        // Track menu button
        private IWebElement btnTrack => DriverContext.Driver.FindElement(By.Id("sideTrack"));

        // Export side menu
        private IWebElement btnExport => DriverContext.Driver.FindElement(By.Id("sideExport"));

        // pulse menu button
        private IWebElement btnPulse => DriverContext.Driver.FindElement(By.Id("sidePulse"));

        // menu bar
        private IWebElement menuBar => DriverContext.Driver.FindElement(By.Id("sideMenu"));

        // user name drop down
        private IWebElement ddUserName => DriverContext.Driver.FindElement(By.Id("username"));

        #endregion

        #region Actions
        /// <summary>
        /// Click on leads button from menu
        /// </summary>
        public LeadsPage ClickLeadsButton()
        {
            btnLeads.Click();
            return new LeadsPage();
        }

        /// <summary>
        /// Click on menu
        /// </summary>
        public void ClickMenu()
        {
            btnMenu.Click();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Navigate to Leads Page
        /// </summary>
        public void NavigateToLeads()
        {
            // Click on menu button if leads page is not displayed
            if (!btnLeads.Displayed)
            {
                ClickMenu();
            }
            ClickLeadsButton();

            // wait for page to load
            //DriverContext.Driver.WaitForPageLoaded();

        }
        #endregion
    }
}
