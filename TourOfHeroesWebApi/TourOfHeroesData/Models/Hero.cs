using System.Collections.Generic;

namespace TourOfHeroesData.Models
{
    public class Hero
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string CoverImage { get; set; }

        public virtual IEnumerable<EditHistory> EditHistories => new List<EditHistory>();
    }
}
