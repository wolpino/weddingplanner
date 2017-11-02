using System.ComponentModel.DataAnnotations;
namespace weddingplanner.Models

{
    public class LoginViewModel : BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }
 
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string PWD { get; set; }

    }
}
    