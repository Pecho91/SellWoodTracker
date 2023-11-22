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
       // void DeletePerson(PersonModel model);
        
        List<PersonModel> GetRequestedPeople_All();
        List<PersonModel> GetCompletedPeople_All();


    }
}
