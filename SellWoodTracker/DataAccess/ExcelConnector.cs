using SellWoodTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.DataAccess
{
    public class ExcelConnector : IDataConnection
    {
        public void CreatePerson(PersonModel model)
        {
            SavePersonToExcel(model);
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

        public void SavePersonToExcel(PersonModel person)
        {
            using (var workbook = GetOrCreateWorkbook())
            {
                var worksheet = workbook.Worksheets("RequestedPeople");

                int lastRow = worksheet.LastRowUsed().RowNumber();

                int nextId = lastRow > 0 ? worksheet.Cell(lastRow, 1).GetValue<int>() + 1 : 1;

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
        }

        private void CreatePersonInCompleted(PersonModel person)
        {
            using (var workbook = GetOrCreateWorkbook())
            {
                var worksheet = workbook.Worksheet("CompletedPeople");

                int lastRow = worksheet.LastRowUsed().RowNumber();

                int nextId = lastRow > 0 ? worksheet.Cell(lastRow, 1).GetValue<int>() + 1 : 1;

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
        }

        private List<PersonModel> GetPeopleFromExcel(string sheetName)
        {
            var people = new List<PersonModel>();

            using (var workbook = new XLWorkbook(excelFilePath))
            {
                var worksheet = workbook.Worksheet(sheetName);

                var rows = worksheet.RangeUsed().RowUsed().Skip(1);

                foreach (var row in rows)
                {
                    var person = new PersonModel()
                    {
                        Id = row.Cell(1).GetValue<int>(),
                        FirstName = row.Cell(2).GetValue<string>(),
                        LastName = row.Cell(3).GetValue<string>(),
                        CellphoneNumber = row.Cell(4).GetValue<string>(),
                        EmailAddress = row.Cell(5).GetValue<string>(),
                        Date = row.Cell(6).GetValue<DateTime>(),
                        MetricAmount = row.Cell(7).GetValue<int>(),
                        MetricPrice = row.Cell(8).GetValue<decimal>()
                    };

                    people.Add(person);
                }
            }
        }
    }
}
