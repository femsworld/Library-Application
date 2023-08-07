using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

        public string VerifyCredentials(AuthDto auth)
        {
            var foundUser = _userRepo.VerifyCredentials(auth.Email, auth.Password);
            if (foundUser != null)
            {
                var token = JwtBuilder.Create()
                    //   .WithAlgorithm(new RS256Algorithm(_rsaPrivateKey))
                      .WithAlgorithm(new HMACSHA256Algorithm())
                      .WithSecret("my-secrete-key")
                      .AddClaim(ClaimTypes.Email, foundUser.Email)
                      .AddClaim(ClaimTypes.NameIdentifier, foundUser.Id)
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