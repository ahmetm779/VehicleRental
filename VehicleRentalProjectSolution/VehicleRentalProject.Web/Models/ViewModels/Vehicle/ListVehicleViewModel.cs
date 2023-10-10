using VehicleRentalProject.Web.Utility;

namespace VehicleRentalProject.Web.Models.ViewModels.Vehicle
{
    public class ListVehicleViewModel
    {
        public IEnumerable<VehicleViewModel> VehicleList { get; set; }
        public PageInfo PageInfo { get; set; }
        public string SearchingText { get; set; }
    }
}
