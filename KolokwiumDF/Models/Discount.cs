using namepsace KolokwiumDF.Models;

public class Discount : DbContext
{
    public int IdDiscount { get; set; }
    public string Value { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int IdClient { get; set; }
}