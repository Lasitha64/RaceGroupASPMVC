using RunGroup.Data.Enum;
using RunGroup.Models;

namespace RunGroup.ViewModels
{
    public class CreateRacesViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public RaceCategory RaceCategory { get; set; }
        public string AppUserId { get; set; }

    }
}
