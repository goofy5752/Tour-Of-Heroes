namespace TourOfHeroesData.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            this.Comments = new List<Comment>();
        }

        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}