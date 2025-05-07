public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginDto model);
    string GenerateJwtToken(Employee employee);
}