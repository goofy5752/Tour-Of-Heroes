using System;
using System.ComponentModel.DataAnnotations;

namespace TourOfHeroesData.Models
{
    public class EditHistory
    {
        public EditHistory()
        {
            this.EditedOn = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string OldValue { get; set; }

        [Required]
        [MaxLength(500)]
        public string NewValue { get; set; }

        [Required]
        public DateTime EditedOn { get; set; }

        public virtual Hero Hero { get; set; }
        public int HeroId { get; set; }
    }
}