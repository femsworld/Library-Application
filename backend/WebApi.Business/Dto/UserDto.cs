namespace WebApi.Business.Dto
{
    // [AutoMap(typeof(User))]
    public class UserDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; } 
        // [Ignore]
        
        // public byte[] Password { get; set; } 
    }
}