using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoansService _loansService;

        public LoansController(Parameters)
        {
            _loansService = loansService;
        }

        // [Authorize]
        // [HttpPost]
        // public Loan PlaceLoan

    }
}