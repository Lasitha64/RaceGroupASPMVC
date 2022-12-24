using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroup.Data;
using RunGroup.Interfaces;
using RunGroup.Models;
using RunGroup.Repository;
using RunGroup.ViewModels;

namespace RunGroup.Controllers
{
    public class RacesController : Controller
    {
        private readonly IRaceRepository _racerepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RacesController(IRaceRepository racerepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _racerepository = racerepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
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
            var curUser = _httpContextAccessor.HttpContext.User.GetUserId();
            var createRacesViewModel = new CreateRacesViewModel { AppUserId = curUser };
            return View(createRacesViewModel);
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
                    AppUserId = raceVM.AppUserId,
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

        public async Task<IActionResult> Edit(int id)
        {
            var race = await _racerepository.GetByIdAsync(id);
            if (race == null) return View("Error");
            var raceVM = new EditRaceViewModel
            {
                Title = race.Title,
                Description = race.Description,
                AddressId = race.AddressId,
                Address = race.Address,
                URL = race.Image,
                RaceCategory = race.RaceCategory,
            };

            return View(raceVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceViewModel raceVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", raceVM);
            }
            var raceClub = await _racerepository.GetByIdAsyncNoTracking(id);

            if (raceClub != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(raceClub.Image);
                }
                catch (Exception)
                {

                    ModelState.AddModelError("", "Could not delete photo");
                    return View(raceVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(raceVM.Image);

                var race = new Races
                {
                    Id = id,
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = raceVM.AddressId,
                    Address = raceVM.Address,
                };
                _racerepository.Update(race);
                return RedirectToAction("Index");
            }
            else
            {
                return View(raceVM);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var raceDetails = await _racerepository.GetByIdAsync(id);
            if (raceDetails == null) return View("Error");
            return View(raceDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var raceDetails = await _racerepository.GetByIdAsync(id);
            if (raceDetails == null) return View("Error");

            _racerepository.Delete(raceDetails);
            return RedirectToAction("Index");
        }
    }
}
