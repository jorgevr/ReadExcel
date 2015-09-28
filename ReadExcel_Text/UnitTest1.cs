using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using NPOI.SS.UserModel;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using ReadXLSWriteCSV;
using System.Text;

namespace ReadExcel_Text
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [DeploymentItem(@"../../TestData/Baraganul 3 _Centralizator IBD_2013.xlsx", "TestData")]
        [DeploymentItem(@"../../TestData/Baraganul 3_centralizator IBD_2014.xlsx", "TestData")]
        [DeploymentItem(@"../../TestData/Baraganul 3_centralizator IBD_2015.xlsx", "TestData")]
        public void ReadExcel()
        {

            string[] filePaths = new string[] { @"TestData\Baraganul 3 _Centralizator IBD_2013.xlsx",
                @"TestData\Baraganul 3_centralizator IBD_2014.xlsx",@"TestData\Baraganul 3_centralizator IBD_2015.xlsx"  };

            IList<Measure> result = new List<Measure>();
            
            StringBuilder sb = new StringBuilder();
            int counter = 0;
            foreach (string filenamePath in filePaths)
            {
                counter++;

                FileStream _fileStream = new FileStream(filenamePath, FileMode.Open,
                                          FileAccess.Read);

                IWorkbook _workbook = WorkbookFactory.Create(_fileStream);
                _fileStream.Close();
                string getMonth = "";
                string getYear = "";
                string parsedDay = "";
                string parsedHour = "";

                IList<string> lines = new List<string>();
                foreach (ISheet sheet in _workbook)
                {
                    int sheetlength = sheet.SheetName.Length;
                    getYear = sheet.SheetName.Substring(sheetlength - 4, 4);
                    if (sheetlength == 7)
                    {
                        getMonth = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(sheet.SheetName.Substring(0,2));                       
                    } else
                    {
                        getMonth = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(sheet.SheetName.Substring(3,2));
                    }
                    //getMonth = ;

                    //start iteration per day
                    

                    for (int day = 1; day <= 31; day++)
                    {
                        if (sheet.GetRow(0).GetCell(day) != null)
                        {
                            for (int hour = 1; hour <= 24; hour++)
                            {
                                if (hour.ToString().Length == 1) parsedHour = "0" + hour;
                                else parsedHour = hour.ToString();

                                if (day.ToString().Length == 1) parsedDay = "0" + day;
                                else parsedDay = day.ToString();

                                string DateItem = getYear + getMonth + parsedDay + parsedHour + "0000";
                                double value = sheet.GetRow(hour).GetCell(day).NumericCellValue;
                                // Measure dailyValue = Measure.Create("ESPE01", "0", "d", "yyyyMMddhhmmss", value, 1, 1, "?");

                                var newLine = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};", "ESPE01", "0", "d", DateItem, value, 1, 1, "?");
                                lines.Add(newLine);

                                using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\Users\Jorge\Documents\TestFolder\WriteLines" + counter + ".csv"))
                                {
                                    foreach (string line in lines)
                                    {
                                        // If the line doesn't contain the word 'Second', write the line to the file. 
                                        if (!line.Contains("Second"))
                                        {
                                            file.WriteLine(line);
                                        }
                                    }
                                }

                                //System.IO.File.WriteAllLines(@"C:\Users\Public\TestFolder\WriteLines.txt", lines);
                                /*using (System.IO.StreamWriter file =
                                new System.IO.StreamWriter(@"C:\Users\Jorge\Documents\TestFolder\WriteLines.csv"))
                                {

                                    file.WriteLine(newLine);
                                    file.Flush();
                                }
                                 */ 


                       
                            }
                        }
                    }
                }


              //  System.IO.File.WriteAllLines(@"C:\Users\Public\TestFolder\WriteLines.csv", lines);
            }
            /*
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\Users\Jorge\Documents\TestFolder\WriteLines2.csv"))
            {

                file.WriteLine(sb.ToString());
            }
             */ 
        }
    }
}
