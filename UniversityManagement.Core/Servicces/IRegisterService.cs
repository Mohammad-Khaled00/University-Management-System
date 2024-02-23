using Microsoft.AspNetCore.Identity;
using UniversityManagement.Core.ViewModels;

namespace UniversityManagement.Core.Servicces
{
    public interface IRegisterService
    {
        Task<ResponseEntity<UserVM>> RegisterAsync(AuthVM model, string Role);
    }
}
