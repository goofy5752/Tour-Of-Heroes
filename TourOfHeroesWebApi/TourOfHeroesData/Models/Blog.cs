namespace TourOfHeroesData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Blog
    {
        public Blog()
        {
            this.PublishedOn = DateTime.Now;
            this.Comments = new List<Comment>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        [MaxLength(10000)]
        public string Content { get; set; }

        [Required]
        [MaxLength(400)]
        public string BlogImage { get; set; }

        public DateTime PublishedOn { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}