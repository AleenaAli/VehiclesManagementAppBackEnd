using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAppVega1.Models;
using WebAppVega1.Persistance.Interfaces;

namespace WebAppVega1.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPhotoStorage photoStorage;

        public PhotoService(IUnitOfWork unitOfWork, IPhotoStorage photoStorage)
        {
            this.unitOfWork = unitOfWork;
            this.photoStorage = photoStorage;
        }
        public async Task<Photo> UploadPhoto(Vehicle vehicle, IFormFile file, string uploadFolderPath)
        {
            var fileName = await photoStorage.StorePhoto(uploadFolderPath,file);
            var photo = new Photo { FileName = fileName };

            vehicle.Photos.Add(photo);
            await unitOfWork.Complete();
            return photo;
        }
    }
}
