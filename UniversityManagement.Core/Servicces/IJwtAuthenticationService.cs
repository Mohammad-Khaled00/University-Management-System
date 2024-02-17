namespace UniversityManagement.Core.Servicces
{
    public interface IJwtAuthenticationService
    {
        bool EmailExists(string email);
        string GenerateJwtToken(string email);
    }
}
