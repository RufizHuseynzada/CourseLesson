using CourseLesson.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace CourseLesson.Contract
{
    public interface IAuthManager
    {
        public Task<IEnumerable<IdentityError>> Register(CreateUserMod createUserMod);

        public Task<AuthResponseDto> LogIn(LogInMod logInMod);

        public Task<string> CreateRefreshToken();

        Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
    }
}
