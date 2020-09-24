using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppVega1.Controllers.Resources;
using WebAppVega1.Models;
using WebAppVega1.Persistance;

namespace WebAppVega1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakesController : ControllerBase
    {
        private readonly VegaDbContext context;
        //private readonly IMapper mapper;

        public MakesController(VegaDbContext context)
        {
            this.context = context;
            //this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<Make>> GetAll()
        {
           // var makes = await context.Makes.Include(m=>m.Models).ToListAsync();
          //  return mapper.Map<List<Make>, List<MakeResource>>(makes);
          return await context.Makes.Include(m => m.Models).ToListAsync();
        }
    }
}