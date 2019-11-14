// ReSharper disable VirtualMemberCallInConstructor
namespace TourOfHeroesData.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Comments = new List<Comment>();
            this.Blogs = new List<Blog>();
            this.LikedMovies = new List<Movie>();
        }

        public override string Id { get; set; }

        public override string UserName { get; set; }

        public override string Email { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }

        public string ProfileImage { get; set; }

        public DateTime? RegisteredOn { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual List<Blog> Blogs { get; set; }

        public virtual List<Movie> LikedMovies { get; set; }
    }
}