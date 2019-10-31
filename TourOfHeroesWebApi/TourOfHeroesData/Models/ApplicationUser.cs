// ReSharper disable VirtualMemberCallInConstructor
namespace TourOfHeroesData.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Comments = new List<Comment>();
            this.BlogPosts = new List<Blog>();
        }

        public override string Id { get; set; }

        public override string UserName { get; set; }

        public override string Email { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }

        public string ProfileImage { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual List<Blog> BlogPosts { get; set; }
    }
}