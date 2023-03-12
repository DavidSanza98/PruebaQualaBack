using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Transversal.Contract.Global.DTO
{
    public class Status
    {
        public enum LevelEnum
        {
            Info,
            Warning,
            Error,
            Critical
        }
        public string RequestId { get; set; }
        public string Place { get; set; }
        public LevelEnum Level { get; set; }

        public string Code { get; set; }
        public Dictionary<string, dynamic> Info { get; set; }
    }
}
