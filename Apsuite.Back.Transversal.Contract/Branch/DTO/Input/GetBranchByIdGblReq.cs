using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Transversal.Contract.Branch.DTO.Input
{
    public class GetBranchByIdGblReq
    {
        [FromQuery]
        public int? Id { get; set; }
    }
}
