using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Transversal.Contract.Global.DTO
{
    public class UploadRes
    {
        public string? File { get; set; }
        public string? Name { get; set; }
        public string? Path { get; set; }
        public string? Url { get; set; }
        public string? UserId { get; set; }
        public DateTime UploadDate { get; set; }

    }
}
