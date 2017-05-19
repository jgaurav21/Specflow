using NUnit.Framework;
using OpenQA.Selenium;
using SpecflowMozart.Bases;
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

        private IWebElement colorPicker => DriverContext.Driver.FindElement(By.XPath(".//input[@id='color-picker']"));

        #endregion Elements

        #region Actions

        /// <summary>
        /// Select the given color
        /// </summary>
        /// <param name="color"></param>
        public string SelectColor(string color)
        {
            IWebElement element = colors.FirstOrDefault(x => x.GetAttribute("class").ToLower().Contains(color.ToLower()));

            element.Click();

            return colorPicker.GetAttribute("value");
        }

        #endregion Actions


        #region Methods

        #endregion
    }
}
