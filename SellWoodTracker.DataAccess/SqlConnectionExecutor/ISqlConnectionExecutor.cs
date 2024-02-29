using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.DataAccess.SqlConnectionExecutor
{
    public interface ISqlConnectionExecutor
    {
        void Execute(Action<IDbConnection> action);
        T Execute<T>(Func<IDbConnection, T> executeFunction);
    }
}
