using namepsace KolokwiumDF.Models;

public class Sale
{
	public int IdSale { get; set; }
	public int IdClient { get; set; }
	public int IdSubscription { get; set; }
	public string Email { get; set; }
	public int Phone { get; set; }
}