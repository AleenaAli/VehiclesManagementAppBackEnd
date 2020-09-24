using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebAppVega1.Models;
using WebAppVega1.Persistance;
using WebAppVega1.Persistance.Interfaces;
using WebAppVega1.Services;

namespace WebAppVega1.Controllers
{
    [Route("/api/vehicles/photos")]
    [ApiController]

    public class PhotosController : ControllerBase
    {
        private IHostingEnvironment host;
        private readonly IPhotoService photoService;
        private readonly PhotoSettings options;
        private readonly VegaDbContext context;


        public PhotosController(IHostingEnvironment host,IPhotoService photoService, VegaDbContext context,IOptionsSnapshot<PhotoSettings> options)
        {
            this.host = host;
            this.photoService = photoService;
            this.context = context;
            this.options = options.Value;
        }

        [HttpPost("{vehicleId}")]
        [Authorize]
        public async Task<IActionResult> Upload(int vehicleId ,[FromForm] IFormFile file)
        {
            Vehicle vehicle =await context.Vehicles.FindAsync(vehicleId);
            if (vehicle == null)
            {
                return NotFound();
            }

            if (file == null)
                return BadRequest("Null File");
            if (file.Length == 0)
                return BadRequest("Empty File");
            if (file.Length >= this.options.MaxBytes)
                return BadRequest("Max File size exceeded");
            if (!this.options.IsSupported(file.FileName))
                return BadRequest("Invalid File type");

            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");

            var photo = await photoService.UploadPhoto(vehicle, file, uploadsFolderPath);

            return Ok(photo);
        }

        [HttpGet("{vehicleId}")]
        public async Task<IActionResult> GetPhotos(int vehicleId)
        {
            Photo[] photos =await context.Photos.Where(v => v.vehicleId == vehicleId).ToArrayAsync();
            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");
            return Ok(photos);
        }

        [HttpGet("path")]
        public async Task<IActionResult> GetPath()
        {
            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");
            return Ok(uploadsFolderPath);

        }

    }
}