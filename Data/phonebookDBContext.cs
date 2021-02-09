using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

namespace Data
{
    public class phonebookDBContext: DbContext
    {
        public IConfiguration Configuration { get; }
        public phonebookDBContext(DbContextOptions<phonebookDBContext> options, IConfiguration configuration): base(options)
        {
            Configuration = configuration;
        }


        public static IDbConnection MyConnection(string connect)
        {
            return new SqlConnection(connect);
        }

        public static IEnumerable<T> ExecuteProc<T>(string proc, DynamicParameters param, IDbConnection db)
        {
            return db.Query<T>(proc, param, commandType: CommandType.StoredProcedure);
        }
    }
}
