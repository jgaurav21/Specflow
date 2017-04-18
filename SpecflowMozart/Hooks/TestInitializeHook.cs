using System;
using OpenQA.Selenium;
using SpecflowMozart.Base;
using TechTalk.SpecFlow;
using SpecflowMozart.Config;

namespace SpecflowMozart.Hooks
{
    [Binding]
    public class TestInitializeHook : HookMethods
    {
        [BeforeFeature]
        public static void BeforeFeature()
        {

        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            StartDriver(Settings.BrowserType);
        }


        [AfterScenario]
        public void AfterScenario()
        {

        }


        [AfterFeature]
        public void AfterFeature()
        {

        }

    }
}
