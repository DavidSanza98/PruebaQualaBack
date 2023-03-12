using Apsuite.Back.Infrastructure.Contract.Branch.DTO.Output;
using Apsuite.Back.Infrastructure.Contract.Branch.Interface;
using Apsuite.Back.Infrastructure.Implement.Global;
using Apsuite.Back.Transversal.Contract.Branch.DTO.Input;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Apsuite.Back.Infrastructure.Implement.Branch
{
    public class BranchRepository<DbType> : DbManager<DbType>, IBranchRepository where DbType : DbConnection
    {
        public BranchRepository(IConfiguration configuration, string connectionStringSection, string? settingsSection = null) : base(configuration, connectionStringSection, settingsSection)
        {
        }

        public async Task<GetBranchSpRes> GetBranch()
        {
            GetBranchSpRes result = await QueryProcedureMult("PA_LEER_SUCURSAL",
                (GridReader reader) =>
                {
                    return new GetBranchSpMap()
                    {
                        Response = reader.Read<GetBranchQryResponse>().ToList(),
                    };
                });
            return result;
        }

        public async Task<CreateBranchSpRes> CreateBranch(CreateBranchGblReq data)
        {
            CreateBranchSpRes result = await QueryProcedureMult("PA_INSERTAR_SUCURSAL",
                (GridReader reader) =>
                {
                    return new CreateBranchSpMap()
                    {
                        Response = reader.Read<CreateBranchQryResponse>().ToList(),
                    };
                }, new
                {
                    CODIGO = data.Code,
                    DESCRIPCION = data.Description,
                    DIRECCION = data.Adress,
                    IDENTIFICACION = data.Identificacion,
                    FECHA_CREACION = data.CreationDate,
                    ID_MONEDA = data.Currency
                });
            return result;
        }

        public async Task<UpdateBranchSpRes> UpdateBranch(UpdateBranchGblReq data)
        {
            UpdateBranchSpRes result = await QueryProcedureMult("PA_ACTUALIZAR_SUCURSAL",
                (GridReader reader) =>
                {
                    return new UpdateBranchSpMap()
                    {
                        Response = reader.Read<UpdateBranchQryResponse>().ToList(),
                    };
                }, new
                {
                    ID = data.Id,
                    CODIGO = data.Code,
                    DESCRIPCION = data.Description,
                    DIRECCION = data.Adress,
                    IDENTIFICACION = data.Identificacion,
                    FECHA_CREACION = data.CreationDate,
                    ID_MONEDA = data.Currency
                });
            return result;
        }

        public async Task<DeleteBranchSpRes> DeleteBranch(DeleteBranchGblReq data)
        {
            DeleteBranchSpRes result = await QueryProcedureMult("PA_ELIMINAR_SUCURSAL",
                (GridReader reader) =>
                {
                    return new DeleteBranchSpMap()
                    {
                        Response = reader.Read<DeleteBranchQryResponse>().ToList(),
                    };
                }, new
                {
                    ID = data.Id
                });
            return result;
        }

        public async Task<GetCurrencySpRes> GetCurrency()
        {
            GetCurrencySpRes result = await QueryProcedureMult("PA_LEER_MONEDA",
                (GridReader reader) =>
                {
                    return new GetCurrencySpMap()
                    {
                        Response = reader.Read<GetCurrencyQryResponse>().ToList(),
                    };
                });
            return result;
        }

        public async Task<GetBranchByIdSpRes> GetBranchById(GetBranchByIdGblReq data)
        {
            GetBranchByIdSpRes result = await QueryProcedureMult("PA_LEER_UNA_SUCURSAL",
                (GridReader reader) =>
                {
                    return new GetBranchByIdSpMap()
                    {
                        Response = reader.Read<GetBranchByIdQryResponse>().ToList(),
                    };
                }, new
                {
                    ID = data.Id
                });
            return result;
        }

    }
}
