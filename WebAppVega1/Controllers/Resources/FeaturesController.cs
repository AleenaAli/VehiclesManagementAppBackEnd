using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppVega1.Models;
using WebAppVega1.Persistance.Interfaces;

namespace WebAppVega1.Controllers.Resources
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IRepositoryInterface<Feature> repository;

        public FeaturesController(IRepositoryInterface<Feature> repository)
        {
            this.repository = repository;
        }
        

        [HttpGet]
        public async Task<Feature[]> GetFeatures()
        {
            return repository.GetList();
        }
    }
}