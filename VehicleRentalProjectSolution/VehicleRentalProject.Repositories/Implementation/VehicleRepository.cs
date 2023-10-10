using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRentalProject.Models;
using VehicleRentalProject.Repositories.Infrastructure;

namespace VehicleRentalProject.Repositories.Implementation
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly CarContext _context;
        public VehicleRepository(CarContext context)
        {
            _context = context;
        }
        public async Task DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                _context.SaveChanges();
            }
        }
        public async Task<Vehicle> GetVehicleById(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                return vehicle;
            }
            throw new Exception($"Vehicle with ID{id} not found");
        }

        public async Task<IEnumerable<Vehicle>> GetVehicles()
        {
            var vehicles = await _context.Vehicles.ToListAsync();
            if (vehicles.Count == 0)
            {
                throw new Exception($"Vehicle Table is Empty");
            }
            return vehicles;
        }

        public async Task InsertVehicle(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVehicle(Vehicle vehicle)
        {
            var vehiclesFromDb = await _context.Vehicles.FindAsync(vehicle.Id);
            if (vehiclesFromDb != null)
            {
                vehiclesFromDb.VehicleName = vehicle.VehicleName;
                vehiclesFromDb.VehicleType = vehicle.VehicleType;
                vehiclesFromDb.VehicleModel = vehicle.VehicleModel;
                vehiclesFromDb.VehicleNumber = vehicle.VehicleNumber;
                vehiclesFromDb.VehicleColor = vehicle.VehicleColor;
                vehiclesFromDb.DailyRate = vehicle.DailyRate;
                vehiclesFromDb.VehicleDescription = vehicle.VehicleDescription;
                if (vehicle.VehicleImage != null)
                {
                    vehiclesFromDb.VehicleImage = vehicle.VehicleImage;
                }
                vehiclesFromDb.VehiclePrice = vehicle.VehiclePrice;
                vehiclesFromDb.UpdateAt = DateTime.UtcNow;
                _context.SaveChanges();
            }
        }
    }
}
