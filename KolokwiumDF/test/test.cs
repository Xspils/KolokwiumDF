using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly baza _context;

        public ClientController(baza context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDto>> GetClientSubscriptions(int id)
        {
            var client = await _context.Clients
                .Include(c => c.Discount)
                .Include(c => c.Subscriptions)
                    .ThenInclude(s => s.Payments)
                .FirstOrDefaultAsync(c => c.Idclient == id);

            if (client == null)
            {
                return NotFound();
            }

            var clientDto = new ClientDto
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                Phone = client.Phone,
                Discount = client.Discount?.Value,
                Subscriptions = client.Subscriptions.Select(s => new SubscriptionDto
                {
                    IdSubscription = s.IdSubsription,
                    Name = s.Name,
                    RenewalPeriod = s.RewnewalPeriod,
                    TotalPaidAmount = s.Payments.Sum(p => p.Amount)
                }).ToList()
            };

            return clientDto;
        }
    }

    public class ClientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Discount { get; set; }
        public List<SubscriptionDto> Subscriptions { get; set; }
    }

    public class SubscriptionDto
    {
        public int IdSubscription { get; set; }
        public string Name { get; set; }
        public int RenewalPeriod { get; set; }
        public decimal TotalPaidAmount { get; set; }
    }
}

public class Client
{
    public int Idclient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int Phone { get; set; }
    public Discount Discount { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; }
}

public class Discount
{
    public int IdDiscount { get; set; }
    public string Value { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int IdClient { get; set; }
    public Client Client { get; set; }
}

public class Payment
{
    public int IdPayment { get; set; }
    public DateTime Date { get; set; }
    public int IdClient { get; set; }
    public int IdSubscription { get; set; }
    public decimal Amount { get; set; }
    public Client Client { get; set; }
    public Subscription Subscription { get; set; }
}

public class Sale
{
    public int IdSale { get; set; }
    public int IdClient { get; set; }
    public int IdSubscription { get; set; }
    public string Email { get; set; }
    public int Phone { get; set; }
    public Client Client { get; set; }
    public Subscription Subscription { get; set; }
}

public class Subscription
{
    public int IdSubsription { get; set; }
    public string Name { get; set; }
    public int RewnewalPeriod { get; set; }
    public DateTime EndTime { get; set; }
    public decimal Price { get; set; }
    public ICollection<Payment> Payments { get; set; }
    public ICollection<Client> Clients { get; set; }
}
