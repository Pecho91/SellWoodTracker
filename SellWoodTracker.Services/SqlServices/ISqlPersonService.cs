using SellWoodTracker.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.Services.SqlServices
{
    public interface ISqlPersonService
    {
        public void CreatePerson(PersonModel model);
        public void MoveRequestedPersonToCompletedService(int id);
        public void DeletePersonFromRequestedService(int id);
        public void DeletePersonFromCompletedService(int id);
        public decimal GetTotalGrossIncomeFromCompletedService();
        public decimal GetTotalMetricAmountFromCompletedService();
       
    }
}
