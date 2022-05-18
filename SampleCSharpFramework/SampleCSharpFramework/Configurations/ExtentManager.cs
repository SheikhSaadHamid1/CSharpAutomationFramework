using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using SampleCSharpFramework.Utilities;

namespace SampleCSharpFramework.Configurations
{
    class ExtentManager : Base
    {
        #region Fields


        private static ExtentReports _extent;
        private static readonly object _lock = new();

        #endregion

        #region Properties
        public static ExtentReports GetInstance
        {

            get
            {
                lock (_lock)
                {
                    if (_extent == null)
                    {
                        _extent = CreateInstance();

                    }

                }

                return _extent;
            }



        }

        #endregion

        #region Constructor
        private ExtentManager()
        {
            //Private constructor to restrict Instance creation of Extent Manager class using new keyword
            Debug.WriteLine("Extent Manager object created");
        }
        #endregion

        #region Methods
        private static ExtentReports CreateInstance()
        {

            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(ConfigurationConstants.ExtentReportPath);
            //Adding configurations for Extent Report
            htmlReporter.Config.Encoding = ConfigurationConstants.ReportEncoding;
            htmlReporter.Config.DocumentTitle = ConfigurationConstants.ExtentReportTitle;
            htmlReporter.Config.ReportName = ConfigurationConstants.ExtentReportName;
            htmlReporter.Config.Theme = ConfigurationConstants.ReportTheme;
            _extent = new ExtentReports();

            //Adding System information for Extent Report
            _extent.AttachReporter(htmlReporter);
            _extent.AddSystemInfo("Browser", (ConfigurationConstants.Browser).ToUpper());
            _extent.AddSystemInfo("Environment", Environment.OSVersion.ToString());

            return _extent;


        }

        public static ExtentTest CreateParentTest(string className, string description = null)
        {

            ParentTest = (!string.IsNullOrEmpty(className))

                ? GetInstance.CreateTest(className, description)
                : GetInstance.CreateTest("Unkown Test", "Test class name is missing");

            return ParentTest;
        }

        public static ExtentTest CreateTest(string testName, string description = null)
        {
            ExtentTest = (!string.IsNullOrEmpty(testName))

                ? ParentTest.CreateNode(testName, description)
                : ParentTest.CreateNode("Unkown Test", "Test name is missing");

            return ExtentTest;
        }


        public static void LogTestStatus(TestStatus status, string stacktrace)
        {
            //Creating Test logs with  markups in Test Report w.r.t Test status


            IMarkup markup;
            Status logStatus;
            try
            {
                switch (status)
                {
                    case TestStatus.Failed:

                        logStatus = Status.Fail;
                        string screenshotPath = CommonUtilities.TakeScreenShot();
                        ExtentTest.AddScreenCaptureFromPath(screenshotPath, status.ToString());
                        ExtentTest.Log(logStatus, $"Test ended with  {logStatus} status due to :   {stacktrace}");
                        markup = MarkupHelper.CreateLabel($"Test {TestContext.CurrentContext.Test.Name} is {logStatus}", ExtentColor.Red);
                        ExtentTest.Log(logStatus, markup);
                        break;


                    case TestStatus.Skipped:

                        logStatus = Status.Skip;
                        ExtentTest.Log(logStatus, $"Test ended with  {logStatus} status due to :   {stacktrace}");
                        markup = MarkupHelper.CreateLabel($"Test {TestContext.CurrentContext.Test.Name} is {logStatus}", ExtentColor.Yellow);
                        ExtentTest.Log(logStatus, markup);
                        break;

                    case TestStatus.Passed:

                        logStatus = Status.Pass;
                        markup = MarkupHelper.CreateLabel($"Test {TestContext.CurrentContext.Test.Name} is {logStatus}", ExtentColor.Green);
                        ExtentTest.Log(logStatus, markup);
                        break;


                    default:

                        logStatus = Status.Warning;
                        markup = MarkupHelper.CreateLabel($"Test {TestContext.CurrentContext.Test.Name} is {logStatus}", ExtentColor.Grey);
                        ExtentTest.Log(logStatus, markup);
                        break;

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Test status not logged due to  {e.Message}");
            }

        }

        public static void CloseExtent()
        {
            //Writes or updates the test information(logs) to the destination report.
            _extent.Flush();

        }

        #endregion
    }
}
