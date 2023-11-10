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

        List<PersonModel> GetPerson_All();
    }
}
