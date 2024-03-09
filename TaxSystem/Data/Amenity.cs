using System.ComponentModel.DataAnnotations;
using TaxSystem.Extensions;

namespace TaxSystem.Data
{
    public class Amenity
    {
        public Amenity()
        {
            Desks = new List<DeskAmenity>();
        }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        public int Id { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [Display(Name = "Необходимо време за извършване в минути")]
        public string RequiredMinutes { get; set; }

        [Required]
        public ICollection<DeskAmenity> Desks { get; set; }


    }
}
