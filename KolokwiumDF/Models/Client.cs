using namepsace KolokwiumDF.Models;

public class Client : DbContext
{
    public int IdClient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int Phone { get; set; }
}
