using SellWoodTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        private const string db = "SellWoodDatabase";
        /// <summary>
        /// Saves a new people to database
        /// </summary>
        /// <param name="model">The people information</param>
        /// <returns>The people information, including the unique identifier</returns>
        /// 
        public void CreatePerson(PersonModel model)
        {
            throw new NotImplementedException();
        }

        public List<PersonModel> GetPerson_All()
        {
            throw new NotImplementedException();
        }

        
    }
}
