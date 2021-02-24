using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using HeroApi.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HeroApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeroController : ControllerBase
    {

        private readonly ILogger<HeroController> _logger;
        private readonly IHeroService _heroService;

        public HeroController(ILogger<HeroController> logger, IHeroService heroService)
        {
            _logger = logger;
            _heroService = heroService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHeroes([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Ok(await _heroService.GetHeroes());
            }
            else
            {
                return Ok(await _heroService.SearchHeroes(name));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHero([FromRoute] int id)
        {
            return Ok(await _heroService.GetHero(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HeroDocument hero)
        {
            return Ok(await _heroService.InsertHero(hero));
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] HeroDocument hero)
        {
            return Ok(await _heroService.DeleteHero(hero));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] HeroDocument hero)
        {
            return Ok(await _heroService.UpdateHero(hero));
        }
    }
}
