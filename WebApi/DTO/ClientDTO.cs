namespace WebApi.DTO

{
    public class ClientDto
    {
        public int ClientID { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
    }
}
