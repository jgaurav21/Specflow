using System;
using OpenQA.Selenium;
using SpecflowMozart.Base;
using TechTalk.SpecFlow;
using SpecflowMozart.Config;
using SpecflowMozart.DTO;

namespace SpecflowMozart.Hooks
{
    [Binding]
    public class TestInitializeHook : HookMethods
    {
        [BeforeFeature]
        public static void BeforeFeature()
        {
            SetFrameworkSettings();
            login = GetTestData();
            
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            
            StartDriver(Settings.BrowserType);
            NavigateToURL(Settings.AUT);
        }


        [AfterScenario]
        public void AfterScenario()
        {

            CloseDriver();
        }


        [AfterFeature]
        public static void AfterFeature()
        {
            DriverContext.Driver.Quit();
        }

    }
}
