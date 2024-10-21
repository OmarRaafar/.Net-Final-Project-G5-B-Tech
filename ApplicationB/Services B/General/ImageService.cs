using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Services_B.General
{
    public class ImageService
    {
        //private readonly IWebHostEnvironment webHostEnvironment;

        //public ImageService(IWebHostEnvironment webHostEnvironment)
        //{
        //    this.webHostEnvironment = webHostEnvironment;
        //}

        //private string SaveImageAndGetUrl(IFormFile file)
        //{
        //    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "ImageUrls");

        //    // Ensure the upload folder exists
        //    if (!Directory.Exists(uploadFolder))
        //    {
        //        Directory.CreateDirectory(uploadFolder);
        //    }

        //    // Generate a unique file name
        //    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        //    string filePath = Path.Combine(uploadFolder, uniqueFileName);

        //    // Save the image to the server
        //    using (var fileStream = new FileStream(filePath, FileMode.Create))
        //    {
        //        file.CopyToAsync(fileStream); // Await the file copy operation
        //    }

        //    // Return the relative URL to the saved image
        //    return Path.Combine("ImageUrls", uniqueFileName).Replace("\\", "/");
        //}
    }
}
