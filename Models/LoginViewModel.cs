using System.ComponentModel.DataAnnotations;

namespace CBApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(70)]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [StringLength(255)]
        public string? Password { get; set; }
        public string? ReturnUrl { get; set; }

        public bool RememberMe { get; set; }

    }
}
