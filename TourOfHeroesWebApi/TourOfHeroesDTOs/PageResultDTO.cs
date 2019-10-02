using System.Collections.Generic;

namespace TourOfHeroesDTOs
{
    // ReSharper disable once InconsistentNaming
    public class PageResultDTO<T>
    {
        public int Count { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public List<T> Items { get; set; }
    }
}
