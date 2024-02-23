using System.ComponentModel.DataAnnotations;

namespace UniversityManagement.Core.ViewModels
{
    public class AuthVM
    {
        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(128)]
        public string Email { get; set; }

        [Required, StringLength(256)]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
