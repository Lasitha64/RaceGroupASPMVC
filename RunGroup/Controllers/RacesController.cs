using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroup.Data;
using RunGroup.Models;

namespace RunGroup.Controllers
{
    public class RacesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RacesController(ApplicationDbContext context)
        {
            _context= context;
        }
        public IActionResult Index()
        {
            List<Races> races = _context.Races.ToList();
            return View(races);
        }

        public IActionResult Detail(int id)
        {
            Races race = _context.Races.Include(a => a.Address).FirstOrDefault(c => c.Id == id);
            return View(race);
        }
    }
}
