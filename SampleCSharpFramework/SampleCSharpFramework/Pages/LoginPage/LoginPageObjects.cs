using SampleCSharpFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCSharpFramework.Pages.LoginPage
{
    class LoginPageObjects
    {
        #region Fields
        private LoginPageHelper login = new LoginPageHelper();
        private  ExcelUtility excelData ;
        #endregion

        public LoginPageObjects()
        {
            excelData = new ExcelUtility();
        }



        #region Methods

        public void PerformUserLogin()
        {
           string username =  "SampleUser"; //to_do: To be retreived from Excel File using Excel Utility
            string userPassword = "SamplePassword"; //to_do: To be retreived from Excel File using Excel Utility

            login.PerformUserLogin(username, userPassword);


        }


        #endregion





    }
}
