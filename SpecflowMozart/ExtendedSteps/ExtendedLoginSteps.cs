using SpecflowMozart.Bases;
using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using SpecflowMozart.Pages;
using SpecflowMozart.Extensions;

namespace SpecflowMozart.ExtendedStep
{
    [Binding]
    public class ExtendedLoginStep : BaseStep
    {
        /// <summary>
        /// Login to the given application
        /// </summary>
        [Given(@"I login to (.*)")]
        public void GivenILoginToLeads(Product product)
        {


            currentPage = login.Login<BasePage>(dtLogin.userName, dtLogin.password);
            currentPage.WaitForHomePageLoad();

            currentPage = currentPage.As<BasePage>().NavigateToLeads();
            DriverContext.Driver.WaitForPageLoaded();
            currentPage.As<LeadsPage>().WaitForGridRefresh();
            
        }

       


    }



}
