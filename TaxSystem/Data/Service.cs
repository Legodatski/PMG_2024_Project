﻿using System.ComponentModel.DataAnnotations;

namespace TaxSystem.Data
{
    public class Service
    {
        public Service()
        {
            Desks = new List<DesksServices>();
            IsDeleted = false;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Необходимо време за извършване в минути")]
        public string RequiredMinutes { get; set; }

        [Required]
        public ICollection<DesksServices> Desks { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

    }
}
