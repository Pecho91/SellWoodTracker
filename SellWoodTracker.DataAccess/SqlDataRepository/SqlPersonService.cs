using SellWoodTracker.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.DataAccess.SqlDataAccess
{
    public class SqlPersonService
    {
        private readonly ISqlPersonRepository _repository;

        public SqlPersonService(ISqlPersonRepository repository)
        {
            _repository = repository;
        }

        public void CreatePerson(PersonModel model)
        {
            _repository.CreatePerson(model);
        }

        public void MoveRequestedPersonToCompletedService(int id)
        {
            _repository.MoveRequestedPersonToCompleted(id);
        }

        public void DeletePersonFromRequestedService(int id)
        {
            _repository.DeletePersonFromRequested(id);
        }

        public void DeletePersonFromCompletedService(int id)
        {
            _repository.DeletePersonFromCompleted(id);
        }

        public decimal GetTotalGrossIncomeFromCompletedService()
        {
           return _repository.GetTotalGrossIncomeFromCompleted();
        }

        public decimal GetTotalMetricAmountFromCompletedService()
        {
            return _repository.GetTotalMetricAmountFromCompleted();
        }

    }
}
