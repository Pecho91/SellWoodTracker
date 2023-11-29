using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using SellWoodTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SellWoodTracker.DataAccess
{
    public class ExcelConnector : IDataConnection
    {
        private readonly string _excelFilePath;
        
        public ExcelConnector(string excelFilePath)
        {
            _excelFilePath = excelFilePath;
        }
        public void CreatePerson(PersonModel person)
        {
             SavePersonToExcel(person, "RequestedPeople");                 
        }

        public List<PersonModel> GetRequestedPeople_All()
        {
            var requestedPeople = GetPeopleFromExcel("RequestedPeople");

            if (requestedPeople.Count == 0)
            {
                Console.WriteLine("No requested people data found.");
            }

            return requestedPeople;
        }

        public List<PersonModel> GetCompletedPeople_All()
        {
            var completedPeople = GetPeopleFromExcel("CompletedPeople");

            if (completedPeople.Count == 0)
            {
                Console.WriteLine("No completed people data found.");
            }

            return completedPeople;
        }

        public void MoveRequestedPersonToCompleted(int personId)
        {
            var requestedPeople = GetRequestedPeople_All();
            var personToMove = requestedPeople.FirstOrDefault(p => p.Id == personId);

            if (personToMove != null)
            {
                CreatePersonInCompleted(personToMove);
                DeletePersonFromExcel("RequestedPeople", personId);
            }
        }

        private void SavePersonToExcel(PersonModel person, string sheetName)
        {
            try
            {
                using (var workbook = GetOrCreateWorkbook())
                {
                    var worksheet = workbook.Worksheet(sheetName);

                    if (worksheet != null)
                    {
                        int lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 0;

                        if (lastRow == 0)
                        {
                            // Header row
                            worksheet.Cell(1, 1).Value = "Id";
                            worksheet.Cell(1, 2).Value = "First Name";
                            worksheet.Cell(1, 3).Value = "Last Name";
                            worksheet.Cell(1, 4).Value = "Email";
                            worksheet.Cell(1, 5).Value = "Cellphone";
                            worksheet.Cell(1, 6).Value = "Date";
                            worksheet.Cell(1, 7).Value = "Metric Amount";
                            worksheet.Cell(1, 8).Value = "Metric Price";

                            var range = worksheet.Range("A1:H1");
                            range.Style.Font.Bold = true;
                            range.Style.Fill.BackgroundColor = XLColor.LightGray;

                            lastRow = 1; // Reset lastRow to 1 after adding headers
                        }

                        int nextId = lastRow; // Using lastRow as nextId for simplicity, you can adjust this logic as needed

                        worksheet.Cell(lastRow + 1, 1).Value = nextId;
                        worksheet.Cell(lastRow + 1, 2).Value = person.FirstName;
                        worksheet.Cell(lastRow + 1, 3).Value = person.LastName;
                        worksheet.Cell(lastRow + 1, 4).Value = person.EmailAddress;
                        worksheet.Cell(lastRow + 1, 5).Value = person.CellphoneNumber;
                        worksheet.Cell(lastRow + 1, 6).Value = person.Date;
                        worksheet.Cell(lastRow + 1, 7).Value = person.MetricAmount;
                        worksheet.Cell(lastRow + 1, 8).Value = person.MetricPrice;

                        workbook.Save();
                    }
                    else
                    {
                        Debug.WriteLine($"Worksheet '{sheetName}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving person to Excel: {ex.Message}");
                throw; // Rethrow the exception for higher-level handling
            }
        }


        private void CreatePersonInCompleted(PersonModel person)
        {
            SavePersonToExcel(person, "CompletedPeople");
        }

        private List<PersonModel> GetPeopleFromExcel(string sheetName)
        {
            var people = new List<PersonModel>();

            try
            {
                XLWorkbook workbook = GetOrCreateWorkbook();
                var worksheet = workbook.Worksheet(sheetName);

                if (worksheet != null)
                {
                    var range = worksheet.RangeUsed();

                    if (range != null && range.RowCount() > 1) // Checking if there's data beyond the header row
                    {
                        var rows = range.RowsUsed().Skip(1); // Skipping the header row

                        foreach (var row in rows)
                        {
                            try
                            {
                                var person = new PersonModel()
                                {
                                    Id = row.Cell(1).GetValue<int>(),
                                    FirstName = row.Cell(2).GetValue<string>(),
                                    LastName = row.Cell(3).GetValue<string>(),
                                    CellphoneNumber = row.Cell(4).GetValue<string>(),
                                    EmailAddress = row.Cell(5).GetValue<string>(),
                                    Date = GetSafeDateValue(row.Cell(6)),
                                    MetricAmount = row.Cell(7).GetValue<decimal>(),
                                    MetricPrice = row.Cell(8).GetValue<decimal>()
                                };

                                people.Add(person);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"Error processing row {row.RowNumber()} in '{sheetName}': {ex.Message}");
                                // Add any specific details or logging you want here
                            }
                        }
                    }
                    else
                    {
                        Debug.WriteLine($"No data found in '{sheetName}' or the sheet is empty.");
                    }
                }
                else
                {
                    Debug.WriteLine($"Worksheet '{sheetName}' not found.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }

            return people;
        }
        private void DeletePersonFromExcel(string sheetName, int personId)
        {
            using (var workbook = new XLWorkbook(_excelFilePath))
            {
                var worksheet = workbook.Worksheet(sheetName);

                // Find the row based on the personId and handle conversion issues or empty cells
                var rowToDelete = worksheet.RowsUsed()
                    .FirstOrDefault(r =>
                    {
                        int id;
                        if (r.Cell(1).TryGetValue(out id))
                        {
                            return id == personId;
                        }
                        return false;
                    });

                if (rowToDelete != null)
                {
                    rowToDelete.Delete();
                    workbook.Save();
                }
            }
        }

        public void DeletePersonFromRequested(int personId)
        {
            DeletePersonFromExcel("RequestedPeople", personId);
        }

        public void DeletePersonFromCompleted(int personId)
        {
            DeletePersonFromExcel("CompletedPeople", personId);
        }


        private XLWorkbook GetOrCreateWorkbook()
        {
            XLWorkbook workbook;

            if (File.Exists(_excelFilePath))
            {
                workbook = new XLWorkbook(_excelFilePath);
            }
            else
            {
                workbook = new XLWorkbook();
                workbook.AddWorksheet("RequestedPeople"); // Change to your desired sheet name
                workbook.AddWorksheet("CompletedPeople"); // Change to your desired sheet name
                workbook.SaveAs(_excelFilePath);
            }

            return workbook;
        }
        private DateTime? GetSafeDateValue(IXLCell cell)
        {
            DateTime dateValue;
            if (DateTime.TryParse(cell.Value.ToString(), out dateValue))
            {
                return dateValue;
            }
            else
            {
                Debug.WriteLine($"Error parsing date from Excel cell: {cell.Address} - Value: {cell.Value}");
                return null; // Return null if the date cannot be parsed
            }
        }
    }
}
