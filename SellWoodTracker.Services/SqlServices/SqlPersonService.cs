using SellWoodTracker.Common.Model;
using SellWoodTracker.DataAccess.SqlDataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.Services.SqlServices
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

        // TODO ???
        public PersonModel GetPersonById(int personId)
        {
            throw NotImplementedException();
        }

        private Exception NotImplementedException()
        {
            throw new NotImplementedException();
        }

        public List<PersonModel> GetRequestedPeople_All(PersonModel model)
        {
            _repository.GetRequestedPeople_All(model);
        }
        public List<PersonModel> GetCompletedPeople_All();

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
