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
        private IWebElement ddUserName => DriverContext.Driver.FindElement(By.Id("userName"));

        private IWebElement insightLogo => DriverContext.Driver.FindElement(By.XPath("//a[contains(@class,'logo')]"));

        private IWebElement dashboardLoading => DriverContext.Driver.FindElement(By.Id("imgLoading-event"));

        private IWebElement userInfo => DriverContext.Driver.FindElement(By.Id("userinfo"));
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

        /// <summary>
        /// Click on User Name drop down
        /// </summary>
        public void ClickUserName()
        {
            ddUserName.Click();
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

        /// <summary>
        /// To click on user drop down
        /// </summary>
        public void OpenUserMenu()
        {
            if (ddUserName.GetAttribute("class").ToLower().Contains("open"))
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
    }
}
