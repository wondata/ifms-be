using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Data.SqlClient;
using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Domain.Models;

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
            var result = new List<LookupModel>(); //_dbContext.Set<LookupModel>().FromSql(commandText).ToList();
            return  Task.FromResult<List<LookupModel>>(result);
        }

        public Task<LookupModel> Get(Guid id, string table)
        {
            //var param = new SqlParameter("id", id);

            var commandText = $"Select * From  {table} Where Id=@id";
            var result = new List<LookupModel>(); // _dbContext.Set<LookupModel>().FromSql(commandText, param).ToList();

            return Task.FromResult<LookupModel>(result.FirstOrDefault());
        }

    }
}
