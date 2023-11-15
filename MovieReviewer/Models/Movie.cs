using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;

namespace MovieReviewer.Models
{
    public class Movie
    {
        [DisplayName("Movie Id")]
        public short MovieId { get; set; }

        [DisplayName("Movie")]
        public string MovieName { get; set; }

        [ValidateNever]
        public string ImagePath { get; set; }

        [DisplayName("Director")]
        public short DirectorId { get; set; }

        [ValidateNever]
        public Director Director { get; set; }

        [ValidateNever]
        public ICollection<User> LikedByUsers { get; set; }

        [ValidateNever]
        public ICollection<User> DislikedByUsers { get; set; }

        [ValidateNever]
        public ICollection<User> UsersPreferred { get; set; }

        [ValidateNever]
        public ICollection<Actor> ActtorsIn { get; set; }
    }
}
