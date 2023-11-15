using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewer.Models
{
    public class Actor
    {
        [DisplayName("Actor Id")]
        public short Id { get; set; }

        [Required(ErrorMessage = "You have to provide a First name. ")]
        [MaxLength(15, ErrorMessage = "First Name must be less than 15 characters. ")]
        [DisplayName("Actor First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You have to provide a Last name. ")]
        [MaxLength(15, ErrorMessage = "Last Name must be less than 15 characters. ")]
        [DisplayName("Actor Last Name")]
        public string LastName { get; set; }

        [Range(15, 150, ErrorMessage = "Write a valid age. ")]
        [DisplayName("Age")]
        public byte Age { get; set; }

        [ValidateNever]
        public string ImagePath { get; set; }

        [ValidateNever]
        public ICollection<Movie> MoviesIn { get; set; }

        [ValidateNever]
        public ICollection<User> UsersPreferred { get; set; }
    }
}
