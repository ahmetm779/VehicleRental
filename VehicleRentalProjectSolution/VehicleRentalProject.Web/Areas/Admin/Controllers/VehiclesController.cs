using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleRentalProject.Models;
using VehicleRentalProject.Repositories.Infrastructure;
using VehicleRentalProject.Web.Models.ViewModels.Vehicle;

namespace VehicleRentalProject.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class VehiclesController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private IMapper _mapper;
        public VehiclesController(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, string SearchingText = null)
        {
            IEnumerable<VehicleViewModel> viewModelList;
            var vehicles = _vehicleRepository.GetVehicles().GetAwaiter().GetResult().Skip(pageNumber * pageSize - pageSize).Take(pageSize);

            viewModelList = _mapper.Map<IEnumerable<VehicleViewModel>>(vehicles);
            if (!string.IsNullOrEmpty(SearchingText))
            {
                viewModelList = viewModelList.Where(x => x.VehicleNumber.Equals(SearchingText));
            }

            var vehicleViewModel = new ListVehicleViewModel
            {
                VehicleList = viewModelList,
                PageInfo = new Utility.PageInfo
                {
                    ItemsPerPage = pageSize,
                    CurrentPage = pageNumber,
                    TotalItems = _vehicleRepository.GetVehicles().GetAwaiter().GetResult().Count()
                },
            };

            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateVehicleViewModel vm)
        {
            var model = _mapper.Map<Vehicle>(vm);
            await _vehicleRepository.InsertVehicle(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vehicle = await _vehicleRepository.GetVehicleById(id);
            var vehicleViewModel = _mapper.Map<EditVehicleViewModel>(vehicle);
            return View(vehicleViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditVehicleViewModel vm)
        {
            var vehicle = _mapper.Map<Vehicle>(vm);
            await _vehicleRepository.UpdateVehicle(vehicle);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = _vehicleRepository.GetVehicleById(id);
            await _vehicleRepository.DeleteVehicle(vehicle.Id);
            return RedirectToAction("Index");
        }
    }
}
