using RunGroup.Models;

namespace RunGroup.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Races>> GetAllUserRaces();
        Task<List<Club>> GetAllUserClubs();

    }
}
