﻿using System.ComponentModel.DataAnnotations;

namespace TaxSystem.Data
{
    public class Service
    {
        public Service()
        {
            Desks = new List<DeskService>();
            IsDeleted = false;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double RequiredHours { get; set; }

        [Required]
        public ICollection<DeskService> Desks { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

    }
}
