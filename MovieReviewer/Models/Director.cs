using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MovieReviewer.Models
{
    public class Director
    {
        [DisplayName("Directo Id")]
        public short Id { get; set; }

        [Required(ErrorMessage = "You have to provide a First name. ")]
        [MaxLength(15, ErrorMessage = "First Name must be less than 15 characters. ")]
        [DisplayName("Director First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You have to provide a Last name. ")]
        [MaxLength(15, ErrorMessage = "Last Name must be less than 15 characters. ")]
        [DisplayName("Director Last Name")]
        public string LastName { get; set; }

        [Range(15, 150, ErrorMessage = "Write a valid age. ")]
        [DisplayName("Age")]
        public byte Age { get; set; }

        [ValidateNever]
        public string ImagePath { get; set; }
        
        [ValidateNever]
        public ICollection<Movie> MoviesDirected { get; set; }

        [ValidateNever]
        public ICollection<User> UsersPreferred { get; set; }

    }
}
