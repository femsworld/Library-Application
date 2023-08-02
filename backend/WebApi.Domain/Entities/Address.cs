namespace WebApi.Domain.Entities
{
    public class Address : BaseEnity
    {
        public string? Street { get; set; }
        public string? State { get; set; }
        public string? PostCode { get; set; }
        // public User User{ get; set; }
    }
}