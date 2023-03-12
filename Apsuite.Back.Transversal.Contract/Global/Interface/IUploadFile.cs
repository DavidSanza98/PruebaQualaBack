using Apsuite.Back.Transversal.Contract.Global.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Transversal.Contract.Global.Interface
{
    public interface IUploadFile
    {
        Task<List<UploadRes>> UploadFiles(IFormFileCollection files, string userId);
        Task<List<UploadRes>> UploadFilesByFile(IFormFile files, string complementaryPath);
        ConvertImageToBase64Res ConvertImageToBase64(string url);
    }
}
