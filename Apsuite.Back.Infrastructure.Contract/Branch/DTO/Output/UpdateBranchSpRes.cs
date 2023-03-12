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
    public class UpdateBranchQryResponse
    {
        public bool? ESTADO_TRANSACCION { get; set; }
    }
    public class UpdateBranchSpMap
    {
        public List<UpdateBranchQryResponse>? Response { get; set; }
    }
    public class UpdateBranchSpRes : SimpleResultRes<UpdateBranchSpMap>
    {
        public static implicit operator UpdateBranchSpRes(UpdateBranchSpMap data)
        {
            UpdateBranchSpRes result = new()
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

        public static implicit operator UpdateBranchGblRes(UpdateBranchSpRes data)
        {
            return new UpdateBranchGblRes
            {
                Data = data.Data?.Response?.Select(d => new UpdateBranchGblItemRes
                {
                    StatusCode = d.ESTADO_TRANSACCION
                }).FirstOrDefault(),
                IsSuccess = data.IsSuccess,
                Messages = data.Messages
            };
        }
    }
}
