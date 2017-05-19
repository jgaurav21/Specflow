using OpenQA.Selenium;
using SpecflowMozart.Bases;
using SpecflowMozart.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpecflowMozart.Pages
{
    public class ManageSearchesPage : BasePage
    {
        #region By for pageload

        By leadsSearchesGridBy => By.Id("manageSearchGrid");
        #endregion
        #region Elements
        private IWebElement leadsManageSearchGrid => DriverContext.Driver.FindElement(By.Id("manageSearchGrid"));

        private List<IWebElement> leadsGridColumnHeader => leadsManageSearchGrid.FindElements(By.CssSelector(".x-leaf-column-header")).ToList();

        private IWebElement leadsColumnHeaderTrigger => leadsManageSearchGrid.FindElement(By.ClassName("x-column-header-trigger"));

        private IWebElement resultsPerPageValue
        {
            get
            {
                return resultsPerPageValue.FindElement(By.XPath(".//div[contains(@class,'x-form-arrow-trigger-default')]/preceding-sibling::div/input"));
            }
            set
            {
                resultsPerPageValue = value;
            }
        }

        private List<IWebElement> rowsWithSearchTag => DriverContext.Driver.FindElements(By.XPath(".//div[@class='input-group pick-a-color-markup']//span[@class='display-none']/../ancestor::tr")).ToList();

        private IWebElement leadsManageSearchGridLoading => DriverContext.Driver.FindElement(By.ClassName("manage-searches-grid-loading"));
        
        #endregion

        #region Actions
        /// <summary>
        /// Get Results per page value
        /// </summary>
        /// <param name="grid">grid</param>
        /// <returns>results per page</returns>
        public string GetResultPerPageValue(IWebElement grid)
        {
            return (resultsPerPageValue = grid).GetAttribute("value");
        }
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

        /// <summary>
        /// Verify the search tag color for given search
        /// </summary>
        /// <param name="searchColor"> Key = Search Name and Value = Color Code</param>
        /// <returns>error message</returns>
        public string VerifySearchTagColorOfGivenSearches(Dictionary<string, string> searchColor)
        {
            string errorMessage = string.Empty;


            int pageCount = GetManageGridPageCount(leadsManageSearchGrid);
            int j = 1;
            bool flag = false;


            while (j <= pageCount)
            {
                List<IWebElement> rows = rowsWithSearchTag;
                foreach (IWebElement row in rows)
                {
                    string searchNames = row.FindElement(By.XPath(".//td[contains(@class,'manage-search-name')]/div")).Text;
                    if (searchColor.ContainsKey(searchNames))
                    {
                        string searchTag = row.FindElement(By.XPath(".//td[contains(@class,'colColorPicker')]//input[@class='pick-a-color']")).GetAttribute("value");
                        if (searchColor[searchNames] != searchTag)
                        {
                            errorMessage = errorMessage + " Fail: color not matching for search :  " + searchNames + ". ";
                        }
                        else
                        {
                            errorMessage = errorMessage + " " + searchNames + " ";
                        }
                        searchColor.Remove(searchNames);
                        if (searchColor.Count == 0)
                        {
                            flag = true;
                        }
                        //break;
                    }
                }
                if (flag)
                {
                    break;
                }

                if (j < pageCount)
                {
                    //GotoNextPage(LeadsManageSearchGrid());

                }
                j++;
            }

            return errorMessage;
        }

        /// <summary>
        /// To get the grid page count
        /// </summary>
        /// <param name="grid">Grid</param>
        /// <returns></returns>
        public int GetManageGridPageCount(IWebElement grid)
        {
            double pageCount = 0;
            double count;
            count = GetTotalRecordCount(grid) / double.Parse(GetResultPerPageValue(grid));
            pageCount = Math.Ceiling(count);

            return Convert.ToInt32(pageCount);
        }


        /// <summary>
        /// Get Total record count on the grid
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public double GetTotalRecordCount(IWebElement grid)
        {
            double recordCount = 0;

            string recordCountText = DriverContext.Driver.RunJavaScript("Ext.getCmp('" + grid.GetAttribute("id") + "').getStore().totalCount", DriverContext.Driver.CurrentWindowHandle);
            if (recordCountText != String.Empty)
            {
                recordCount = double.Parse(recordCountText);
            }
            //return Convert.ToInt32(recordCount);
            return recordCount;
        }

        /// <summary>
        /// Waitfor Manage Searches Page to load
        /// </summary>
        /// <typeparam name="ManageSearchesPage"></typeparam>
        public void WaitForPage()
        {
            DriverContext.Driver.WaitForElementVisible(leadsSearchesGridBy, 60);
            DriverContext.Driver.WaitForElementInvisible(leadsManageSearchGridLoading, 60);
        }
        #endregion
    }
}
