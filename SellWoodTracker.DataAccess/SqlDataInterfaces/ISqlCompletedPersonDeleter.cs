using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.DataAccess.SqlDataInterfaces
{
    public interface ISqlCompletedPersonDeleter
    {
        void DeletePersonFromCompleted(int personId);
    }
}
