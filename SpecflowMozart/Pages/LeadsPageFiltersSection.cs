using OpenQA.Selenium;
using SpecflowMozart.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using SpecflowMozart.Extensions;

namespace SpecflowMozart.Pages
{
    public partial class LeadsPage : BasePage
    {
        private IWebElement filter;

        #region Elements

        private IWebElement moreFiltersSpan => DriverContext.Driver.FindElement(By.XPath(".//span[contains(text(),'More Filters')]"));

        private IWebElement addFiltersPopUp => DriverContext.Driver.FindElement(By.Id("winConfigFilters"));

        private List<IWebElement> filtersCheckboxes => addFiltersPopUp.FindElements(By.XPath("//*[contains(@class,'x-form-cb-label')]")).ToList();

        private IWebElement cancelButtonSpan => addFiltersPopUp.FindElement(By.Id("moreFilterCancel-btnEl"));

        private IWebElement saveAndCloseButtonSpan => addFiltersPopUp.FindElement(By.Id("moreFilterSaveClose-btnInnerEl"));

        private IWebElement projectValueFilterDiv => DriverContext.Driver.FindElement(By.Id("divProjectValueFilterGroup"));


        private IWebElement expandFilterIcon
        {
            get
            {
                return filter.FindElement(By.CssSelector(".icon-plus-filter"));
            }
            set
            {
                filter = value;
            }
        }

        private IWebElement minProjectValueInput => DriverContext.Driver.FindElement(By.Id("value-amount-min"));

        private IWebElement maxProjectValueInput => DriverContext.Driver.FindElement(By.Id("value-amount-max"));

        private IWebElement showProjectWithoutValuesCheckbox => DriverContext.Driver.FindElement(By.Id("chkShowProjectWithoutValus-inputEl"));


        #endregion Elements

        #region Actions

        /// <summary>
        /// Click on " + " icon on the filter
        /// </summary>
        public void ClickExpandFilter()
        {
            expandFilterIcon.Click();
        }

        public void EnterMinProjectValue(string minValue)
        {
            minProjectValueInput.Clear();
            minProjectValueInput.SendKeys(minValue);
        }

        public void EnterMaxProjectValue(string maxValue)
        {
            maxProjectValueInput.Clear();
            maxProjectValueInput.SendKeys(maxValue);
        }


        #endregion Actions

        #region Methods

        ///// <summary>
        ///// Expand the given filter
        ///// </summary>
        ///// <param name="filterToExpand">filter to be expanded</param>
        //public void ExpandFilter(IWebElement filterToExpand)
        //{
        //    filter = filterToExpand;

        //    ClickExpandFilter();
        //}

        /// <summary>
        /// Expand the specified filter
        /// </summary>
        /// <param name="filterName">filter name</param>
        public void ExpandFilter(string filterName)
        {
            switch(filterName)
            {
                case "Project Value":
                    filter = projectValueFilterDiv;
                    ClickExpandFilter();
                    break;
            }
        }

        public void ApplyFilters()
        {

        }

        public void ApplyFiltersForSearchTag()
        {
            int minValue = 1;
            int maxValue = 1000;
            if(!minProjectValueInput.Displayed)
            {
                ExpandFilter("Project Value");
            }

            while (int.Parse(GetSearchResultGridResultCount()) > 1000)
            {
                EnterMinProjectValue(minValue.ToString());
                ClickQuickSearchInput();
                DriverContext.Driver.WaitForPageLoaded();
                EnterMaxProjectValue(maxValue.ToString());
                ClickQuickSearchInput();
                DriverContext.Driver.WaitForPageLoaded();
            }
        }

        #endregion Methods
    }
}
