using SpecflowMozart.Base;
using SpecflowMozart.Pages;
using System;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow;
using SpecflowMozart.Helper;

namespace SpecflowMozart.Step
{
    [Binding]
    public class LoginStep : BaseStep
    {
        LoginPage login = new LoginPage();

        [Given(@"I launch the application")]
        public void GivenILaunchTheBrowser()
        {
            Console.WriteLine("Browser is launched");
            //CurrentPage = GetInstance<LoginPage>();
            
            LogHelpers.Write("Browser Launched");
        }

        [When(@"I enter (.*) into username")]
        public void WhenIEnterIntoUsername(string p0, Table table)
        {
            dynamic user = table.CreateDynamicInstance();
            //CurrentPage.As<LoginPage>().EnterUserName(user.username);

            login.EnterUserName(user.username);
            LogHelpers.Write("Username is entered" + p0);
            
        }
        

        [When(@"I enter (.*) into password")]
        public void WhenIEnterIntoPassword(string p0, Table table)
        {
            dynamic pass = table.CreateDynamicInstance();
            //CurrentPage.As<LoginPage>().EnterPassword(pass.password);

            login.EnterPassword(pass.password);

            
            LogHelpers.Write("Password is entered " + p0);
        }


       

        [When(@"I click login button")]
        public void WhenIClickLoginButton()
        {
            //CurrentPage.As<LoginPage>().ClickLoginButton();

            GivenILaunchTheBrowser();
            login.ClickLoginButton();
            LogHelpers.Write("Login button is clicked");
        }

        [Then(@"I logged into application")]
        public void ThenILoggedIntoApplication()
        {
            Console.WriteLine("Success");
        }

    }
}
