using NUnit.Framework;
using OpenQA.Selenium;
using SpecflowMozart.Base;
using SpecflowMozart.Pages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpecflowMozart.PopUps
{
    public class SearchTagSelector : BasePage
    {
        #region Elements

        private IWebElement searchTagColorPopup => DriverContext.Driver.FindElement(By.CssSelector(".dropdown-menu.color-menu"));

        private List<IWebElement> colors => searchTagColorPopup.FindElements(By.CssSelector("ul>li>a")).ToList();

        #endregion Elements

        #region Actions

        /// <summary>
        /// Select the given color
        /// </summary>
        /// <param name="color"></param>
        public void SelectColor(string color)
        {
            IWebElement element = colors.FirstOrDefault(x => x.GetAttribute("class").ToLower().Contains(color.ToLower()));

            element.Click();
        }

        #endregion Actions
    }
}
