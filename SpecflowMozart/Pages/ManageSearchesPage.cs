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

        /// <summary>
        /// To get the column index of the given column
        /// </summary>
        /// <param name="grid">grid to search</param>
        /// <param name="colName">column Name</param>
        /// <returns></returns>
        public int GetColumnIndex(ManageSearchesGrids grid,string colName)
        {
            int counter=0;
            switch(grid)
            {
                case ManageSearchesGrids.Leads:
                    for(int i=0;i < leadsGridColumnHeader.Count;i++)
                    {
                        if(leadsGridColumnHeader[i].FindElement(By.ClassName("x-column-header-text-inner")).Text == colName)
                        {
                            counter = i;
                            break;
                        }
                    }
                    
                break;
            }

            return counter;
        }


        #endregion
    }
}
