using SellWoodTracker.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.DataAccess.SqlDataAccess
{
    public class SqlPersonService
    {
        private readonly SqlPersonRepository _repository;

        public SqlPersonService(SqlPersonRepository repository)
        {
            _repository = repository;
        }

    }
}
