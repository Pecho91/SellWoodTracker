using Dapper;
using SellWoodTracker.Common.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.DataAccess
{
    public interface ISqlPersonRepository
    {
        void CreatePerson(PersonModel model);
        public PersonModel GetPersonById(int personId);
        List<PersonModel> GetRequestedPeople_All();
        List<PersonModel> GetCompletedPeople_All();
        void MoveRequestedPersonToCompleted(int personId);
        void DeletePersonFromRequested(int personId);
        void DeletePersonFromCompleted(int personId);
        decimal GetTotalGrossIncomeFromCompleted();
        decimal GetTotalMetricAmountFromCompleted();

    
    }
}

