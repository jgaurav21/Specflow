using SpecflowMozart.Bases;
using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using SpecflowMozart.Pages;
using SpecflowMozart.ExtendedStep;
using SpecflowMozart.Helper;
using System.Collections.Generic;
using SpecflowMozart.DTO;
using AventStack.ExtentReports.Gherkin.Model;

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

            Report.scenario.CreateNode<When>($"Filters valid for search tag applied successfully.").Pass("Passed");

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

            Report.scenario.CreateNode<When>($"Saved Search {searchName} is created successfully.").Pass("Passed");



        }

        /// <summary>
        /// Create a search with search tag
        /// </summary>
        [Then(@"I create a search with search tag")]
        public void ThenICreateASearchWithSearchTag()
        {
            When("I create a saved search");
            Report.scenario.CreateNode<When>($"Search tag applied is applied to search successfully.").Pass("Passed");
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

            Report.scenario.CreateNode<When>($"Navigation to Manage Searches page is successful.").Pass("Passed");
            currentPage.As<ManageSearchesPage>().WaitForPage();

            //When(@"I navigate to Manage Searches page");

            string errorMessage = currentPage.As<ManageSearchesPage>().VerifySearchTagColorOfGivenSearches(searchColor);
            LogHelpers.Write(errorMessage);

            if(errorMessage.Length==0)
                Report.scenario.CreateNode<When>("Search tag is verified on Manage Seareches page successfully.").Pass("Passed");
            else
                Report.scenario.CreateNode<When>($"Search tag is not verified on Manage Seareches page - {errorMessage} ").Pass("Failed");

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
