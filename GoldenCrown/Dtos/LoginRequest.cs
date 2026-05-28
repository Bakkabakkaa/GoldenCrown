using System.ComponentModel.DataAnnotations;

namespace GoldenCrown.Dtos;

public class LoginRequest
{
    [Required(ErrorMessage = "The login field is required")]
    [MinLength(3, ErrorMessage = "The minimum login length is 3 characters")]
    public string Login { get; set; }
    
    [Required(ErrorMessage = "The password field is required")]
    [MinLength(6, ErrorMessage = "Minimum password length is 6 characters")]
    public string Passwoed { get; set; }
}