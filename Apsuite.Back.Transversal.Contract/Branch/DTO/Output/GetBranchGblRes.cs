using Apsuite.Back.Transversal.Contract.Global.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Transversal.Contract.Branch.DTO.Output
{
    public class GetBranchGblRes : SimpleResultRes<List<GetBranchGblItemRes>>
    {
    }

    public class GetBranchGblItemRes
    {
        public int? Id { get; set; }
        public int? Code { get; set; }
        public string? Description { get; set; }
        public string? Adress { get; set; }
        public string? Identificacion { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? Currency { get; set; }
        public string? DescriptionCurrency { get; set; }
    }
}
