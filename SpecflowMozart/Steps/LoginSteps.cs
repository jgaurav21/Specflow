using SpecflowMozart.Base;
using SpecflowMozart.Pages;
using System;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow;

namespace SpecflowMozart.Steps
{
    [Binding]
    public class LoginSteps : BaseStep
    {
        [Given(@"I launch the application")]
        public void GivenILaunchTheBrowser()
        {
            Console.WriteLine("Browser is launched");
            CurrentPage = GetInstance<LoginPage>();
        }
        
        [When(@"I enter (.*) into username")]
        public void WhenIEnterUsername(Table table)
        {
            dynamic user = table.CreateDynamicInstance();
            CurrentPage.As<LoginPage>().EnterUserName(user.username);

        }

        [When(@"I enter (.*) into password")]
        public void WhenIEnterPassword(Table table)
        {
            dynamic pass = table.CreateDynamicInstance();
            CurrentPage.As<LoginPage>().EnterPassword(pass.password);
        }

        [When(@"I click login button")]
        public void WhenIClickLoginButton()
        {
            CurrentPage.As<LoginPage>().ClickLoginButton();
        }

        [Then(@"I logged into application")]
        public void ThenILoggedIntoApplication()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
