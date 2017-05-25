using SpecflowMozart.Bases;
using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using SpecflowMozart.Pages;
using SpecflowMozart.ExtendedStep;
using SpecflowMozart.Helper;
using System.Collections.Generic;
using SpecflowMozart.DTO;

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
            CreateSavedSearchFilters createSearch = new CreateSavedSearchFilters();
            createSearch.gridOption = GridOptions.Project;
            createSearch.isSearchTag = true;

            string searchName = currentPage.As<LeadsPage>().CreateSaveSearch(createSearch);
            ScenarioContext.Current.Add("searchName", searchName);
            LogHelpers.Write("Search is created successfully");



        }

        /// <summary>
        /// Create a search with search tag
        /// </summary>
        [Then(@"I create a search with search tag")]
        public void ThenICreateASearchWithSearchTag()
        {
            When("I create a saved search");
        }

        /// <summary>
        /// To apply filters valid for search tag
        /// </summary>
        [Then(@"I apply search tag to filter")]
        public void ThenIApplySearchTagToFilter()
        {

            When("I create a saved search");
        }

        /// <summary>
        /// To verify search tag on Manage searches page
        /// </summary>
        [Then(@"I verify search tag on Manage Searches page")]
        public void ThenIVerifySearchTagOnManageSearchesPage()
        {
            //Getting the searchname and color code of the search created earlier
            string searchName = ScenarioContext.Current["searchName"].ToString();
            Dictionary<string, string> searchColor = new Dictionary<string, string>();
            searchColor.Add(searchName.Split('|')[0], searchName.Split('|')[1]);

            // Verifying search tag on Manage searches page
            currentPage = currentPage.ClickOnUserMenuOption<ManageSearchesPage>(UserMenuOption.ManageSearches);
            currentPage.As<ManageSearchesPage>().WaitForPage();

            //When(@"I navigate to Manage Searches page");

            string errorMessage = currentPage.As<ManageSearchesPage>().VerifySearchTagColorOfGivenSearches(searchColor);
            LogHelpers.Write(errorMessage);

        }

        /// <summary>
        /// Create a search having search tags
        /// </summary>
        /// <param name="p0"></param>
        [When(@"I create (.*) search tag")]
        public void WhenICreateSearchTag(int p0)
        {
            for(int i=0;i<p0;i++)
            {
                When(@"I apply filters for search tag");
                LogHelpers.Write("Filters are applied successfully.");

                Then(@"I create a search with search tag");

                LogHelpers.Write("Search tags are applied successfully.");

            }
        }



    }
}
