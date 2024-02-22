using SellWoodTracker.Common.Model;
using SellWoodTracker.DataAccess.SqlDataRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.Services.SqlServices
{
    public class SqlPersonService : ISqlPersonService
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

        public PersonModel GetPersonById(int personId)
        {
            return _repository.GetPersonById(personId);
        }

        public List<PersonModel> GetRequestedPeople_All()
        {
            return _repository.GetRequestedPeople_All();
        }

        public List<PersonModel> GetCompletedPeople_All()
        {
            return _repository.GetCompletedPeople_All();
        }

        public void MoveRequestedPersonToCompleted(int personId)
        {
            _repository.MoveRequestedPersonToCompleted(personId);
        }

        public void DeletePersonFromRequested(int personId)
        {
            _repository.DeletePersonFromRequested(personId);
        }

        public void DeletePersonFromCompleted(int personId)
        {
            _repository.DeletePersonFromCompleted(personId);
        }

        public decimal GetTotalGrossIncomeFromCompleted()
        {
            return _repository.GetTotalGrossIncomeFromCompleted();
        }

        public decimal GetTotalMetricAmountFromCompleted()
        {
            return _repository.GetTotalMetricAmountFromCompleted();
        }

    }
}
