using Dapper;
using SellWoodTracker.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.DataAccess.SqlDynamicParameters
{
    public interface ISqlDynamicParametersBuilder
    {
        DynamicParameters GetPersonDynamicParameters(PersonModel model);
    }
}
