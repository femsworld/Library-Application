namespace WebApi.Business.Dto
{
    public class LoanDto
    {
         public Guid UserId { get; set; }
         public List<LoanBookDto> LoanBooks { get; set; }
    }
}