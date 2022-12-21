using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroup.Data;
using RunGroup.Interfaces;
using RunGroup.Models;

namespace RunGroup.Controllers
{
    public class RacesController : Controller
    {
        private readonly IRaceRepository _racerepository;
        public RacesController(IRaceRepository racerepository)
        {
            _racerepository= racerepository;
        }
        public async  Task<IActionResult> Index()
        {
            IEnumerable<Races> races = await _racerepository.GetAll();
            return View(races);
        }

        public async  Task<IActionResult> Detail(int id)
        {
            Races race = await _racerepository.GetByIdAsync(id);
            return View(race);
        }
    }
}
