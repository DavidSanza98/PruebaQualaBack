using Apsuite.Back.Transversal.Contract.Global.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Transversal.Contract.Branch.DTO.Output
{
    public class GetCurrencyGblRes : SimpleResultRes<List<GetCurrencyGblItemRes>>
    {
    }

    public class GetCurrencyGblItemRes
    {
        public int? Id { get; set; }
        public string? Description { get; set; }
    }
}
