namespace TourOfHeroesData.Models
{
    using System.ComponentModel.DataAnnotations;
    using System;

    using Common.Contracts;

    public class Comment : IDeletableEntity
    {
        public Comment()
        {
            this.PublishedOn = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Text { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(1000)]
        public string ProfileImage { get; set; }

        public DateTime PublishedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int? HeroId { get; set; }
        public virtual Hero Hero { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int? BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }
}