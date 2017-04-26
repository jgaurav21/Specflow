using OpenQA.Selenium;
using SpecflowMozart.Base;
using SpecflowMozart.Extensions;
using SpecflowMozart.Pages;
using System;
using System.Threading;

namespace SpecflowMozart.PopUps
{
    public class SaveSearchPopUp: BasePage
    {

        #region Elements

        private IWebElement saveSearchPopUp => DriverContext.Driver.FindElement(By.CssSelector(".common-popup.search-popup"));

        private IWebElement searchNameInput => saveSearchPopUp.FindElement(By.Id("txtSearchName-inputEl"));

        private IWebElement saveButton => saveSearchPopUp.FindElement(By.Id("btnSave-btnEl"));

        private IWebElement popUpLoadingImg => saveSearchPopUp.FindElement(By.Id("popup-loading-image"));

        private IWebElement popUpLoadingText => saveSearchPopUp.FindElement(By.Id("popup-loading-text"));
        #endregion Elements

        #region Actions

        /// <summary>
        /// clear the Search Name textbox
        /// </summary>
        public void ClearSearchNameInput() => searchNameInput.Clear();

        /// <summary>
        /// Enter the name of search
        /// </summary>
        /// <param name="searchName">name</param>
        public void EnterSearchName(string searchName) => searchNameInput.SendKeys(searchName);

        /// <summary>
        /// Click Save button
        /// </summary>
        public void ClickSaveButton() => saveButton.Click();
        #endregion Actions


        #region Methods

        /// <summary>
        /// To generate the name of search
        /// </summary>
        /// <param name="grid">Project or company grid</param>
        /// <returns>name of the new created search</returns>
        public string GenerateSearchName(GridOptions grid)
        {
            string searchName = string.Empty;
            if(grid==GridOptions.Project)
            {
               searchName = "PS_" + string.Format("{ 0:yyyymmddhhmmss}", DateTime.Now);
            }
            else
            {
                searchName = "CS_" + string.Format("{0:yyyymmddhhmmss}", DateTime.Now);
            }


            return searchName;
        }

        /// <summary>
        /// Create a new search
        /// </summary>
        /// <param name="grid">project / companies</param>
        /// <returns>search name</returns>
        public string CreateSearch(GridOptions grid)
        {
            
            string searchName = GenerateSearchName(grid);

            EnterSearchName(searchName);

            ClickSaveButton();

            Thread.Sleep(3000);

            return searchName;


        }
        #endregion Methods
    }
}
