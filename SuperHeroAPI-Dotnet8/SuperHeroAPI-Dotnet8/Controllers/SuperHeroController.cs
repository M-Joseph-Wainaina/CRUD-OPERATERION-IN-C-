using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_Dotnet8.Data;
using SuperHeroAPI_Dotnet8.Entities;

namespace SuperHeroAPI_Dotnet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeros()
        {
            var heroes = await _context.SuperHeros.ToListAsync();

            return Ok(heroes);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<SuperHero>> GetHero(int Id)
        {
            var hero = await _context.SuperHeros.FindAsync(Id);

            if (hero == null)
            {
                return NotFound();
            }

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeros.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeros.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero updatedHero)
        {
           var dbHero = await _context.SuperHeros.FindAsync(updatedHero.Id);
            if (dbHero is null)
                return NotFound("Hero not found");

            dbHero.Name = updatedHero.Name;
            dbHero.FirstName = updatedHero.FirstName;
            dbHero.LastName = updatedHero.LastName;
            dbHero.Place = updatedHero.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeros.ToListAsync());

        }
        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int Id)
        {
            var dbHero = await _context.SuperHeros.FindAsync(Id);
            if (dbHero is null)
                return NotFound("Hero not found");
            _context.SuperHeros.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeros.ToListAsync());

        }
    }
}
