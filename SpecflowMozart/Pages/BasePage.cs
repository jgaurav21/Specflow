using OpenQA.Selenium;
using SpecflowMozart.Bases;
using SpecflowMozart.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecflowMozart.Pages
{
    public class BasePage : Base
    {
        //#region Properties
        //public TPage As<TPage>() where TPage : BasePage
        //{
        //    return (TPage)this;
        //}


        //private IWebDriver _driver { get; set; }

        //public TPage GetInstance<TPage>() where TPage : BasePage, new()
        //{
        //    TPage pageInstance = new TPage()
        //    {
        //        _driver = DriverContext.Driver
        //    };
            
        //    return pageInstance;
        //}

        //#endregion Properties

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

        private IWebElement insightLogo => DriverContext.Driver.FindElement(By.XPath("//a[contains(@class,'logo')]"));

        private IWebElement dashboardLoading => DriverContext.Driver.FindElement(By.Id("imgLoading-event"));


        #endregion

        #region Actions
        /// <summary>
        /// Click on leads button from menu
        /// </summary>
        public void ClickLeadsButton()
        {
            btnLeads.ClickWithJS();
            DriverContext.Driver.WaitForElementInvisible(dashboardLoading, 20);

            //return GetInstance<LeadsPage>();
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
        
        public void WaitForHomePageLoad()
        {
            
            DriverContext.Driver.WaitForElementVisible(By.XPath("//a[contains(@class,'logo')]"), 60);
            DriverContext.Driver.WaitForAjax();
            DriverContext.Driver.WaitForElementVisibleQuick(btnLeads);
        }

        /// <summary>
        /// Navigate to Leads Page
        /// </summary>
        public LeadsPage NavigateToLeads()
        {
            
            // Click on menu button if leads page is not displayed
            if (!btnLeads.Displayed)
            {
                ClickMenu();
            }

            DriverContext.Driver.WaitForAjax();
            
            ClickLeadsButton();

            

            return GetInstance<LeadsPage>();          
            

        }


        
        #endregion
    }
}
