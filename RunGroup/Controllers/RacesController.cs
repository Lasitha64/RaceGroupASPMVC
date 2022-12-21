using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroup.Data;
using RunGroup.Interfaces;
using RunGroup.Models;
using RunGroup.Repository;
using RunGroup.Services;
using RunGroup.ViewModels;

namespace RunGroup.Controllers
{
    public class RacesController : Controller
    {
        private readonly IRaceRepository _racerepository;
        private readonly IPhotoService _photoService;
        public RacesController(IRaceRepository racerepository, IPhotoService photoService)
        {
            _racerepository= racerepository;
            _photoService= photoService;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRacesViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceVM.Image);
                var race = new Races
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = raceVM.Address.Street,
                        City = raceVM.Address.City,
                        State = raceVM.Address.State,
                    }
                };
                _racerepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(raceVM);
        }
    }
}
