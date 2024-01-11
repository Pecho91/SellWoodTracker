using SellWoodTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.DataAccess
{
    public interface IDataConnection
    {
        void CreatePerson(PersonModel model);

        void MoveRequestedPersonToCompleted(int personId);
        void DeletePersonFromRequested(int personId);

        void DeletePersonFromCompleted(int personId);

        List<PersonModel> GetRequestedPeople_All();
        List<PersonModel> GetCompletedPeople_All();

        decimal GetTotalGrossIncomeFromCompleted();

        decimal GetTotalMetricAmountFromCompleted();

        decimal GetGrossIncomeFromCompleted();

    }
}
