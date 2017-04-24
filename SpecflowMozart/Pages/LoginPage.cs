using OpenQA.Selenium;
using SpecflowMozart.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecflowMozart.Pages
{
    public class LoginPage
    {
        #region Elements

        // Username Textbox
        private IWebElement txtUserName => DriverContext.Driver.FindElement(By.Id("txtUserName"));

        // Password Textbox
        private IWebElement txtPassword => DriverContext.Driver.FindElement(By.Id("txtPassword"));

        // Login button
        private IWebElement btnLogin => DriverContext.Driver.FindElement(By.Id("btnLogon"));

        #endregion

        #region Actions

        /// <summary>
        /// To enter user name in the textbox
        /// </summary>
        /// <param name="userName">user name</param>
        public void EnterUserName(string userName)
        {
            txtUserName.SendKeys(userName);
        }

        /// <summary>
        /// To enter password in the textbox
        /// </summary>
        /// <param name="password">password</param>
        public void EnterPassword(string password)
        {
            txtPassword.SendKeys(password);
        }

        /// <summary>
        /// To click login button
        /// </summary>
        public void ClickLoginButton()
        {
            btnLogin.Click();
        }
        #endregion


        #region Method

        /// <summary>
        /// Click on Login button
        /// </summary>
        /// <param name="userName">username</param>
        /// <param name="password">password</param>
        public T Login<T>(string userName, string password) where T : new()
        {
            EnterUserName(userName);
            EnterPassword(password);
            ClickLoginButton();

            return new T();

            //return GetInstance<HomePage>();
        }



        #endregion
    }
}
