using Apsuite.Back.Transversal.Contract.Branch.DTO.Output;
using Apsuite.Back.Transversal.Contract.Global.DTO;
using Apsuite.Back.Transversal.Contract.Global.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Infrastructure.Contract.Branch.DTO.Output
{
    public class DeleteBranchQryResponse
    {
        public bool? ESTADO_TRANSACCION { get; set; }
    }
    public class DeleteBranchSpMap
    {
        public List<DeleteBranchQryResponse>? Response { get; set; }
    }
    public class DeleteBranchSpRes : SimpleResultRes<DeleteBranchSpMap>
    {
        public static implicit operator DeleteBranchSpRes(DeleteBranchSpMap data)
        {
            DeleteBranchSpRes result = new()
            {
                IsSuccess = (data != null && data.Response != null && data.Response.Count > 0)
            };

            if (result.IsSuccess)
            {
                result.Data = data;
                result.Messages.Add(new SimpleResultItmStatusInfoRes
                {
                    Place = "Infrastructure",
                    Level = SimpleResultItmStatusLevel.Info,
                    Code = "DDBB-200"
                });
            }
            else if (data?.Response?.Count <= 0)
            {
                result.IsSuccess = false;
                result.Messages.Add(new SimpleResultItmStatusInfoRes
                {
                    Place = "Infrastructure",
                    Level = SimpleResultItmStatusLevel.Warning,
                    Code = "DDBB-404"
                });
            }
            else
            {
                result.IsSuccess = false;
                result.Messages.Add(new SimpleResultItmStatusInfoRes
                {
                    Place = "Infrastructure",
                    Level = SimpleResultItmStatusLevel.Error,
                    Code = "DDBB-500"
                });
            }
            return result;
        }

        public static implicit operator DeleteBranchGblRes(DeleteBranchSpRes data)
        {
            return new DeleteBranchGblRes
            {
                Data = data.Data?.Response?.Select(d => new DeleteBranchGblItemRes
                {
                    StatusCode = d.ESTADO_TRANSACCION
                }).FirstOrDefault(),
                IsSuccess = data.IsSuccess,
                Messages = data.Messages
            };
        }
    }
}
