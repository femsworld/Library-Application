using System.Security.Claims;
using JWT.Algorithms;
using JWT.Builder;
using WebApi.Business.Dto;
using WebApi.Business.RepoAbstractions;
using WebApi.Business.Services.Abstractions;

namespace WebApi.Business.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepo _userRepo;

        public AuthService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<string> VerifyCredentialsAsync(AuthDto auth)
        {
            var foundUser = await _userRepo.VerifyCredentialsAsync(auth.Email, auth.Password);
            if (foundUser != null)
            {
                var token = JwtBuilder.Create()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret("my-secrete-key")
                    .AddClaim(ClaimTypes.Email, foundUser.Email)
                    .AddClaim(ClaimTypes.NameIdentifier, foundUser.Id.ToString())
                    .AddClaim(ClaimTypes.Role, foundUser.Role.ToString())
                    .MustVerifySignature()
                    .Encode();
                Console.WriteLine(token);
                return token;
            }
            else
            {
                throw new Exception("Credentials are incorrect");
            }
        }
    }
}