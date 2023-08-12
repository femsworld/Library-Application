namespace WebApi.Domain.Entities
{
    public class Loan : BaseEnity
    {
        public Guid UserId { get; set; }
        public List<LoanBook>? LoanBooks { get; set; }
    }
}