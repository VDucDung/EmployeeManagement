using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto model)
        {

        Console.WriteLine(model.Email);
        Console.WriteLine(model.Password);
        var employee = await _context.Employees
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Email == model.Email && e.IsActive);

        Console.WriteLine(employee?.LastName);

            if (employee == null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Email không tồn tại hoặc tài khoản đã bị vô hiệu hóa."
                };
            }

        if (!BCrypt.Net.BCrypt.Verify(model.Password, employee.Password))
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Sai mật khẩu."
                };
            }

            var token = GenerateJwtToken(employee);

            return new AuthResponseDto
            {
                Success = true,
                Token = token,
                EmployeeId = employee.EmployeeId,
                FullName = $"{employee.FirstName} {employee.LastName}",
                RoleId = employee.RoleId,
                Role = employee.Role.Name
            };
        }

        public string GenerateJwtToken(Employee employee)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, employee.EmployeeId.ToString()),
            new Claim(ClaimTypes.Email, employee.Email),
            new Claim(ClaimTypes.Name, $"{employee.FirstName} {employee.LastName}"),
            new Claim(ClaimTypes.Role, employee.Role.Name)
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

