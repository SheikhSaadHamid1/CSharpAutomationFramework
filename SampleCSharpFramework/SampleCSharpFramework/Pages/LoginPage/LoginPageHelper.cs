using OpenQA.Selenium;
using SampleCSharpFramework.Configurations;
using SampleCSharpFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCSharpFramework.Pages.LoginPage
{
    class LoginPageHelper : Base
    {
        #region Fields
        private IWebElement loginPageMainHeading => Driver.FindElement(By.XPath(LoginPageConstants.LoginPageMainHeading));
        private IWebElement loginField => Driver.FindElement(By.XPath(LoginPageConstants.loginNameField));
        private IWebElement passwordField => Driver.FindElement(By.XPath(LoginPageConstants.passwordField));
        private IWebElement loginButton => Driver.FindElement(By.XPath(LoginPageConstants.loginButton));

        #endregion


        #region Methods

        public void PerformUserLogin(string userName, string password)
        {
            EnterLoginId(userName);
            EnterPassword(password);
            clickLoginButton();
        }

        private void EnterLoginId(string loginId)
        {
            CommonUtilities.SendKeys(loginField, loginId);
        }

        private void EnterPassword(string password)
        {
            CommonUtilities.SendKeys(passwordField, password);
        }

        private void clickLoginButton()
        {
            CommonUtilities.ClickOnElement(loginButton);
        }

        


        #endregion







    }
}
