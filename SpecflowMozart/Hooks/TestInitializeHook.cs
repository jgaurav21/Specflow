using System;
using OpenQA.Selenium;
using SpecflowMozart.Bases;
using TechTalk.SpecFlow;
using SpecflowMozart.Config;
using SpecflowMozart.DTO;
using SpecflowMozart.Helper;
using SpecflowMozart.Pages;
using NUnit.Framework;

namespace SpecflowMozart.Hooks
{
    [Binding]
    public class TestInitializeHook : HookMethods
    {
        [BeforeFeature]
        public static void BeforeFeature()
        {
            string featureName = FeatureContext.Current.FeatureInfo.Title;
            ConfigReader.SetFrameworkSettings();
            string path = $"{Settings.ReportPath}\\Test_{featureName}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.html";
            Report.ReportInitialize(path);
            LogHelpers.CreateLogFile(featureName);
            Report.AddFeature(featureName);
            dtLogin = GetTestData();
            
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            login = new LoginPage();
            Report.TestReportInitialize(ScenarioContext.Current.ScenarioInfo.Title);
            LogHelpers.Write($"Start Scenario : {ScenarioContext.Current.ScenarioInfo.Title} ------------");
            StartDriver(Settings.BrowserType);
            DriverContext.Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            DriverContext.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
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
            Report.GenerateReport();
            //DriverContext.Driver.Quit();
        }

        [AfterTestRun]
        public static void AfterRun()
        {
            
            CloseDriver();
        }

        [OneTimeTearDown]
        public void AssemblyCleanUp()
        {
            //Report.GenerateReport();
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
