using SpecflowMozart.Bases;
using System;
using OpenQA.Selenium;
using SpecflowMozart.PopUps;
using SpecflowMozart.DTO;
using SpecflowMozart.Extensions;

namespace SpecflowMozart.Pages
{
    public partial class LeadsPage : BasePage
    {

        #region Elements
        private IWebElement resultTextSpan => DriverContext.Driver.FindElement(By.ClassName("result-text"));

        private IWebElement resultCount => DriverContext.Driver.FindElement(By.Id("resultCount"));

        private IWebElement quickSearchInput => DriverContext.Driver.FindElement(By.Id("demo-input-local"));

        private IWebElement saveSearchButtonSpan => DriverContext.Driver.FindElement(By.Id("btnSearch-btnInnerEl"));

        private IWebElement leadsSearchGridLoading => DriverContext.Driver.FindElement(By.XPath("//div[@class='x-mask-msg-text' and text()='Loading...']"));


        #endregion Elements

        #region Actions

        public string GetSearchResultGridResultCount()
        {
            return resultCount.Text;
        }

        public void ClickQuickSearchInput()
        {
            quickSearchInput.Click();
        }

        public SaveSearchPopUp ClickSaveSearch()
        {
            saveSearchButtonSpan.Click();
            return new SaveSearchPopUp();
        }

        #endregion Actions

        #region Methods

        /// <summary>
        /// Wait for the grid to refresh
        /// </summary>
        /// <param name="i"></param>
        public void WaitForGridRefresh(int i=0)
        {
            try
            {
                DriverContext.Driver.WaitForElementInvisible(leadsSearchGridLoading, 120);
            }
            catch
            {
                try
                {
                    if (i < 3)
                    {
                        WaitForGridRefresh(i + 1);
                    }
                }
                catch { }
            }
        }


        /// <summary>
        /// Create a saved search
        /// </summary>
        /// <returns></returns>
        public string CreateSaveSearch(CreateSavedSearchFilters createSearch)
        {
            
            string searchName = ClickSaveSearch().CreateSearch(createSearch);

            return searchName;
        }
        #endregion Methods
    }
}
