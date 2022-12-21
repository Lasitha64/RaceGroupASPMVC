using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace RunGroup.Models
{
    public class AppUser : IdentityUser
    {
        [Key]
        public string Id { get; set; }
        public int? Pace { get; set; }
        public int? Milage { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Races> Races { get; set; }
    }
}
