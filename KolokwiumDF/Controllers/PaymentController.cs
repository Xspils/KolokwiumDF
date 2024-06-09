using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KolokwiumDF.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly baza _context;

    public PaymentController(baza context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentDto paymentDto)
    {
//1
        var client = await _context.Clients.FindAsync(paymentDto.IdClient);
        if (client == null)
        {
            return NotFound(new { message = "Klient chyba nie istnieje" });
        }
//2
        var subscription = await _context.Subscriptions.FindAsync(paymentDto.IdSubscription);
        if (subscription == null)
        {
            return NotFound(new { message = "Chyba nie ma subskrypcji" });
        }
//3
        if (subscription.EndTime < DateTime.UtcNow)
        {
            return BadRequest(new { message = "Na pewno nie ma subskrypcji" });
        }

//4
        var existingPayment = await _context.Payments
            .Where(p => p.IdClient == paymentDto.IdClient && p.IdSubscription == paymentDto.IdSubscription && p.Date >= subscription.EndTime.AddMonths(-subscription.RewnewalPeriod))
            .FirstOrDefaultAsync();

        if (existingPayment != null)
        {
            return BadRequest(new { message = "Chyba juz oplacil" });
        }
//5

        var activeDiscount = await _context.Discounts
            .Where(d => d.IdClient == paymentDto.IdClient && d.DateFrom <= DateTime.UtcNow && d.DateTo >= DateTime.UtcNow)
            .FirstOrDefaultAsync();
//6
        decimal finalAmount = subscription.Price;
        if (activeDiscount != null && decimal.TryParse(activeDiscount.Value, out var discountValue))
        {
            finalAmount = subscription.Price - (subscription.Price * discountValue / 100);
        }
         if (paymentDto.Amount != finalAmount)
        {
            return BadRequest(new { message = "zaplata nie zgodna ze znika chyba" });
        }
//7
        var payment = new Payment
        {
            IdClient = paymentDto.IdClient,
            IdSubscription = paymentDto.IdSubscription,
            Date = DateTime.UtcNow,
            Amount = paymentDto.Amount
        };

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();
//8
        return Ok(new { message = "Chyba sie utworzylo, wiec powinno byc git", PaymentId = payment.IdPayment });
    }
}