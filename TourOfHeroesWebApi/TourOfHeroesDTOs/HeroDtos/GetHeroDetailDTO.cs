using TourOfHeroesDTOs.EditHistoryDtos;

namespace TourOfHeroesDTOs.HeroDtos
{
    using System.Collections.Generic;

    using TourOfHeroesData.Models;
    using TourOfHeroesMapping.Mapping;

    using CommentDtos;
    using MovieDtos;

    public class GetHeroDetailDTO : IMapFrom<Hero>
    {
        public GetHeroDetailDTO()
        {
            this.EditHistory = new List<GetAllHistoryDTO>();
            this.Comments = new List<CommentDTO>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string CoverImage { get; set; }

        public string RealName { get; set; }

        public string Birthday { get; set; }

        public string Gender { get; set; }

        public string CurrentUser { get; set; }

        public IEnumerable<GetMovieTitleDTO> Movies { get; set; }

        public IEnumerable<GetAllHistoryDTO> EditHistory { get; set; }

        public IEnumerable<CommentDTO> Comments { get; set; }
    }
}