using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VehicleRentalProject.Models;
using VehicleRentalProject.Repositories.Infrastructure;
using VehicleRentalProject.Web.Models.ViewModels.Vehicle;
using VehicleRentalProject.Web.Utility;

namespace VehicleRentalProject.Web.Mapper
{
    public class VehicleProfile : Profile
    {
        private IWebHostEnvironment _WebHostEnvironment;
        public VehicleProfile(IWebHostEnvironment webHostEnvironment)
        {
            _WebHostEnvironment = webHostEnvironment;
            CreateMap<Vehicle, VehicleViewModel>();

            CreateMap<CreateVehicleViewModel, Vehicle>()
                .ForMember(dest => dest.VehicleImage, opt => opt.MapFrom(src => new ImageUpload(_WebHostEnvironment).SaveImageFile(src.VehicleImageUrl)));

            CreateMap<Vehicle, EditVehicleViewModel>()
                .ForMember(dest => dest.VehicleImageUrl, opt => opt.Ignore());

            CreateMap<Vehicle, VehicleDetailsViewModel>()
                .ForMember(dest => dest.StartDate, opt => opt.Ignore())
                .ForMember(dest => dest.EndDate, opt => opt.Ignore());


            CreateMap<EditVehicleViewModel, Vehicle>()
                .ForMember(dest => dest.VehicleImage, opt => opt.MapFrom(src => new ImageUpload(_WebHostEnvironment).SaveImageFile(src.VehicleImageUrl)));
        }
    }
}