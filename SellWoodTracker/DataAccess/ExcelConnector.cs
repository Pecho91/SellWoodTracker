using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using SellWoodTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
            return GetPeopleFromExcel("RequestedPeople");
        }

        public List<PersonModel> GetCompletedPeople_All()
        {
            return GetPeopleFromExcel("CompletedPeople");
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
                            
                            worksheet.Cell(1, 1).Value = "Id";
                            worksheet.Cell(1, 2).Value = "First Name";
                            worksheet.Cell(1, 3).Value = "Last Name";
                            worksheet.Cell(1, 4).Value = "Email";
                            worksheet.Cell(1, 5).Value = "Cellphone";
                            worksheet.Cell(1, 6).Value = "Date";
                            worksheet.Cell(1, 5).Value = "Metric Amount";
                            worksheet.Cell(1, 6).Value = "Metric Price";
                          
                            var range = worksheet.Range("A1:H1");
                            range.Style.Font.Bold = true;
                            range.Style.Fill.BackgroundColor = XLColor.LightGray;
                        }

                        int nextId = 1;
                        if (lastRow > 0)
                        {
                            int tempId;
                            if (worksheet.Cell(lastRow, 1).TryGetValue(out tempId))
                            {
                                nextId = tempId + 1;
                            }
                            else
                            {
                                // Handle invalid or non-integer value in the cell
                                Debug.WriteLine($"Cell A{lastRow} does not contain a valid integer.");
                            }
                        }

                        worksheet.Cell(lastRow + 1, 1).Value = nextId;
                        worksheet.Cell(lastRow + 1, 2).Value = person.FirstName;
                        worksheet.Cell(lastRow + 1, 3).Value = person.LastName;
                        worksheet.Cell(lastRow + 1, 4).Value = person.CellphoneNumber;
                        worksheet.Cell(lastRow + 1, 5).Value = person.EmailAddress;
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

                if (workbook != null)
                {
                    var worksheet = workbook.Worksheet(sheetName);

                    if (worksheet != null)
                    {
                        var range = worksheet.RangeUsed();

                        if (range != null)
                        {
                            var rows = range.RowsUsed()?.Skip(1);

                            if (rows != null)
                            {
                                foreach (var row in rows)
                                {
                                    var person = new PersonModel()
                                    {
                                        Id = row.Cell(1).GetValue<int>(),
                                        FirstName = row.Cell(2).GetValue<string>(),
                                        LastName = row.Cell(3).GetValue<string>(),
                                        CellphoneNumber = row.Cell(4).GetValue<string>(),
                                        EmailAddress = row.Cell(5).GetValue<string>(),
                                        Date = row.Cell(6).GetValue<DateTime>().ToString("dd.MMM.yyyy."),
                                        MetricAmount = row.Cell(7).GetValue<int>(),
                                        MetricPrice = row.Cell(8).GetValue<decimal>()
                                    };

                                    people.Add(person);
                                }
                            }
                            else
                            {
                                Debug.WriteLine($"No rows found in '{sheetName}'.");
                            }
                        }
                        else
                        {
                            Debug.WriteLine($"No used range found in '{sheetName}'.");
                        }
                    }
                    else
                    {
                        Debug.WriteLine($"Worksheet '{sheetName}' not found.");
                    }
                }
                else
                {
                    Debug.WriteLine("Workbook not obtained.");
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

                var rowToDelete = worksheet.RowsUsed().FirstOrDefault(r => r.Cell(1).GetValue<int>() == personId);

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
    }
}
