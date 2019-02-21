﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
namespace Blog.Data.FileManager
{
    public class FileManager : IFileManager
    {
        private string _imagePath;

        public FileManager(IConfiguration config)
        {
            _imagePath = config["Path:Images"];
        }

        public FileStream ImageStream(string image)
        {
            return new FileStream(Path.Combine(_imagePath, image), FileMode.Open ,FileAccess.Read);
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            try
            {
                var save_dir = Path.Combine(_imagePath);
                //throw new NotImplementedException();
                if (!Directory.Exists(save_dir))
                {
                    Directory.CreateDirectory(save_dir);
                }
                //dont do it this way
                //var fileName = image.FileName;
                var mimeType = image.FileName.Substring(image.FileName.LastIndexOf('.'));
                var fileName = $"img_{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}{mimeType}";


                using (var fileStream = new FileStream(Path.Combine(save_dir, fileName), FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                return fileName;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return "error";
            }
        }
    }
}
