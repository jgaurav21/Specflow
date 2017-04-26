using OpenQA.Selenium;
using SpecflowMozart.DTO;
using SpecflowMozart.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecflowMozart.Base
{
    public class BaseStep : Steps
    {

        #region Properties
        //private LoginPage login;
        public LoginPage login
        {
            get
            {
                return (LoginPage)ScenarioContext.Current["current"];
            }
            set
            {
                ScenarioContext.Current["current"] = value;
            }

        }

        //private BasePage _currentPage;

        public BasePage currentPage
        {
            get
            {
                return (BasePage)ScenarioContext.Current["new"];
            }
            set
            {
                ScenarioContext.Current["new"] = value;
            }
        }

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

        //private IWebDriver _driver { get; set; }

        //public T GetInstance<T>() where new()
        //{
        //    T pageInstance = new T()
        //    {
        //        _driver = DriverContext.Driver
        //    };



        //    return pageInstance;
        //}

        //public TPage As<TPage>() where TPage : BasePage
        //{
        //    return (TPage)new BasePage();
        //}


        public static LoginDTO dtLogin { get; set; }


        #endregion Properties


    }
}
