﻿using Apsuite.Back.Transversal.Contract.Branch.DTO.Output;
using Apsuite.Back.Transversal.Contract.Global.DTO;
using Apsuite.Back.Transversal.Contract.Global.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Infrastructure.Contract.Branch.DTO.Output
{
    public class GetBranchByIdQryResponse
    {
        public int? ID { get; set; }
        public int? CODIGO { get; set; }
        public string? DESCRIPCION { get; set; }
        public string? DIRECCION { get; set; }
        public string? IDENTIFICACION { get; set; }
        public DateTime? FECHA_CREACION { get; set; }
        public int? ID_MONEDA { get; set; }
        public string? DESCRIPCION_MONEDA { get; set; }
    }
    public class GetBranchByIdSpMap
    {
        public List<GetBranchByIdQryResponse>? Response { get; set; }
    }
    public class GetBranchByIdSpRes : SimpleResultRes<GetBranchByIdSpMap>
    {
        public static implicit operator GetBranchByIdSpRes(GetBranchByIdSpMap data)
        {
            GetBranchByIdSpRes result = new()
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

        public static implicit operator GetBranchByIdGblRes(GetBranchByIdSpRes data)
        {
            return new GetBranchByIdGblRes
            {
                Data = data.Data?.Response?.Select(d => new GetBranchByIdGblItemRes
                {
                    Id = d.ID,
                    Code = d.CODIGO,
                    Description = d.DESCRIPCION,
                    Adress = d.DIRECCION,
                    Identificacion = d.IDENTIFICACION,
                    CreationDate = d.FECHA_CREACION,
                    Currency = d.ID_MONEDA,
                    DescriptionCurrency = d.DESCRIPCION_MONEDA
                }).FirstOrDefault(),
                IsSuccess = data.IsSuccess,
                Messages = data.Messages
            };
        }
    }
}
