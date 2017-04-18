using SpecflowMozart.Base;
using SpecflowMozart.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecflowMozart.Steps
{
    [Binding]
    public class LoginSteps : BaseStep
    {
        [Given(@"I launch the browser")]
        public void GivenILaunchTheBrowser()
        {
            Console.WriteLine("Browser is launched");
            CurrentPage = GetInstance<LoginPage>();
        }
        
        [When(@"I enter username")]
        public void WhenIEnterUsername(string userName)
        {

            CurrentPage.As<LoginPage>().EnterUserName(userName);

        }

        [When(@"I enter password")]
        public void WhenIEnterPassword(string password)
        {
            CurrentPage.As<LoginPage>().EnterPassword(password);
        }

        [When(@"I click (.*) button")]
        public void WhenIClickLoginButton()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I logged into application")]
        public void ThenILoggedIntoApplication()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
