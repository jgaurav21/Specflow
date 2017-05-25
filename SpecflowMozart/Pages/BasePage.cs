using OpenQA.Selenium;
using SpecflowMozart.Bases;
using SpecflowMozart.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecflowMozart.Pages
{
    public class BasePage : Base
    {
        #region By for wait

        By dashboardLoadingBy => By.Id("imgLoading-event");

        #endregion

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

        private IWebElement dashboardLoading => DriverContext.Driver.FindElement(dashboardLoadingBy);

        private IWebElement userInfo => DriverContext.Driver.FindElement(By.Id("userinfo"));
        #endregion

        #region Actions
        /// <summary>
        /// Click on leads button from menu
        /// </summary>
        public void ClickLeadsButton()
        {
            btnLeads.ClickWithJS();
            try
            {
                DriverContext.Driver.WaitForElementInvisible(dashboardLoading, 20);
            }
            catch (StaleElementReferenceException)
            {
                
            }

            //return GetInstance<LeadsPage>();
        }

        /// <summary>
        /// Click on menu
        /// </summary>
        public void ClickMenu()
        {
            btnMenu.Click();
        }

        /// <summary>
        /// Click on User Name drop down
        /// </summary>
        public void ClickUserName()
        {
            ddUserName.Click();
        }

       
        #endregion

        #region Methods

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
            if(!btnLeads.GetClass().ToLower().Contains("current"))
                ClickLeadsButton();

            

            return GetInstance<LeadsPage>();          
            

        }

        /// <summary>
        /// To click on user drop down
        /// </summary>
        public void OpenUserMenu()
        {
            if (!ddUserName.GetParentElement().GetClass().ToLower().Contains("open"))
            {
                // If user menu is not open
                //SeleniumHelper.HighlightElement(TestRunner.InsightDashboard.UserDropDownArrowButton(),1000);
                try
                {
                    ClickUserName();
                }
                catch (Exception)
                {

                    OpenUserMenu();
                }
            }
            //if (UserMenuLogoutLink().Displayed != true)
            //{
            //    OpenUserMenu();
            //}
        }

        /// <summary>
        /// This will open the respective pages from user menu options.
        /// </summary>
        /// <param name="option"></param>
        public T ClickOnUserMenuOption<T>(UserMenuOption option) where T : BasePage ,new()
        {
            //Click on down arrow button
            OpenUserMenu();

            string elementText = option.GetDescription();

            IWebElement element = DriverContext.Driver.GetElementByText(elementText);
            element.Click();
            return new T();
            

            //DriverContext.Driver
            //Click on input option
            //switch (option)
            //{
            //    case UserMenuOption.MyProfile:
            //        UserMenuMyProfileLink().Click();
            //        //Call method from respective page to wait till grid data is loaded.
            //        break;
            //    case UserMenuOption.ManageSearches:
            //        UserMenuManageSearchesLink().Click();
            //        //Wait for page load
            //        SeleniumHelper.WaitForPageReady(30);
            //        IWebElement header = TestRunner.InsightSettings.LeadsSearchesHeader();
            //        if (header.GetAttribute("class").Contains("collapsed"))
            //            header.Click();
            //        DriverContext.Driver.WaitForElementVisible(LeadsManageSearchGrid(), 60);
            //        SeleniumHelper.WaitForElementInvisible(TestRunner.ManageSearches.LeadsManageSearchLoading(), 60);
            //        break;
            //    case UserMenuOption.ManageUsers:
            //        UserMenuManageUsersLink().Click();
            //        //Call method from respective page to wait till grid data is loaded.
            //        TestRunner.ManageTeam.WaitForGridLoad();
            //        break;
            //    case UserMenuOption.ManageTeam:  //Added [Pragati G 3/15/2016 - Added for pulse team and also renamed ManageTeam]
            //        TestRunner.Pulse.PulseManageTeam().Click();
            //        SeleniumHelper.WaitForElementVisible(TestRunner.Pulse.NodeContainer(), 2000);
            //        break;
            //    case UserMenuOption.Settings:
            //        UserMenuSettingsLink().Click();
            //        //Call method from respective page to wait till grid data is loaded.
            //        //Ranjit P[4/7/2016]
            //        //TestRunner.InsightSettings.WaitForManageSearchGridExists();
            //        SeleniumHelper.WaitForElementVisible(TestRunner.InsightSettings.PreferredDefaultProductSectionDiv());
            //        break;
            //    case UserMenuOption.Help:
            //        UserMenuHelpLink().Click();
            //        //Call method from respective page to wait till grid data is loaded.
            //        break;
            //    case UserMenuOption.ContactUs:
            //        UserMenuContactUsLink().Click();
            //        //Call method from respective page to wait till grid data is loaded.
            //        break;
            //    case UserMenuOption.DocumentCenter:
            //        UserMenuDocumentCenterLink().Click();
            //        //Call method from respective page to wait till grid data is loaded.
            //        break;
            //    case UserMenuOption.Logout:
            //        UserMenuLogoutLink().Click();
            //        //Call method from respective page to wait till grid data is loaded.
            //        break;
            //}
        }



        #endregion

        #region Synchronization
        /// <summary>
        /// Wait for Home Page to load
        /// </summary>
        public void WaitForHomePageLoad()
        {

            //DriverContext.Driver.WaitForElementVisible(By.XPath("//a[contains(@class,'logo')]"), 60);
            WaitForSavedSearchGrid();
            WaitForGeoLoad();
            DriverContext.Driver.WaitForAjax();
            //DriverContext.Driver.WaitForElementVisibleQuick(btnLeads);
        }

        /// <summary>
        /// Wait for Saved Search grid on Dashboard
        /// </summary>
        public void WaitForSavedSearchGrid()
        {
            DriverContext.Driver.WaitForElementVisible("searchSummaryGrid-body", 30);
        }

        /// <summary>
        /// Wait for Geo location graph to load
        /// </summary>
        public void WaitForGeoLoad()
        {
            DriverContext.Driver.WaitForElementVisible("graphSection", 30);
        }
        #endregion
    }
}
