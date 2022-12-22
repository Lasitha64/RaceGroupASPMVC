using Microsoft.EntityFrameworkCore;
using RunGroup.Data;
using RunGroup.Interfaces;
using RunGroup.Models;

namespace RunGroup.Repository
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext _context;

        public RaceRepository(ApplicationDbContext context) {
            _context = context;
        }
        public bool Add(Races race)
        {
            _context.Add(race);
            return Save();
        }

        public bool Delete(Races race)
        {
            _context.Remove(race);
            return Save();
        }

        public async Task<IEnumerable<Races>> GetAll()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<IEnumerable<Races>> GetAllRacesByCity(string city)
        {
            return await _context.Races.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<Races> GetByIdAsync(int id)
        {
            return await _context.Races.Include(i=> i.Address).FirstOrDefaultAsync(I => I.Id == id);
        }

        public async Task<Races> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Races.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(I => I.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Races race)
        {
            _context.Update(race);
            return Save();
        }
    }

    
}
