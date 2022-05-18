using System;
using System.Collections.Generic;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Diagnostics;
using System.IO;
using SampleCSharpFramework.Configurations;

namespace SampleCSharpFramework.Utilities
{
    public class ExcelUtility
    {
        #region Fields
        private static IWorkbook _workbook = null;
        private static Dictionary<string, string> excelData = new Dictionary<string, string>();
        private static string filePath = string.Empty;
        private static FileStream fileStream;



        #endregion

        #region Constructor
        static ExcelUtility()
        {
            try
            {
                _workbook = SetupExcelConfig();
                ReadDataFromExcel();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Cannot Initialze Workbook due to {e.Message} ");
            }
        }

        #endregion



        #region Methods

        /*
         * Method setups and returns a workbook
         * Workbook can have either of extension .xlsx or .xls
         */
        private static IWorkbook SetupExcelConfig()
        {

            filePath = ConfigurationConstants.ExcelFilePath;
            if (!String.IsNullOrWhiteSpace(filePath))
            {
                //Setting up file path of Excel file

                try
                {
                    ValidateFile(filePath); // to-do: to be handled with boolean

                    fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);




                    if (filePath.EndsWith(".xlsx"))
                    {
                        _workbook = new XSSFWorkbook(fileStream);
                    }
                    else if (filePath.EndsWith(".xls"))
                    {
                        _workbook = new HSSFWorkbook(fileStream);
                    }


                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

            }
            return _workbook;

        }

        private static void ValidateFile(string filePath)
        {
            if (!(filePath.EndsWith(".xlsx") || (filePath.EndsWith(".xls"))))
            {

                throw new Exception("Incorrect File Extension specified for Excel File ");
            }
        }
        /*
         * This method returns a String cell value of a provided field name
         * 
         * */
        private static Dictionary<string, string> ReadDataFromExcel()
        {
            string cellKey = string.Empty;
            string cellValue = string.Empty;
            try
            {

                if (_workbook != null)
                {
                    ISheet sheet = _workbook.GetSheetAt(0);

                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        //Key value 0 represents Key for which value is to be retrieved
                        cellKey = row.GetCell(0).StringCellValue.Trim();
                        cellValue = row.GetCell(1).ToString();
                        excelData.Add(cellKey, cellValue);

                    }
                }

            }

            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }


            return excelData;
        }

        public string GetExcelDataValue(string key)
        {
            string cellValue = string.Empty;
            if (!String.IsNullOrEmpty(key))
            {
                foreach (var item in excelData.Keys)
                {
                    if (item.ToLower().Equals(key.ToLower()))
                    {
                        excelData.TryGetValue(item, out cellValue);
                        break;
                    }
                    else
                    {
                        Debug.WriteLine("No value available for provided key");
                    }
                }

            }
            else
            {
                Debug.WriteLine("Null or Empty values provided for Keys");
            }

            return cellValue;
        }

        #endregion
    }
}
