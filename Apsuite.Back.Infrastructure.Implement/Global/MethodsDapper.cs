using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Apsuite.Back.Infrastructure.Implement.Global
{
    public static class MethodsDapper
    {
        public static IEnumerable<dynamic> QueryProcedure(this DbConnection ctx, string procedure, DynamicParameters dynamicParameters)
        {
            return ctx.Query<dynamic>(procedure, dynamicParameters, null, true, 999999, commandType: CommandType.StoredProcedure);
        }

        public async static Task<IEnumerable<dynamic>> QueryFunction(this DbConnection ctx, string function, DynamicParameters dynamicParameters)
        {
            return await ctx.QueryAsync<dynamic>(function, dynamicParameters, null, 999999);
        }

        public async static Task<IEnumerable<dynamic>> QueryProcedure<dynamic>(this DbConnection ctx, string procedure, DynamicParameters dynamicParameters)
        {
            return await ctx.QueryAsync<dynamic>(procedure, dynamicParameters, null, 999999, commandType: CommandType.StoredProcedure);
        }

        public async static Task<GridReader> QueryProcedureMult(this DbConnection ctx, string procedure, object dynamicParameters)
        {
            return await ctx.QueryMultipleAsync(procedure, dynamicParameters, commandTimeout: 600, commandType: CommandType.StoredProcedure);
        }

        public async static Task<GridReader> QueryProcedureMult(this DbConnection ctx, string procedure)
        {
            return await ctx.QueryMultipleAsync(procedure, commandType: CommandType.StoredProcedure);
        }
    }
}
