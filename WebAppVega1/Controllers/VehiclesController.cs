using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppVega1.Models;
using WebAppVega1.Persistance;
using WebAppVega1.Extensions;
using WebAppVega1.Persistance.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WebAppVega1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly VegaDbContext context;
        private readonly IRepositoryInterface<Vehicle> repository;


        //public VehiclesController(VegaDbContext context)
        //{
        //    this.context = context;
        //}

        public VehiclesController(VegaDbContext context, IRepositoryInterface<Vehicle> repository)
        {
            this.context = context;
            this.repository = repository;
        }
        [HttpGet("dada")]
        public async Task<IActionResult> GetAll()
        {
            //var query = await repository.GetList().ToAsyncEnumerable().ToArray();
            var query = await context.Vehicles.Include("Contact").Include("Model").Include("Features").ToArrayAsync();
            return Ok(query);

        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetVehicle(int filter, string sortCol, bool sortAscending)
        {
            var query = await context.Vehicles.Include(v => v.Model).Include(v => v.Features).Include(v => v.Contact).OrderByDescending(v => v.Id).ToArrayAsync();

            if (filter != 0)
            {
                query = query.Where(v => v.Model.MakeID == filter).ToArray();
            }

            if (sortCol == "contactName")
                query = (sortAscending) ? query.OrderBy(v => v.Contact.ContactName).ToArray() : query.OrderByDescending(v => v.Contact.ContactName).ToArray();
            else if (sortCol == "model")
                query = (sortAscending) ? query.OrderBy(v => v.ModelId).ToArray() : query.OrderByDescending(v => v.ModelId).ToArray();
            else if (sortCol == "make")
                query = (sortAscending) ? query.OrderBy(v => v.Model.MakeID).ToArray() : query.OrderByDescending(v => v.Model.MakeID).ToArray();
            else
                query = (sortAscending) ? query.OrderBy(v => v.Id).ToArray() : query.OrderByDescending(v => v.Id).ToArray();
            return Ok(query);
        }

        [HttpPost("Filtered")]
        public async Task<IActionResult> GetVehicle([FromBody]IQueryableVehicle queryObj)
        {
            var query = context.Vehicles.Include(v => v.Model).Include(v => v.Features).Include(v => v.Contact).OrderByDescending(v => v.Id).AsQueryable();

            query = query.ApplyFiltering(queryObj);

            //if (queryObj.SortBy == "contatName")
            //    query = (queryObj.IsSortAscending) ? query.OrderBy(v => v.Contact.ContactName) : query.OrderByDescending(v => v.Contact.ContactName);
            //else if (queryObj.SortBy == "model")
            //    query = (queryObj.IsSortAscending) ? query.OrderBy(v => v.ModelId) : query.OrderByDescending(v => v.ModelId);
            //else if (queryObj.SortBy == "make")
            //    query = (queryObj.IsSortAscending) ? query.OrderBy(v => v.Model.MakeID) : query.OrderByDescending(v => v.Model.MakeID);
            //else
            //    query = (queryObj.IsSortAscending) ? query.OrderBy(v => v.Id) : query.OrderByDescending(v => v.Id);

            Dictionary<string, Expression<Func<Vehicle, Object>>> columnsMap = new Dictionary<string, Expression<Func<Vehicle, Object>>>()
            {
                ["make"] = v => v.Model.MakeID,
                ["model"] = v => v.Model.Id,
                ["contactName"] = v => v.Contact.ContactName,
                ["id"] = v => v.Id
            };
            //IQueryable queryable = query.AsQueryable();
            query = query.ApplyOrdering(queryObj, columnsMap);
            query = query.ApplyPaging(queryObj);
            return Ok(query);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            var modell = await context.Vehicles.Include(v=>v.Contact).Include(v=>v.Features).Include(v=>v.Model).FirstOrDefaultAsync(v=>v.Id==id);
            //var model = await repository.GetVehicle(id);
            return Ok(modell);
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicle(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = await context.Models.FindAsync(vehicle.ModelId);
            if (model == null)
            {
                ModelState.AddModelError("Model ID", "Invalid Model");
                return BadRequest(ModelState);
            }
            repository.Add(vehicle);
            //context.Vehicles.Add(vehicle);
            //await context.SaveChangesAsync();
            return Ok(vehicle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody]Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Vehicle model = await context.Vehicles.FindAsync(id);
            if (model == null)
            {
                ModelState.AddModelError("Model ID", "Invalid Model");
                return BadRequest(ModelState);
            }
            vehicle.LastUpdated = DateTime.Now;
            repository.UpdateEntity(vehicle);
            //context.Entry(vehicle).State = EntityState.Modified;
            //await context.SaveChangesAsync();
            //var returnVehicle = await context.Vehicles.FindAsync(id);
            return Ok(vehicle);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            //repository.Delete(vehicle);
            context.Vehicles.Remove(vehicle);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}