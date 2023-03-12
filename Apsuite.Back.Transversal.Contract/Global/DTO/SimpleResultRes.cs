using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Transversal.Contract.Global.DTO
{
    public class SimpleResultRes<TResult>
    {
        public List<SimpleResultItmStatusInfoRes> Messages { get; set; } = new List<SimpleResultItmStatusInfoRes>();
        public TResult? Data { get; set; }
        public bool IsSuccess { get; set; } = true;
    }
}
