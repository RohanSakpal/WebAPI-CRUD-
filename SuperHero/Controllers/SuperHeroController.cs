using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHero.Data;

namespace SuperHero.Controllers
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

        //Dynamic
        [HttpGet("{page}")]
        public async Task<ActionResult<List<SuperHero>>> GetPage(int page)
        {
            var pageResults = 3f;
            var pageCount = Math.Ceiling(_context.SuperHeroes.Count() / pageResults);

            var hero = await _context.SuperHeroes
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            var response = new HeroResponse
            {
                SuperHeroList = hero,
                CurrentPages = page,
                Pages = (int)pageCount
            };
            return Ok(response);
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<List<SuperHero>>> Get(int id)
        //{
        //    var hero = await _context.SuperHeroes.FindAsync(id);
        //    if (hero == null)
        //        return BadRequest("Hero Not Found");
        //    return Ok(hero);
        //}

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            hero.Id = 0;
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero req)
        {
            var hero = await _context.SuperHeroes.FindAsync(req.Id);
            if (hero == null)
                return BadRequest("Hero Not Found");

            hero.Name = req.Name;
            hero.FirstName = req.FirstName;
            hero.LastName = req.LastName;
            hero.Place = req.Place;

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero Not Found");

            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }


        //Static
        //private static List<SuperHero> heroes = new List<SuperHero> {
        //    new SuperHero {
        //        Id = 1,
        //        Name = "Spider Map",
        //        FirstName = "Peter",
        //        LastName = "Parker",
        //        Place = "US"
        //    }
        //};

        //[HttpGet]
        //public async Task<ActionResult<List<SuperHero>>> Get()
        //{
        //    return Ok(heroes);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<List<SuperHero>>> Get(int id)
        //{
        //    var hero = heroes.Find(h => h.Id == id);
        //    if (hero == null)
        //        return BadRequest("Hero Not Found");
        //    return Ok(hero);
        //}

        //[HttpPost]
        //public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        //{
        //    heroes.Add(hero);
        //    return Ok(heroes);
        //}

        //[HttpPut]
        //public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero req)
        //{
        //    var hero = heroes.Find(h => h.Id == req.Id);
        //    if (hero == null)
        //        return BadRequest("Hero Not Found");

        //    hero.Name = req.Name;
        //    hero.FirstName = req.FirstName;
        //    hero.LastName = req.LastName;
        //    hero.Place = req.Place;

        //    return Ok(heroes);
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        //{
        //    var hero = heroes.Find(h => h.Id == id);
        //    if (hero == null)
        //        return BadRequest("Hero Not Found");

        //    heroes.Remove(hero);
        //    return Ok(hero);
        //}
    }
}
