namespace TourOfHeroesDTOs.HeroDtos
{
    using System.Collections.Generic;

    using TourOfHeroesData.Models;
    using TourOfHeroesMapping.Mapping;

    using MovieDtos;

    public class GetHeroDetailDTO : IMapFrom<Hero>
    {
        public GetHeroDetailDTO()
        {
            this.EditHistory = new List<EditHistory>();
            this.Comments = new List<Comment>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public string Image { get; set; }
        
        public string CoverImage { get; set; }
        
        public string RealName { get; set; }
        
        public string Birthday { get; set; }
        
        public string Gender { get; set; }

        public IEnumerable<GetMovieTitleDTO> Movies { get; set; }

        public IEnumerable<EditHistory> EditHistory { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}