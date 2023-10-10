using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VehicleRentalProject.Repositories.Infrastructure;
using VehicleRentalProject.Web.Models;
using VehicleRentalProject.Web.Models.ViewModels.Vehicle;

namespace VehicleRentalProject.Web.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private IVehicleRepository _vehicleRepository;
        private IMapper _mapper;

        public HomeController(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var vehicles = _vehicleRepository.GetVehicles().GetAwaiter().GetResult().ToList().Where(x => !x.IsDeleted && x.IsAvailable);
            var vm = _mapper.Map<List<VehicleViewModel>>(vehicles);
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var vehicle = await _vehicleRepository.GetVehicleById(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            var vm = _mapper.Map<VehicleDetailsViewModel>(vehicle);
            return View(vm);
        }
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Summary(VehicleDetailsViewModel vm)
        {
            if (ModelState.IsValid)
            {
                TimeSpan duration = (TimeSpan)(vm.EndDate - vm.StartDate);
                vm.TotalAmount = vm.DailyRate * duration.Days;
            }
            return View(vm);
        }




        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}