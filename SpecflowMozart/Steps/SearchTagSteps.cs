using SpecflowMozart.Base;
using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using SpecflowMozart.Pages;

namespace SpecflowMozart.Steps
{
    public class SearchTagSteps : BaseStep
    {
        /// <summary>
        /// Login to the given application
        /// </summary>
        [Given(@"I login to Leads")]
        public void GivenILoginToLeads()
        {
            CurrentPage.GetInstance<LoginPage>();

            CurrentPage=CurrentPage.As<LoginPage>().Login(login.userName, login.password);

            CurrentPage = CurrentPage.As<HomePage>().ClickLeadsButton();
        }

        [When(@"I apply filters")]
        public void WhenIApplyFilters()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I create a saved search")]
        public void WhenICreateASavedSearch()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I apply search tag to filter")]
        public void ThenIApplySearchTagToFilter()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
