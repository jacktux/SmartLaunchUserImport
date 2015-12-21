using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Smartlaunch.Api.Client.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLaunchUserImport.Helpers
{
    public class ExcelImporter
    {

        public List<ExtendedUser> ImportUsers()
        {
            List<ExtendedUser> users = new List<ExtendedUser>();
            using (FileStream file = new FileStream(@"C:\Users\admin2\Downloads\InternetCafe.xlsx", FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook wb = new XSSFWorkbook(file);
                var sheet = wb.GetSheetAt(0);
                for (int row = 0; row <= sheet.LastRowNum; row++)
                {
                    if (sheet.GetRow(row) != null && row > 0) //null is when the row only contains empty cells 
                    {
                        ExtendedUser extuser = new ExtendedUser();
                        User user = new User();
                        extuser.PersonalNumber = GetStringValue(sheet.GetRow(row).GetCell(0));
                        extuser.LastName = GetStringValue(sheet.GetRow(row).GetCell(1));
                        extuser.FirstName = GetStringValue(sheet.GetRow(row).GetCell(2));
                        extuser.SetBirthday(FromExcelSerialDate(Int32.Parse(GetStringValue(sheet.GetRow(row).GetCell(3)))));
                        extuser.State = GetStringValue(sheet.GetRow(row).GetCell(4));
                        extuser.Address = GetStringValue(sheet.GetRow(row).GetCell(5));
                        extuser.Birthday = extuser.GetBirthday();
                        user.UserName = extuser.PersonalNumber;
                        extuser.User = user;
                        users.Add(extuser);
                        Console.Out.WriteLine(string.Format("Row {0} = {1}", row, sheet.GetRow(row).GetCell(0).NumericCellValue));
                    }
                }
            }
            return users;
        }

        private static DateTime FromExcelSerialDate(int SerialDate)
        {
            if (SerialDate > 59) SerialDate -= 1; //Excel/Lotus 2/29/1900 bug   
            return new DateTime(1899, 12, 31).AddDays(SerialDate);
        }

        private string GetStringValue(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.Unknown:
                    break;
                case CellType.Numeric:
                    return cell.NumericCellValue.ToString();
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Formula:
                    break;
                case CellType.Blank:
                    break;
                case CellType.Boolean:
                    break;
                case CellType.Error:
                    break;
                default:
                    break;
            }
            return "";
        }
    }
}
