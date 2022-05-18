using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SampleCSharpFramework.Configurations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SampleCSharpFramework.Utilities
{
    sealed class WebSetupUtility
    {

        #region Fields
        private static WebSetupUtility _setup = null;
        private static IWebDriver _driver;
        private static long _implicitWait = 0;
        private static long _pageloadTimeout = 0;
        private static readonly object _lock = new object();

        #endregion

        #region Constructor
        private WebSetupUtility()
        {
            //Private constructor to restrict Instance creation of SetupUtility class using new keyword
            Debug.WriteLine("SetupUtility object created");
        }
        #endregion


        #region Properties
        public static WebSetupUtility GetInstance
        {

            get
            {
                //Implimenting Thread safety to synchronize object creation of Setup Utility class
                lock (_lock)
                {
                    if (_setup == null)
                    {
                        _setup = new WebSetupUtility();

                    }
                }

                return _setup;
            }
        }
        #endregion

        #region Methods
        public IWebDriver InitializeBrowser()
        {
            //Method to Initialize IWebDriver driver object with browser configured in app.config. 
            //Chrome browser is the default
            try
            {
                switch (ConfigurationConstants.Browser)
                {
                    case "Chrome":
                        {
                            _driver = InitializeChromeBrowser();
                            break;
                        }
                    default:
                        {
                            Debug.WriteLine("No Browser Configured, Launched Chrome as default");
                            break;
                        }

                }


                //Configuring Implicit wait time
                bool isConverted = false;
                isConverted = long.TryParse(ConfigurationConstants.ImplicitWait, out _implicitWait);
                if (isConverted)
                {
                    _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_implicitWait);
                    isConverted = false;
                }
                else
                {
                    Debug.WriteLine("Incorrect Time provided for Implicit Wait");
                }

                //Configuring PageLoad Timeout time
                isConverted = long.TryParse(ConfigurationConstants.PageLoadTimeOut, out _pageloadTimeout);
                if (isConverted)
                {
                    _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(_pageloadTimeout);
                    isConverted = false;
                }
                else
                {
                    Debug.WriteLine("Incorrect Time provided for PageLoadTime out");
                }

                //Setting browser Properties on opening a new browser window.

                _driver.Manage().Cookies.DeleteAllCookies();
                _driver.Manage().Window.Maximize();


            }
            catch (Exception e)
            {
                Debug.WriteLine($"Browser cannot be Started due to: {e.Message}");
            }

            return _driver;
        }


        private IWebDriver InitializeChromeBrowser()
        {
            //Method to open Chrome Browser
            /*var browserPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _driver = new ChromeDriver(browserPath);
            */
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();
            return _driver;
        }

        #endregion
    }
}
