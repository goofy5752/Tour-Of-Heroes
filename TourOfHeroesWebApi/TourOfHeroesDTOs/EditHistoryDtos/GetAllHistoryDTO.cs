namespace TourOfHeroesDTOs.EditHistoryDtos
{
    using System;

    using TourOfHeroesData.Models;
    using TourOfHeroesMapping.Mapping;

    public class GetAllHistoryDTO : IMapFrom<EditHistory>
    {
        public int Id { get; set; }

        public string NewValue { get; set; }

        public string OldValue { get; set; }

        public DateTime EditedOn { get; set; }
    }
}