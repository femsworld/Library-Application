using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Business.Dto;

namespace WebApi.Business.Services.Abstractions
{
    public interface IAuthService
    {
        string VerifyCredentials(AuthDto auth);
    }
}