using AutoMapper;
using CourseLesson.Contract;
using CourseLesson.Data;
using CourseLesson.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CourseLesson.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        private ApiUser _user;

        private readonly string _refreshToken = "RefreshToken";
        private readonly string _provider = "CourseLessonApi";




        public async Task<string> CreateRefreshToken()
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user,_provider,_refreshToken);
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _provider, _refreshToken);
            await _userManager.SetAuthenticationTokenAsync(_user, _provider, _refreshToken, newRefreshToken);

            return newRefreshToken;
        }

        public async Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;
            _user = await _userManager.FindByNameAsync(username);

            if (_user == null || _user.Id != request.UserId)
            {
                return null;
            }

            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user, _provider, _refreshToken, request.RefreshToken);

            if (isValidRefreshToken)
            {
                var token = await GenerateToken();
                return new AuthResponseDto
                {
                    Token = token,
                    UserId = _user.Id,
                    RefreshToken = await CreateRefreshToken()
                };
            }

            await _userManager.UpdateSecurityStampAsync(_user);
            return null;
        }
        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> LogIn(LogInMod logInMod)
        {
            _user = await _userManager.FindByEmailAsync(logInMod.Email);
            var isValidCredentials = await _userManager.CheckPasswordAsync(_user, logInMod.Password);

            if (_user == null || isValidCredentials == false) return null;

            string genToken = await GenerateToken();

            var token = new AuthResponseDto()
            {
                Token = genToken,
                UserId = _user.Id
            };

            return token;
        }
        public async Task<IEnumerable<IdentityError>> Register(CreateUserMod createUserMod)
        {
            var user = _mapper.Map<ApiUser>(createUserMod);
            user.UserName = createUserMod.Email;

            var result = await _userManager.CreateAsync(user, createUserMod.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result.Errors.ToList();
        }

        private async Task<string> GenerateToken()
        {
            // Шаг 1: Получение секретного ключа из конфигурации
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

            // Шаг 2: Создание учетных данных для подписи токена
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Шаг 3: Получение ролей пользователя
            var roles = await _userManager.GetRolesAsync(_user);

            // Шаг 4: Создание утверждений (claims) для пользователя, включая роли
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(_user);

            // Шаг 5: Создание основных утверждений для токена
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim("uid", _user.Id),
            }
            .Union(userClaims)
            .Union(roleClaims);

            // Шаг 6: Создание JWT токена
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
            );

            // Шаг 7: Преобразование токена в строку
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
