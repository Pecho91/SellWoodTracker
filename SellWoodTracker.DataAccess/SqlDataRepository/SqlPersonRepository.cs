﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellWoodTracker.GlobalConfig;
using SellWoodTracker.Common.Model;
using System.Data;
using System.Runtime.CompilerServices;
using Dapper;
using SellWoodTracker.DataAccess.SqlDataRepository;
using System.Data.Common;

namespace SellWoodTracker.DataAccess.SqlDataAccess
{
    public class SqlPersonRepository : ISqlPersonRepository
    {
        
        private readonly SqlConnectionFactory _sqlConnectionFactory;
        private readonly SqlDynamicParametersBuilder _sqlDynamicParametersBuilder;

        public SqlPersonRepository(IGlobalConfig globalConfig,SqlConnectionFactory connectionFactory, SqlDynamicParametersBuilder sqlDynamicParametersBuilder)
        {
               
            _sqlConnectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(_sqlConnectionFactory));
            _sqlDynamicParametersBuilder = sqlDynamicParametersBuilder ?? throw new ArgumentNullException(nameof(sqlDynamicParametersBuilder);
        }

        public void CreatePerson(PersonModel model)
        {
            using (IDbConnection connection = _sqlConnectionFactory?.CreateSqlConnection())
            {
                var p = _sqlDynamicParametersBuilder.BuildParametersForPerson(model);
                connection.Execute("dbo.spRequestedPeople_Insert", p, commandType: CommandType.StoredProcedure);
                model.Id = p.Get<int>("@id");
            }
        }


        public PersonModel? GetPersonById(int personId)
        {
            using (IDbConnection connection = _sqlConnectionFactory.CreateSqlConnection())
            {
                return connection?.QueryFirstOrDefault<PersonModel>("dbo.spRequestedPeople_GetById",
                            new { Id = personId }, commandType: CommandType.StoredProcedure);
            }
        }
        //TODO
      
        public List<PersonModel> GetRequestedPeople_All()
        {
            using (IDbConnection connection = _sqlConnectionFactory.CreateSqlConnection())
            {
                return connection.Query<PersonModel>("dbo.spRequestedPeople_GetAll").ToList();
            }
        }

        public List<PersonModel> GetCompletedPeople_All()
        {
            using (IDbConnection connection = _sqlConnectionFactory.CreateSqlConnection())
            {
                return connection.Query<PersonModel>("dbo.spCompletedPeople_GetAll").ToList();
            }
        }

        public void MoveRequestedPersonToCompleted(int personId)
        {
            using (IDbConnection connection = _sqlConnectionFactory.CreateSqlConnection())
            {
                var person = GetPersonById(personId);

                if (person != null)
                {
                    _sqlPersonService.MoveRequestedPersonToCompleted(connection, person);
                }
            }
        }

        public void DeletePersonFromRequested(int personId)
        {
            throw new NotImplementedException();
        }

        public void DeletePersonFromCompleted(int personId)
        {
            throw new NotImplementedException();
        }

        public decimal GetTotalGrossIncomeFromCompleted()
        {
            throw new NotImplementedException();
        }

        public decimal GetTotalMetricAmountFromCompleted()
        {
            throw new NotImplementedException();
        }
    }
}