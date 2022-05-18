using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;


using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.Threading;
using SampleCSharpFramework.Utilities;

namespace SampleCSharpFramework.Configurations
{
    [SetUpFixture]
    public class Base
    {
          
      
      
            /*
             * Class is used to initialize the Test Execution environment and is responsible to 
             * 1: Initialize browser
             * 2:Create driver instance, which is to be used in several test methods and different utilities
             * 3: Initialize Extent Reports 
             * 4: Author Test status in Extent Report
             * 5: Close Browser Instances         * 
             */

            #region Fields
            private readonly static string _url = ConfigurationConstants.URL;
            
            #endregion

            #region Properties
            public static IWebDriver Driver { get; private set; }
            public static ExtentReports ExtentReports { get; set; }
            public static ExtentTest ExtentTest { get; set; }
            public static ExtentTest ParentTest { get; set; }
            public static WebDriverWait Wait { get; set; }
          

            #endregion

            #region Methods

            [OneTimeSetUp]
            protected void InitializeExtent()
            {
                //Intializing Parent Test Node.
                ExtentManager.CreateParentTest(GetType().Name);
            }

            [SetUp]
            protected void InitializeTest()
            {
                try
                {
                    WebSetupUtility setup = WebSetupUtility.GetInstance;
                    //Opening Browser and instantiating Driver 
                    Driver = setup.InitializeBrowser();
                Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Double.Parse(ConfigurationConstants.ExplicitWait)));
                    if (Driver != null)
                    {
                        //Navigating to the url of Application under test.
                        if (!String.IsNullOrEmpty(_url))
                        {
                            Driver.Navigate().GoToUrl(_url);
                        }
                        else
                        {
                            Debug.WriteLine("No Url provided, Please provide valid URL for Application under Test in app.config file");
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Driver is not initialized due to Null value for Driver object.");
                    }
                }

                catch (Exception e)
                {
                    Debug.WriteLine($"Test Initialization failed due to {e.Message}");
                }

            }

            [TearDown]
            protected void AfterTest()
            {
               
                TestStatus status = TestContext.CurrentContext.Result.Outcome.Status;
                string stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : $"{TestContext.CurrentContext.Result.Message}";

                ExtentManager.LogTestStatus(status, stacktrace);

                ExtentManager.CloseExtent();
                if (Driver != null)
                {
                    Thread.Sleep(2000); //todo: to be removed after adding tests
                    Driver.Quit();

                }
            }
            #endregion
        }
    }
