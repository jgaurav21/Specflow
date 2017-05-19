using SpecflowMozart.Bases;
using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using SpecflowMozart.Pages;
using SpecflowMozart.ExtendedStep;
using SpecflowMozart.Helper;
using System.Collections.Generic;

namespace SpecflowMozart.Step
{
    [Binding]
    public class SearchTagStep : BaseStep
    {
        /// <summary>
        /// To apply filters valid for search tag
        /// </summary>
        [When(@"I apply filters for search tag")]
        public void WhenIApplyFiltersForSearchTag()
        {
            currentPage.As<LeadsPage>().ApplyFiltersForSearchTag();
            
        }

        /// <summary>
        /// To create a saved search
        /// </summary>
        [When(@"I create a saved search")]
        public void WhenICreateASavedSearch()
        {
            string searchName = currentPage.As<LeadsPage>().CreateSaveSearch();


        }


        [Then(@"I create a search with search tag")]
        public void ThenICreateASearchWithSearchTag()
        {
            ScenarioContext.Current.Pending();
        }

        /// <summary>
        /// To apply filters valid for search tag
        /// </summary>
        [Then(@"I apply search tag to filter")]
        public void ThenIApplySearchTagToFilter()
        {
            string searchName = currentPage.As<LeadsPage>().CreateSaveSearch();

            Dictionary<string, string> searchColor = new Dictionary<string, string>();
            searchColor.Add(searchName.Split('|')[0], searchName.Split('|')[1]);
            LogHelpers.Write("Search tag is created successfully");
        }


        /// <summary>
        /// To verify search tag on Manage searches page
        /// </summary>
        [Then(@"I verify search tag on Manage Searches page")]
        public void ThenIVerifySearchTagOnManageSearchesPage()
        {
            currentPage = currentPage.ClickOnUserMenuOption<ManageSearchesPage>(UserMenuOption.ManageSearches);
            currentPage.As<ManageSearchesPage>().WaitForPage();


        }

    }
}
