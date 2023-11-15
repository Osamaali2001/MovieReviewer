using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MovieReviewer.Models
{
    public class User
    {
        [DisplayName("User Id")]
        public short Id { get; set; }

        [Required(ErrorMessage = "You have to provide a First name. ")]
        [MaxLength(15, ErrorMessage = "First Name must be less than 15 characters. ")]
        [DisplayName("User First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You have to provide a Last name. ")]
        [MaxLength(15, ErrorMessage = "Last Name must be less than 15 characters. ")]
        [DisplayName("User Last Name")]
        public string LastName { get; set; }

        [ValidateNever]
        public string ImagePath { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "You have to provide a Password. ")]
        [MinLength(8, ErrorMessage = "Password must be more than 8 characters. ")]
        [DisplayName("Passwoord")]
        public string Password { get; set; }

        [ValidateNever]
        public bool IsAdmin { get; set; } = false;

        [ValidateNever]
        public ICollection<Movie> LikedMovies { get; set; }

        [ValidateNever]
        public ICollection<Movie> DislikedMovies { get; set; }

        [ValidateNever]
        public ICollection<Movie> PreferredMovies { get; set; }

        [ValidateNever]
        public ICollection<Actor> PreferredActors { get; set; }

        [ValidateNever]
        public ICollection<Director> PreferredDirectors { get; set; }
    }
}
