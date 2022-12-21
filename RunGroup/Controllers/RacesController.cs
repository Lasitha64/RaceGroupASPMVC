using Microsoft.AspNetCore.Mvc;
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
    }
}
