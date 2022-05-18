using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCSharpFramework.Pages.LoginPage
{
    class LoginPageConstants
    {
        public static string LoginPageMainHeading = ".//h1[@class='heading1']";
        public static string loginNameField = ".//input[@id='loginFrm_loginname']";
        public static string passwordField = ".//input[@id='loginFrm_password']";
        public static string loginButton = ".//button[@title='Login']";
    }
}
