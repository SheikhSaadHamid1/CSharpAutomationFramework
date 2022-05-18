using AventStack.ExtentReports.Reporter.Configuration;
using System;
using System.Configuration;


namespace SampleCSharpFramework.Configurations
{
    static class ConfigurationConstants
    {
        /*
         * Configuration constant provides all the static fields which holds values of respective Keys, speicified  in 'app.config' file
         * For instance, URL holds the address of the application under test, which was configured in 'app.config' file.
         */


        #region Properties
        public static string URL
        {
            get
            {
                return !String.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("Url"))
                      ? ConfigurationManager.AppSettings.Get("Url")
                      : null;
            }
        }

        public static string Browser
        {
            get
            {
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("Browser")))
                {
                    int browserValue = 0;
                    bool isConverted = int.TryParse(ConfigurationManager.AppSettings.Get("Browser"), out browserValue);
                    if (isConverted)
                    {

                        return Enum.GetName(typeof(BrowserEnum), browserValue);
                    }

                }


                return "Chrome";

            }
        }

        public static string ImplicitWait
        {
            get
            {
                return !String.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("Implicit_Wait"))
                    ? ConfigurationManager.AppSettings.Get("Implicit_Wait")
                    : "10";
            }
        }

        public static string ExplicitWait
        {
            get
            {
                return !String.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("Explicit_Wait"))
                    ? ConfigurationManager.AppSettings.Get("Explicit_Wait")
                    : "10";
            }
        }

        public static string PageLoadTimeOut
        {
            get
            {
                return !String.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("PageLoad_Timeout"))
                    ? ConfigurationManager.AppSettings.Get("PageLoad_Timeout")
                    : "15";
            }
        }


        public static string ExtentReportPath
        {
            get
            {
                return !String.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("Report_Path"))
                    ? AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net48", "") + ConfigurationManager.AppSettings.Get("Report_Path")
                    : AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net48", "") + "\\Reports\\Report.html";
            }
        }


        public static string ScreenshotRootPath
        {
            get
            {
                return !String.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("Report_Path"))
                    ? AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net48", ConfigurationManager.AppSettings.Get("Screen_Shot_Path"))
                    : AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net48", "screenshots");
            }
        }


        public static string ExtentReportName
        {
            get
            {
                return !String.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("Report_Name"))
                    ? ConfigurationManager.AppSettings.Get("Report_Name")
                    : "Automation Test Results";
            }
        }


        public static string ExtentReportTitle
        {
            get
            {
                return !String.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("Report_Title"))
                    ? ConfigurationManager.AppSettings.Get("Report_Title")
                    : "Automation Report";
            }
        }

        public static string ReportEncoding
        {
            get
            {
                return !String.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("Report_Encoding"))
                    ? ConfigurationManager.AppSettings.Get("Report_Encoding")
                    : "utf-8";
            }
        }


        public static Theme ReportTheme
        {
            get
            {

                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("Extent_Theme")))
                {
                    if (ConfigurationManager.AppSettings.Get("Extent_Theme") == "1")
                    {
                        return Theme.Dark;
                    }
                    else
                    {
                        return Theme.Standard;
                    }

                }
                else
                {
                    return Theme.Standard;
                }
            }
        }

        public static string ExcelFilePath
        {
            get
            {
                return !String.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("Excel_File_Path"))
                    ? AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net48", "") + ConfigurationManager.AppSettings.Get("Excel_File_Path")
                    : AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net48", "") + ConfigurationManager.AppSettings.Get("Excel_File_Path");
            }
        }




        #endregion

    }
}
