using RunGroup.Models;

namespace RunGroup.Interfaces
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Races>> GetAll();
        Task<Races> GetByIdAsync(int id);
        Task<Races> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Races>> GetAllRacesByCity(string city);
        bool Add(Races race);
        bool Update(Races race);
        bool Delete(Races race);
        bool Save();
    }
}
