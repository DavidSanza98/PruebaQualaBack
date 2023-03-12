using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Apsuite.Back.Infrastructure.Implement.Global
{
    public abstract class DbManager<DbType> where DbType : DbConnection
    {
        private readonly IConfiguration _Configuration;
        private readonly string? _SettingsSection;
        private readonly string? _ConnectionString;
        protected BaseRepository baseRepository;

        public DbManager(IConfiguration configuration, string connectionStringSection, string? settingsSection = null) : base()
        {
            _Configuration = configuration;
            baseRepository = new BaseRepository(configuration);

            if (settingsSection != null && _Configuration.GetSection(settingsSection).Exists()) _SettingsSection = settingsSection;
            else
            {
                _SettingsSection = GetType().Name;

                int limit = _SettingsSection?.IndexOf("`") ?? 0;
                limit = limit < 0 ? _SettingsSection?.Length ?? 0 : limit;

                _SettingsSection = _SettingsSection?.Substring(0, limit);

                if (!_Configuration.GetSection(_SettingsSection!).Exists()) _SettingsSection = null;
            }

            _ConnectionString = _Configuration[connectionStringSection];
        }

        public async Task<TOutput> QueryProcedureMult<TOutput>(string procedure, Func<GridReader, TOutput> converter, object? input = null)
        {
            using (DbConnection _DbConnection = (DbConnection)Activator.CreateInstance(typeof(DbType), _ConnectionString)!)
            {
                GridReader gridReader = await (input != null ? _DbConnection!.QueryProcedureMult(procedure, input) :
                                     _DbConnection!.QueryProcedureMult(procedure));
                var result = converter(gridReader);

                if (_DbConnection!.State == ConnectionState.Open) await _DbConnection.CloseAsync();
                await _DbConnection.DisposeAsync();

                return result;
            }
        }
    }
}
