using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppVega1.Models;

namespace WebAppVega1.Services
{
    public interface IPhotoService
    {
        Task<Photo> UploadPhoto(Vehicle vehicle, IFormFile file, string uploadFolderPath);
    }
}
