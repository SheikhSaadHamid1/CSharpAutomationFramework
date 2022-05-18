using SampleCSharpFramework.Configurations;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCSharpFramework.Utilities
{
    class CommonUtilities : Base
    {
        /*
    * Commmon utilities provides basic reuseable methods which may be used to perform common operations to execute a test
    * Instance of Common utlities should be created with IWebDriver object provided as a parameter
    */

        #region Methods
        public static void ClickOnElement(IWebElement ele)
        {
            try
            {
                if (ele != null)
                {
                    ele.Click();
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine($"Error occured due to: {e.Message}");
            }

        }

        public static void SendKeys(IWebElement ele, string text)
        {
            try
            {
                if (ele != null && !string.IsNullOrEmpty(text))
                {
                    ele.Click();
                    ele.Clear();
                    ele.SendKeys(text);
                }
                else
                {
                    Debug.WriteLine("WebElement or Text are either null or empty");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine($"Error occured due to: {e.Message}");
            }

        }

        public static string GetTextFromElement(IWebElement ele)
        {
            string text = string.Empty;
            try
            {
                if (ele != null && ele.Displayed)
                {
                    text = ele.Text;
                }
                else
                {


                    Debug.WriteLine("WebElement is either null or empty");

                }
            }
            catch (Exception e)
            {

                Debug.WriteLine($"Error occured due to: {e.Message}");
            }
            return text;
        }





        public static string TakeScreenShot()
        {


            string rootPath = ConfigurationConstants.ScreenshotRootPath;


            string fullPath = string.Empty;

            try
            {
                fullPath = rootPath + GenerateFileName();
                ITakesScreenshot screen = Driver as ITakesScreenshot;
                Screenshot screenshot = screen.GetScreenshot();
                screenshot.SaveAsFile(fullPath, format: ScreenshotImageFormat.Png);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Cannot take screenshot due to : {e.Message}");
            }

            return fullPath;
        }

        private static string GenerateFileName()
        {
            string dateString = DateTime.Now.ToString("yyyy-mm-dd hh-mm-ss").Replace("-", "_");
            string fileName = dateString + ".png";
            return fileName;

        }

        #endregion

    }
}
