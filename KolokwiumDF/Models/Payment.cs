using namepsace KolokwiumDF.Models;

public class Payment : DbContext
{
	public int IdPayment { get; set; }
	public DateTime Date { get; set; }
	public int IdClient { get; set; }
	public int IdSubscription { get; set; }
}