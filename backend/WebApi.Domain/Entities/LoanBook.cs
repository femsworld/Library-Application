using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi.Domain.Entities
{
    public class LoanBook
    {
        public Guid BookId { get; set; }
        public string BookTitle { get; set; }

        [JsonIgnore]
        [ForeignKey("BookId")]
        public Book Book { get; set; }

        public Guid LoanId { get; set; }

        [JsonIgnore]
        [ForeignKey("LoanId")]
        public Loan Loan { get; set; }

        public int Amount { get; set; }
    }
}
