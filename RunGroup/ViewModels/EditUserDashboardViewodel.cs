namespace RunGroup.ViewModels
{
    public class EditUserDashboardViewodel
    {
        public string Id { get; set; }
        public int? Pace { get; set; }
        public int? Milage { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set;}
        public string? State { get; set;}
        public IFormFile Image { get; set;}
    }
}
