using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Domain.Entities
{
    [PrimaryKey(nameof(BookId), nameof(LoanId))]
    public class LoanBook
    {
        [ForeignKey(nameof(Book))]
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        [ForeignKey(nameof(Loan))]
        public Guid LoanId { get; set; }
        // public Loan Loan { get; set; }
        public int Amount   { get; set; }
    }
}