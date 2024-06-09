using namepsace KolokwiumDF.Models.DTOs;

public class SubscriptionDTO
{
    public int IdSubsripction { get; set; }
    public string Name { get; set; }
    public int RewnewalPeriod { get; set; }
    public int TotalPaidAmount { get; set; }
}