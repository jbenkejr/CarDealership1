using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealershipCapstone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipCapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly CarDealershipDbContext _context;

        public CarController(CarDealershipDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cars>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Cars>> GetCarsById(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            return car;
        }
        [HttpGet("color")]
        public async Task<ActionResult<List<Cars>>> GetCarsByColor(string color)
        {
            var car = await _context.Cars.Where(c => c.Color.Contains(color)).ToListAsync();
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpGet("model")]
        public async Task<ActionResult<List<Cars>>> GetCarsByModel(string model)
        {
            var car = await _context.Cars.Where(c => c.Model.Contains(model)).ToListAsync();
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpGet("make")]
        public async Task<ActionResult<List<Cars>>> GetCarsByMake(string make)
        {
            var car = await _context.Cars.Where(c => c.Make.Contains(make)).ToListAsync();
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpGet("year")]
        public async Task<ActionResult<List<Cars>>> GetCarsByYear(int year)
        {
            var car = await _context.Cars.Where(c => c.Year == year).ToListAsync();
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Cars>>> GetCarsByAnything(string? make, string? model, string? color, int? year)
        {
            var car = await _context.Cars.Where(c => c.Make.Contains(make) || c.Model.Contains(model) || c.Color.Contains(color) || c.Year == year).ToListAsync();
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpPost]
        public async Task<ActionResult<Cars>> PostCar(Cars car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCarsById), new { id = car.Id }, car);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutCar(int id, Cars car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
            // Response is 204 (no content) - requires the client to send an entirely updated entity and not just the changes
            //to support partial updates, we would use HTTP Patch
        }
    }
}