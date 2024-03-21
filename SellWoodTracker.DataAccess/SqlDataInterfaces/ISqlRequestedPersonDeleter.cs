using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.DataAccess.SqlDataInterfaces
{
    public interface ISqlRequestedPersonDeleter
    {
        void DeletePersonFromRequested(int personId);
        
    }
}
