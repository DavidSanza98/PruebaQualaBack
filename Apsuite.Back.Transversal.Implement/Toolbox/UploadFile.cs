using Apsuite.Back.Transversal.Contract.Global.DTO;
using Apsuite.Back.Transversal.Contract.Global.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;

namespace Apsuite.Back.Transversal.Implement.Toolbox
{
    public class UploadFile : IUploadFile
    {
        public readonly string urlPattern;

        private readonly IConfiguration Configuration;
        private readonly string DirectoryBasePath;
        private readonly string UrlBasePath = "/";

        public UploadFile(IConfiguration configuration)
        {
            Configuration = configuration;
            urlPattern = "[^a-zA-Z0-9-.]";

            DirectoryBasePath = Configuration.GetValue<string>("PathUpload");
        }

        public ConvertImageToBase64Res ConvertImageToBase64(string url)
        {
            ConvertImageToBase64Res result = new();
            if (File.Exists(url))
            {
                using Image image = Image.FromFile(url);
                using MemoryStream m = new();
                image.Save(m, image.RawFormat);
                byte[] imageBytes = m.ToArray();
                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                result.Base64 = base64String;
            }

            return result;
        }

        public async Task<List<UploadRes>> UploadFiles(IFormFileCollection files, string userId)
        {
            var uploads = new List<UploadRes>();
            var path = Path.Combine(DirectoryBasePath, userId);

            foreach (var file in files)
            {
                uploads.Add(await AddUpload(file, path, UrlBasePath, userId));
            }

            return uploads;
        }

        public async Task<List<UploadRes>> UploadFilesByFile(IFormFile file, string complementaryPath)
        {
            var uploads = new List<UploadRes>();
            var path = Path.Combine(DirectoryBasePath, complementaryPath);
            uploads.Add(await AddUpload(file, path, UrlBasePath, complementaryPath));

            return uploads;
        }

        private async Task<UploadRes> AddUpload(IFormFile file, string path, string url, string userId)
        {
            var upload = await WriteFile(file, path, url);
            upload.UserId = userId;
            upload.UploadDate = DateTime.Now;

            return upload;
        }

        private async Task<UploadRes> WriteFile(IFormFile file, string path, string url)
        {
            if (!(Directory.Exists(path)))
            {
                Directory.CreateDirectory(path);
            }

            var upload = await CreateUpload(file, path, url);

            using (var stream = new FileStream(upload.Path!, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return upload;
        }

        private async Task<UploadRes> CreateUpload(IFormFile file, string path, string url)
        {
            var name = await CreateSafeName(file, path);

            var upload = new UploadRes
            {
                File = name,
                Name = file.Name,
                Path = $"{path}\\{name}",
                Url = $"{url}\\{name}"
            };

            return upload;
        }


        private async Task<string> CreateSafeName(IFormFile file, string path)
        {
            var fileName = await UrlEncode(file.FileName);
            string newName = fileName;
            var timeLong = DateTime.Now.ToString("yyyyMMddhhmmssfffff");

            var extension = Path.GetExtension(fileName);
            newName = $"{fileName.Replace($"{extension}", "")}_{timeLong}{extension}";
            return newName;
        }

        private async Task<string> UrlEncode(string url)
        {
            var friendlyUrl = Regex.Replace(url, @"\s", "-").ToLower();
            friendlyUrl = Regex.Replace(friendlyUrl, urlPattern, string.Empty);
            return friendlyUrl;
        }
    }
}
