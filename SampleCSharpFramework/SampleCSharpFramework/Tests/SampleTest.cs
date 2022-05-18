using AventStack.ExtentReports;
using NUnit.Framework;
using SampleCSharpFramework.Configurations;
using SampleCSharpFramework.Pages.HomePage;
using SampleCSharpFramework.Pages.LoginPage;

namespace SampleCSharpFramework
{
    [TestFixture]
    public class SampleTest : Base
    {
        HomePageHelper homePage ;
        LoginPageObjects login;
        

        [Test, Order(1)]
        public void Test1()
        {
            ExtentTest = ExtentManager.CreateTest("Sample  Test", "Sample Test to demonstrate creation of Extent Report with Screenshot of Failed test");
            ExtentTest.Log(Status.Info, "Sample Test Step to Log");
           
            homePage = new HomePageHelper();
            ExtentTest.Log(Status.Debug, "About to Navigating to Login Page");

            login = homePage.NavigateToLoginPage();
            ExtentTest.Log(Status.Info, "Navigated to Login Page");

            ExtentTest.Log(Status.Debug, "About to perform user login");
            login.PerformUserLogin();
            ExtentTest.Log(Status.Info, "User Login Compeleted with invlaid login message");
            ExtentTest.Log(Status.Info, "Failing Test");
            Assert.Fail("Failing Test Case");






        }
    }
}