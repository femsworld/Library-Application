using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Business.Dto
{
    public class LoanBookDto
    {
         public Guid BookId { get; set; }
        public int Amount   { get; set; }
    }
}