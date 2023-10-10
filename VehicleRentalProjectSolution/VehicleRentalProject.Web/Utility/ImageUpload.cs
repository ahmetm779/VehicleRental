namespace VehicleRentalProject.Web.Utility
{
    public class ImageUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageUpload(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string SaveImageFile(IFormFile vehicleImageUrl)
        {
            if (vehicleImageUrl != null || vehicleImageUrl.Length > 0)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string uploadPath = Path.Combine(webRootPath, "upload");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(vehicleImageUrl.FileName);
                string filePath = Path.Combine(uploadPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vehicleImageUrl.CopyTo(fileStream);
                }
                return Path.Combine("upload", fileName);
            }
            return null;
        }
    }
}
