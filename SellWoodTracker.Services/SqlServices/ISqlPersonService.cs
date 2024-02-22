﻿using SellWoodTracker.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.Services.SqlServices
{
    public interface ISqlPersonService
    {
        //public void CreatePerson(PersonModel model);
        //public PersonModel GetPersonById();
        //List<PersonModel> GetRequestedPeople_All();
        //List<PersonModel> GetCompletedPeople_All();
        //public void MoveRequestedPersonToCompletedService(int id);
        //public void DeletePersonFromRequestedService(int id);
        //public void DeletePersonFromCompletedService(int id);
        //public decimal GetTotalGrossIncomeFromCompletedService();
        //public decimal GetTotalMetricAmountFromCompletedService();

        void CreatePerson(PersonModel model);
        PersonModel GetPersonById(int personId);
        List<PersonModel> GetRequestedPeople_All();
        List<PersonModel> GetCompletedPeople_All();
        void MoveRequestedPersonToCompleted(int personId);
        void DeletePersonFromRequested(int personId);
        void DeletePersonFromCompleted(int personId);
        decimal GetTotalGrossIncomeFromCompleted();
        decimal GetTotalMetricAmountFromCompleted();
    }
}
