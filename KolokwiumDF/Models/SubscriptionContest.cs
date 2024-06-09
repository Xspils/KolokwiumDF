using namepsace KolokwiumDF.Models;

public class SubscriptionContext(DbContextOptions<SubscriptionContext>options) : base options
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; 
}