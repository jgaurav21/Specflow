using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using SpecflowMozart.Bases;
using SpecflowMozart.Pages;

namespace SpecflowMozart.ExtendedSteps
{
    [Binding]
    public class ExtendedBasePageSteps : BaseStep
    {

        /// <summary>
        /// Navigate to Manage searches page from user drop down
        /// </summary>
        [When(@"I navigate to Manage Searches page")]
        public void WhenINavigateToManageSearchesPage()
        {
            currentPage = currentPage.ClickOnUserMenuOption<ManageSearchesPage>(UserMenuOption.ManageSearches);
            currentPage.As<ManageSearchesPage>().WaitForPage();
        }

    }
}
