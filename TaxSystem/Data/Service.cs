using System.ComponentModel.DataAnnotations;
using TaxSystem.Extensions;

namespace TaxSystem.Data
{
    public class Service
    {
        public Service()
        {
            Desks = new List<DesksServices>();
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
        public ICollection<DesksServices> Desks { get; set; }


    }
}
