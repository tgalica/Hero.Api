using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace HeroApi.Implementation
{
    public interface IHeroService
    {
        Task<IEnumerable<HeroDocument>> GetHeroes();
        Task<IEnumerable<HeroDocument>> SearchHeroes(string term);
        Task<HeroDocument> GetHero(int id);
        Task<HeroDocument> InsertHero(HeroDocument hero);
        Task<HeroDocument> DeleteHero(HeroDocument hero);
        Task<HeroDocument> UpdateHero(HeroDocument hero);

    }
}