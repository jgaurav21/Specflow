using OpenQA.Selenium;
using SpecflowMozart.DTO;
using SpecflowMozart.Pages;
using System;
using TechTalk.SpecFlow;

namespace SpecflowMozart.Bases
{
    public class Base
    {
        //public BasePage CurrentPage
        //{
        //    get
        //    {
        //        return (BasePage)ScenarioContext.Current["currentPage"];
        //    }
        //    set
        //    {
        //        ScenarioContext.Current["currentPage"] = value;
        //    }
        //}

        private IWebDriver _driver { get; set; }

        public TPage GetInstance<TPage>() where TPage : BasePage, new()
        {
            TPage pageInstance = new TPage()
            {
                _driver = DriverContext.Driver
            };



            return pageInstance;
        }

        public TPage As<TPage>() where TPage : BasePage
        {
            return (TPage)this;
        }

        //public static LoginDTO login { get; set; }

    }
}
