using System.Collections.Generic;

namespace TourOfHeroesData.Models
{
    public class Hero
    {
        public Hero()
        {
            this.EditHistories = new List<EditHistory>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string CoverImage { get; set; }

        public string RealName { get; set; }

        public string Birthday { get; set; }

        public string Gender { get; set; }

        public List<EditHistory> EditHistories { get; set; }
    }
}
