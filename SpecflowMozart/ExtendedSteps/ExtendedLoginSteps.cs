﻿using SpecflowMozart.Base;
using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using SpecflowMozart.Pages;

namespace SpecflowMozart.ExtendedStep
{
    [Binding]
    public class ExtendedLoginStep : BaseStep
    {
        /// <summary>
        /// Login to the given application
        /// </summary>
        [Given(@"I login to (.*)")]
        public void GivenILoginToLeads(string product)
        {
            CurrentPage.GetInstance<LoginPage>();

            CurrentPage = CurrentPage.As<LoginPage>().Login(login.userName, login.password);

            CurrentPage = CurrentPage.As<HomePage>().ClickLeadsButton();
        }

       


    }



}
