using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Transversal.Contract.Global.DTO
{
    public class Result<TResult>
    {
        public List<Status> Messages { get; set; } = new List<Status>();
        public bool IsSuccess { get; set; } = true;
        public TResult? Data { get; set; }
        public int? RecordQuantity { get; set; } = null;
        public int? PageQuantity { get; set; } = null;
        public int? PageNumber { get; set; } = null;
        public int? PageSize { get; set; } = null;
    }
}
