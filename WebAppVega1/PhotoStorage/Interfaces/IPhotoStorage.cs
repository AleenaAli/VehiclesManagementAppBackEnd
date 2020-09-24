using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppVega1
{
    public interface IPhotoStorage
    {
        Task<string> StorePhoto(string uploadsFolderPath, IFormFile file);
    }
}
