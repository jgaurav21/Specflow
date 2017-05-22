using System;
using OpenQA.Selenium;
using SpecflowMozart.Bases;
using TechTalk.SpecFlow;
using SpecflowMozart.Config;
using SpecflowMozart.DTO;
using SpecflowMozart.Helper;
using SpecflowMozart.Pages;

namespace SpecflowMozart.Hooks
{
    [Binding]
    public class TestInitializeHook : HookMethods
    {
        [BeforeFeature]
        public static void BeforeFeature()
        {
           
            ConfigReader.SetFrameworkSettings();
            dtLogin = GetTestData();
            
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            login = new LoginPage();
            LogHelpers.Write($"Start Scenario : {ScenarioContext.Current.ScenarioInfo.Title} ------------");
            StartDriver(Settings.BrowserType);
            DriverContext.Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
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
            //DriverContext.Driver.Quit();
        }

        [AfterTestRun]
        public static void AfterRun()
        {
            CloseDriver();
        }

        [StepArgumentTransformation(@"I login to (.*)")]
        public Product ProductTypeTransformer(string product)
        {
            Product loginProduct = Product.Leads;
            switch(product.ToLower())
            {
                case "analyze":
                    loginProduct = Product.Analyze;
                    break;

                case "forecast":
                    loginProduct = Product.Forecast;
                    break;

                case "pulse":
                    loginProduct = Product.Pulse;
                    break;
                    
            }

            return loginProduct;
        }

    }
}
