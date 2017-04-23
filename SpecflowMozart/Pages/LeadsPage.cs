using SpecflowMozart.Base;
using System;
using OpenQA.Selenium;

namespace SpecflowMozart.Pages
{
    public partial class LeadsPage : BasePage
    {

        #region Elements
        private IWebElement resultTextSpan => DriverContext.Driver.FindElement(By.ClassName("result-text"));

        private IWebElement resultCount => DriverContext.Driver.FindElement(By.Id("resultCount"));

        private IWebElement quickSearchInput => DriverContext.Driver.FindElement(By.Id("demo-input-local"));

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
        #endregion Actions

        #region Methods

        #endregion Methods
    }
}
