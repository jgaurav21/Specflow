using SpecflowMozart.Base;
using System;
using OpenQA.Selenium;
using SpecflowMozart.PopUps;
using SpecflowMozart.DTO;

namespace SpecflowMozart.Pages
{
    public partial class LeadsPage : BasePage
    {

        #region Elements
        private IWebElement resultTextSpan => DriverContext.Driver.FindElement(By.ClassName("result-text"));

        private IWebElement resultCount => DriverContext.Driver.FindElement(By.Id("resultCount"));

        private IWebElement quickSearchInput => DriverContext.Driver.FindElement(By.Id("demo-input-local"));

        private IWebElement saveSearchButtonSpan => DriverContext.Driver.FindElement(By.Id("btnSearch-btnInnerEl"));

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

        public string CreateSaveSearch()
        {
            dtoCreateSaveSearch createSearch = new dtoCreateSaveSearch();
            createSearch.gridOption = GridOptions.Project;
            createSearch.isSearchTag = true;
            string searchName = ClickSaveSearch().CreateSearch(createSearch);

            return searchName;
        }
        #endregion Methods
    }
}
