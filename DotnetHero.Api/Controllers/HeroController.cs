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
        public async Task<IActionResult> Get()
        {
            return Ok(await _heroService.Heroes());
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
