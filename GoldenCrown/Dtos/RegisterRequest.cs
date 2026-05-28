using System.ComponentModel.DataAnnotations;

namespace GoldenCrown.Dtos;

public class RegisterRequest
{
    [Required(ErrorMessage = "The login field is required")]
    [MinLength(3, ErrorMessage = "The minimum login length is 3 characters")]
    public string Login { get; set; }
    
    [Required(ErrorMessage = "The name field is required")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "The password field is required")]
    [MinLength(6, ErrorMessage = "Minimum password length is 6 characters")]
    public string Password { get; set; }
}