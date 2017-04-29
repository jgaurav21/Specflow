using SpecflowMozart.Bases;
using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using SpecflowMozart.Pages;
using SpecflowMozart.ExtendedStep;
using SpecflowMozart.Helper;

namespace SpecflowMozart.Step
{
    [Binding]
    public class SearchTagStep : BaseStep
    {
        [When(@"I apply filters for search tag")]
        public void WhenIApplyFiltersForSearchTag()
        {
            currentPage.As<LeadsPage>().ApplyFiltersForSearchTag();
            
        }


        [When(@"I create a saved search")]
        public void WhenICreateASavedSearch()
        {
            string searchName = currentPage.As<LeadsPage>().CreateSaveSearch();


        }

        [Then(@"I apply search tag to filter")]
        public void ThenIApplySearchTagToFilter()
        {
            string searchName = currentPage.As<LeadsPage>().CreateSaveSearch();
            LogHelpers.Write("Search tag is created successfully");
        }

    }
}
