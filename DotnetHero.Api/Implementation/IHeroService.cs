using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace HeroApi.Implementation
{
    public interface IHeroService
    {
        Task<IEnumerable<HeroDocument>> Heroes();
        Task<HeroDocument> InsertHero(HeroDocument hero);
        Task<HeroDocument> DeleteHero(HeroDocument hero);
        Task<HeroDocument> UpdateHero(HeroDocument hero);

    }
}