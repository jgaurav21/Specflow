using OpenQA.Selenium;
using SpecflowMozart.Bases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpecflowMozart.Pages
{
    public class ManageSearchesPage : BasePage
    {
        #region Elements
        private IWebElement leadsManageSearchGrid => DriverContext.Driver.FindElement(by.Id("manageSearchGrid"));

        private List<IWebElement> leadsGridColumnHeader => leadsManageSearchGrid.FindElements(By.CssSelector(".x-leaf-column-header")).ToList();

        private IWebElement leadsColumnHeaderTrigger => leadsManageSearchGrid.FindElement(By.ClassName("x-column-header-trigger"));
        #endregion

        #region Actions


        #endregion

        #region Methods


        public int GetColumnIndex(ManageSearchesGrids grid,string colName)
        {
            int i;
            switch(grid)
            {
                case ManageSearchesGrids.Leads:
                    //i = leadsGridColumnHeader.FirstOrDefault(x => x.FindElement(By.ClassName("x-column-header-text-inner")).Text == colName)
                    break;
            }

            return i;
        }
        #endregion
    }
}
