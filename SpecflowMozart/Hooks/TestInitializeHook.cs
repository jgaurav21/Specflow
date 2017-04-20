using System;
using OpenQA.Selenium;
using SpecflowMozart.Base;
using TechTalk.SpecFlow;
using SpecflowMozart.Config;
using SpecflowMozart.DTO;
using SpecflowMozart.Helper;

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
            LogHelpers.Write($"Start Scenario : {ScenarioContext.Current.ScenarioInfo.Title} ------------");
            StartDriver(Settings.BrowserType);
            NavigateToURL(Settings.AUT);
        }


        [AfterScenario]
        public void AfterScenario()
        {
            LogHelpers.Write($"End Scenarion : {ScenarioContext.Current.ScenarioInfo.Title} ------------");
            CloseDriver();
        }


        [AfterFeature]
        public static void AfterFeature()
        {
            DriverContext.Driver.Quit();
        }

    }
}
