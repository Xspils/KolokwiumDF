using namepsace KolokwiumDF.Models;

public class Subscription
{
    public int IdSubsription { get; set; }
    public string Name { get; set; }
    public int RewnewalPeriod { get; set; }
    public DateTime EndTime { get; set; }
    public decimal Price { get; set; }
}
