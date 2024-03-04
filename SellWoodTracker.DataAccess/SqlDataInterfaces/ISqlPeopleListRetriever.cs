using SellWoodTracker.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.DataAccess.SqlDataInterfaces
{
    public interface ISqlPeopleListRetriever
    {
        List<PersonModel> GetRequestedPeople_All();
        List<PersonModel> GetCompletedPeople_All();
    }
}
