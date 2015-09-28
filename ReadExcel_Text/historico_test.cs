using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPOI.SS.UserModel;
using ReadXLSWriteCSV;
using ReadXLSWriteCSV.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadExcel_Text
{
    [TestClass]
    public class historico_test
    {

        [TestMethod]
        [DeploymentItem(@"../../TestData/Histórico UF.xlsx", "TestData")]
        public void ReadExcel()
        {

            string filePath = @"TestData\Histórico UF.xlsx";

            IList<Measure> result = new List<Measure>();

            StringBuilder sb = new StringBuilder();
            int counter = 0;

            FileStream _fileStream = new FileStream(filePath, FileMode.Open,
                            FileAccess.Read);

            IWorkbook _workbook = WorkbookFactory.Create(_fileStream);
            _fileStream.Close();


            int sheets = _workbook.NumberOfSheets;

            for (int i = 0; i < sheets; i++)
            {

                ISheet _sheet = _workbook.GetSheetAt(i);

                string idPlant = _sheet.SheetName;
                int lastRow = _sheet.LastRowNum;

                for (int row = 1; row <= lastRow; row++)
                {

                    DateTime datatime = _sheet.GetRow(row).GetCell(0).DateCellValue;
                    //datatime.A = _sheet.GetRow(row).GetCell(1).NumericCellValue;
                    int hour = Convert.ToInt32(_sheet.GetRow(row).GetCell(1).NumericCellValue -1);
                    
                    DateTime finalhour = new DateTime(datatime.Year, datatime.Month, datatime.Day, hour, 0,0);
                    double value = _sheet.GetRow(row).GetCell(2).NumericCellValue;


                }
            }
        }
    }
}
