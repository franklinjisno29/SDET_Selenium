using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunnyCart.Utilities
{
    internal class ExcelUtils
    {
        public static List<SignUpData> ReadSignUpData(string excelFilePath, string sheetName)
        {
            List<SignUpData> signUpDataList = new List<SignUpData>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var stream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });

                    var dataTable = result.Tables[sheetName];

                    if (dataTable != null)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            SignUpData signUpData = new SignUpData
                            {
                                FirstName = GetValueOrDefault(row, "firstname"),
                                LastName = GetValueOrDefault(row, "lastname"),
                                Email = GetValueOrDefault(row, "email"),
                                Password = GetValueOrDefault(row, "pwd"),
                                ConfirmPassword = GetValueOrDefault(row, "conpwd"),
                                MobileNumber = GetValueOrDefault(row, "mbno")
                            };

                            signUpDataList.Add(signUpData);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Sheet '{sheetName}' not found in the Excel file.");
                    }
                }
            }

            return signUpDataList;
        }

        static string GetValueOrDefault(DataRow row, string columnName)
        {
            Console.WriteLine(row + "  " + columnName);
            return row.Table.Columns.Contains(columnName) ? row[columnName]?.ToString() : null;
        }
    }
}

