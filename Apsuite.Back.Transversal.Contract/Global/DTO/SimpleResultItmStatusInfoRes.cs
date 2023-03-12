using Apsuite.Back.Transversal.Contract.Global.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Transversal.Contract.Global.DTO
{
    public class SimpleResultItmStatusInfoRes
    {
        public string? Id { get; set; }
        public string? Place { get; set; }
        public SimpleResultItmStatusLevel Level { get; set; }
        public bool CanUserSeeIt { get; set; }

        public string? Code { get; set; }
        public Dictionary<string, dynamic>? Info { get; set; }
    }
}
