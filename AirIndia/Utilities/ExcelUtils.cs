using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirIndia.Utilities
{
    internal class ExcelUtils
    {
        public static List<SearchFlightData> ReadSearchFlightData(string excelFilePath, string sheetName)
        {
            List<SearchFlightData> searchFlightDataList = new List<SearchFlightData>();
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
                            SearchFlightData searchFlightData = new SearchFlightData
                            {
                                From = GetValueOrDefault(row, "From"),
                                To = GetValueOrDefault(row, "To"),
                                DaySelect = GetValueOrDefault(row, "DaySelect"),
                                MonthSelect = GetValueOrDefault(row, "MonthSelect"),
                                YearSelect = GetValueOrDefault(row, "YearSelect"),
                                Passengers = GetValueOrDefault(row, "Passengers"),
                                ClassSelect = GetValueOrDefault(row, "ClassSelect"),
                                ConcessionType = GetValueOrDefault(row, "ConcessionType"),
                                PId = GetValueOrDefault(row,"Pid"),
                                FirstName = GetValueOrDefault(row,"FirstName"),
                                LastName = GetValueOrDefault(row, "LastName"),
                                Email = GetValueOrDefault(row, "Email"),
                                ConfirmEmail = GetValueOrDefault(row, "ConfirmEmail"),
                                CountryCode = GetValueOrDefault(row, "CountryCode"),
                                MobileNo = GetValueOrDefault(row, "MobileNo"),
                                DOBday = GetValueOrDefault(row, "DOBd"),
                                DOBmonth = GetValueOrDefault(row, "DOBm"),
                                DOByear = GetValueOrDefault(row, "DOBy")
                            };

                            searchFlightDataList.Add(searchFlightData);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Sheet '{sheetName}' not found in the Excel file.");
                    }
                }
            }

            return searchFlightDataList;
        }

        static string GetValueOrDefault(DataRow row, string columnName)
        {
            Console.WriteLine(row + "  " + columnName);
            return row.Table.Columns.Contains(columnName) ? row[columnName]?.ToString() : null;
        }
    }
}

