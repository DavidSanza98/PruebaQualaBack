using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Infrastructure.Implement.Global
{
    public class BaseRepository
    {
        private readonly string _connectionString;
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString:Transactional"]!;
        }

        public ApsuiteEntities GetContext()
        {
            DbContextOptionsBuilder<ApsuiteEntities> options = new DbContextOptionsBuilder<ApsuiteEntities>();
            options.UseSqlServer(_connectionString);
            var ctx = new ApsuiteEntities(options.Options);
            ctx.Database.SetCommandTimeout(999999);

            return ctx;
        }

        public partial class ApsuiteEntities : DbContext
        {
            public ApsuiteEntities()
            {
            }

            public ApsuiteEntities(DbContextOptions<ApsuiteEntities> options)
                : base(options)
            {
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    
                }
            }
        }
    }
}
