using System.ComponentModel.DataAnnotations;
using TaxSystem.Extensions;

namespace TaxSystem.Models.Amenity
{
    public class AmenityViewModel
    {
        public AmenityViewModel()
        {
            DeskIds = new List<string>();
            WorkerFirstNames = new List<string>();
            WorkerLastNames = new List<string>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [Display(Name = "Название на услугата")]
        public string Name { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [Display(Name = "Oписание на услугата")]
        public string Description { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [Display(Name = "Необходимо време за извършване в минути")]
        public string RequiredMinutes { get; set; }

        public List<string> DeskIds { get; set; }

        public List<string> WorkerFirstNames { get; set; }
        public List<string> WorkerLastNames { get; set; }
    }
}
