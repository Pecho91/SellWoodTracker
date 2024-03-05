using Dapper;
using SellWoodTracker.DataAccess.SqlConnectionExecutors;
using SellWoodTracker.DataAccess.SqlDataInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.DataAccess.SqlDataRepositories
{
    public class SqlTotalGrossIncomeRetriever : ISqlTotalGrossIncomeRetriever
    {
        private readonly ISqlConnectionExecutor _sqlConnectionExecutor;

        public SqlTotalGrossIncomeRetriever (ISqlConnectionExecutor sqlConnectionExecutor)
        {
            _sqlConnectionExecutor = sqlConnectionExecutor ?? throw new ArgumentNullException (nameof (sqlConnectionExecutor));
        }
        public decimal GetTotalGrossIncomeFromCompleted()
        {
            return _sqlConnectionExecutor.Execute(connection =>
            {
                return connection.Query<decimal>("dbo.spCompletedPeople_GetTotalGrossIncome").FirstOrDefault();
            });
        }
    }
}
