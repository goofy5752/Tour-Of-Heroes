﻿// ReSharper disable VirtualMemberCallInConstructor
namespace TourOfHeroesData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Hero
    {
        public Hero()
        {
            this.EditHistory = new List<EditHistory>();
            this.Movies = new List<Movie>();
            this.Comments = new List<Comment>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(500)]
        public string Image { get; set; }

        [Required]
        [MaxLength(500)]
        public string CoverImage { get; set; }
        [Required]
        [MaxLength(40)]
        public string RealName { get; set; }

        [Required]
        [MaxLength(5000)]
        public DateTime Birthday { get; set; }

        [Required]
        [MaxLength(10)]
        public string Gender { get; set; }

        public virtual List<EditHistory> EditHistory { get; set; }
        
        public virtual List<Movie> Movies { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}