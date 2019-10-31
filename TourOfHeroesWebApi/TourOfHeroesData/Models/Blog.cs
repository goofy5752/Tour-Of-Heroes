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
            // ReSharper disable once VirtualMemberCallInConstructor
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

        [Required]
        [MaxLength(30)]
        public string AuthorUserName { get; set; }

        public DateTime PublishedOn { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}