using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TourOfHeroesDTOs
{
    // ReSharper disable once InconsistentNaming
    public class UpdateHeroDTO
    {
        public string Name { get; set; }
    }
}
