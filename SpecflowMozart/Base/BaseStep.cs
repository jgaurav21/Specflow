using SpecflowMozart.DTO;
using SpecflowMozart.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecflowMozart.Base
{
    public class BaseStep : Steps
    {

        #region Properties
        private LoginPage _login;
        public LoginPage login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
            }

        }
        public static LoginDTO dtLogin { get; set; }


        #endregion Properties


    }
}
