namespace VehicleRentalProject.Web.Models.ViewModels.Vehicle
{
    public class EditVehicleViewModel
    {
        public int Id { get; set; }
        public string VehicleName { get; set; }
        public string VehicleType { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleColor { get; set; }
        public string VehicleDescription { get; set; }
        public decimal DailyRate { get; set; }
        public IFormFile VehicleImageUrl { get; set; }
        public decimal VehiclePrice { get; set; }
        public bool IsAvailable { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateAt { get; set; } = DateTime.UtcNow;
    }
}
