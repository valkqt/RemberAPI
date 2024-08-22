using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class RemberDbContext
    {
        private readonly IConfiguration _configuration;

        public RemberDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Connect() => new SqlConnection(_configuration.GetConnectionString("RemberConn"));
    }
}
