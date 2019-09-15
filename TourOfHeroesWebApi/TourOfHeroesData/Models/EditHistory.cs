using System;

namespace TourOfHeroesData.Models
{
    public class EditHistory
    {
        public EditHistory()
        {
            this.EditedOn = DateTime.Now;
        }

        public int Id { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public DateTime EditedOn { get; set; }

        public virtual Hero Hero { get; set; }
        public int HeroId { get; set; }
    }
}