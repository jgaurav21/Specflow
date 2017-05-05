using OpenQA.Selenium;
using SpecflowMozart.DTO;
using SpecflowMozart.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using SpecflowMozart.Bases;

namespace SpecflowMozart.Bases
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
                return (BasePage)ScenarioContext.Current["currentPage"];
            }
            set
            {
                ScenarioContext.Current["currentPage"] = value;
            }
        }





        public static LoginDTO dtLogin { get; set; }


        #endregion Properties


    }
}
