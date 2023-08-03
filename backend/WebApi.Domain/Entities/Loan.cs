using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Domain.Entities
{
    public class Loan : BaseEnity
    {
        public Guid UserId { get; set; }
        public List<LoanBook>? LoanBooks { get; set; } 
    }
}