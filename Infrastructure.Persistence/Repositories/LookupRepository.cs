using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;
using Application.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.Persistence.Contexts;

namespace CyberErp.Infrastructure.Repository
{
    public class LookupRepository : ILookupRepository
    {
        private LookupContext _dbContext { get; set; }

        public LookupRepository(LookupContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<LookupModel>> GetAllLookup(string table)
        {
            var commandText = string.Format("Select * from {0}", table);
            var result =  _dbContext.Set<LookupModel>().FromSqlRaw(commandText).ToList(); //new List<LookupModel>();
            return  Task.FromResult<List<LookupModel>>(result);
        }

        public Task<LookupModel> Get(Guid id, string table)
        {
            //var param = new SqlParameter("id", id);

            var commandText = $"Select * From  {table} Where Id=@id";
            var result = new List<LookupModel>(); // _dbContext.Set<LookupModel>().FromSql(commandText, param).ToList();

            return Task.FromResult<LookupModel>(result.FirstOrDefault());
        }

        public Task<LookupModel> Get(string table)
        {
            var commandText = $"Select * From  {table} ";
            var result =  _dbContext.Set<LookupModel>().FromSqlRaw(commandText).ToList(); // new List<LookupModel>();

            return Task.FromResult<LookupModel>(result.FirstOrDefault());
        }
    }
}
