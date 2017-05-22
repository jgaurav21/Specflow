using OpenQA.Selenium;
using SpecflowMozart.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using SpecflowMozart.Extensions;
using SpecflowMozart.Helper;

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

        private IWebElement checkShowProjectWithoutValues => DriverContext.Driver.FindElement(By.Id("chkShowProjectWithoutValus-displayEl"));
        #endregion Elements

        #region Actions

        /// <summary>
        /// Click on " + " icon on the filter
        /// </summary>
        public void ClickExpandFilter()
        {
            expandFilterIcon.Click();
        }

        /// <summary>
        /// Enter min project value 
        /// </summary>
        /// <param name="minValue">value</param>
        public void EnterMinProjectValue(string minValue)
        {
            minProjectValueInput.Clear();
            minProjectValueInput.SendKeys(minValue);
        }

        /// <summary>
        /// Enter Max project value
        /// </summary>
        /// <param name="maxValue">value</param>
        public void EnterMaxProjectValue(string maxValue)
        {
            maxProjectValueInput.Clear();
            maxProjectValueInput.SendKeys(maxValue);
        }

        /// <summary>
        /// Check the Show project without value checkbox
        /// </summary>
        public void ClickShowProjectWithNoVlaueCheckbox()
        {
            showProjectWithoutValuesCheckbox.Click();
        }

        /// <summary>
        /// Clear out max project value
        /// </summary>
        public void ClearMaxProjectValueEdit()
        {
            maxProjectValueInput.Clear();
        }

        #endregion Actions

        #region Methods

        
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

        ////public void EnableFilter(string[] filterList)
        //{
        //    bool enabled = false;
        //    IWebElement selectFilter = null;
        //    //Open 'Add Filters' pop-up
        //    DriverContext.Driver.ClickWithJS(moreFiltersSpan);

        //    //Wait for pop-up to load
        //    SeleniumHelper.WaitForElementVisible(WinConfigFilters(), 10);

        //    if (filterList[0].ToLower().Equals("all"))
        //    {
        //        selectFilter = WinConfigFilters().FindElement(By.Id("chkAllFilter"));
        //        if (!selectFilter.GetAttribute("class").Contains("checked"))
        //        {
        //            selectFilter.FindElement(By.XPath(".//span[contains(text(),'All')]")).Click(); //Ranjit P[12/9/2015]: Modified for sencha changes.
        //            enabled = true;
        //        }
        //    }
        //    else
        //    {
        //        selectFilter = WinConfigFilters().FindElement(By.Id("chkAllFilter"));
        //        if (selectFilter.GetAttribute("class").Contains("checked")) // Remove all filters.
        //            selectFilter.FindElement(By.XPath(".//span[contains(text(),'All')]")).Click();//Ranjit P[12/9/2015]: Modified for sencha changes.
        //        else
        //        {
        //            //Click twice to un-check all checked filters for fresh selection.
        //            //Ranjit P[12/9/2015]: Modified for sencha changes.
        //            //selectFilter.FindElement(By.TagName("input")).Click();
        //            //selectFilter.FindElement(By.TagName("input")).Click();
        //            selectFilter.FindElement(By.XPath(".//span[contains(text(),'All')]")).Click();
        //            selectFilter.FindElement(By.XPath(".//span[contains(text(),'All')]")).Click();
        //        }

        //        //Select required filters to enable
        //        foreach (string filter in filterList)
        //        {
        //            if (Enums.Filter.IsDefined(typeof(Enums.Filter), filter.Replace(" ", ""))) //If the given filter in the list exists.
        //            {
        //                selectFilter = WinConfigFilters().FindElement(By.XPath(".//label[contains(text(),'" + filter + "')]"));

        //                //If filter is already not enabled then enable.
        //                if (!selectFilter.FindElement(By.XPath("../../../..")).GetAttribute("class").Contains("checked"))
        //                {
        //                    selectFilter.Click();
        //                    enabled = true;
        //                }
        //            }
        //        }
        //    }

        //    if (enabled)
        //        WinConfigFilters().FindElement(By.Id("moreFilterSaveClose")).FindElements(By.TagName("span"))[1].Click(); //Click filter save and close button
        //    else
        //        WinConfigFilters().FindElement(By.Id("moreFilterCancel")).FindElements(By.TagName("span"))[1].Click(); //Click filter cancel button.

        //    SeleniumHelper.WaitForElementVisible("panelErrorText", 10);
        //    SeleniumHelper.WaitForElementInvisible("panelErrorText", 10);
        //    //SeleniumHelper.WaitForPageReady(10);
        //}


        /// <summary>
        /// Apply filters valid for search tag
        /// </summary>
        public void ApplyFiltersForSearchTag()
        {
            int minValue = 1;
            int maxValue = 1000;
            int counter = 1;
            if(!minProjectValueInput.Displayed)
            {
                ExpandFilter("Project Value");
            }

            EnterMinProjectValue(minValue.ToString());
            ClickQuickSearchInput();
            WaitForGridRefresh();
            checkShowProjectWithoutValues.Click();
            WaitForGridRefresh();
            while (double.Parse(GetSearchResultGridResultCount()) > 1000)
            {
                ClearMaxProjectValueEdit();
                EnterMaxProjectValue(maxValue.ToString());
                ClickQuickSearchInput();
                WaitForGridRefresh();

                maxValue = maxValue / 10;
                counter++;
                if(counter>5)
                {
                    break;
                }
            } 

            LogHelpers.Write($"Min Project Value : {minValue} and Max Project Value : {maxValue}");
        }

        #endregion Methods
    }
}
