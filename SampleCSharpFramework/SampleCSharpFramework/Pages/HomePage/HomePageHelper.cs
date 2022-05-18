using OpenQA.Selenium;
using SampleCSharpFramework.Configurations;
using SampleCSharpFramework.Pages.LoginPage;
using SampleCSharpFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCSharpFramework.Pages.HomePage
{
    class HomePageHelper : Base
    {
        #region Fields
        private IWebElement loginPage => Driver.FindElement(By.XPath(HomePageConstants.LoginPageLocator));
        #endregion


        #region Methods
        public LoginPageObjects NavigateToLoginPage()
        {
            CommonUtilities.ClickOnElement(loginPage);
            bool isHeadingDisplayed = Wait.Until(ele => Driver.FindElement(By.XPath(LoginPageConstants.LoginPageMainHeading)).Displayed);
            LoginPageObjects login = isHeadingDisplayed ? new LoginPageObjects() : null;
            return login;
        }

        #endregion

    }
}
