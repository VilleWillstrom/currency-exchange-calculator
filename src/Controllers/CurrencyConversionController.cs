using Microsoft.AspNetCore.Mvc;
using currency_exchange_calculator.Data;
using currency_exchange_calculator.Models;
using Microsoft.EntityFrameworkCore;

namespace currency_exchange_calculator.Controllers
{
    /// <summary>
    /// The controller responsible for handling currency conversion operations.
    /// It provides CRUD operations for currency conversions.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyConversionController : ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyConversionController"/> class.
        /// Injects the application database context to interact with the database.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public CurrencyConversionController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a specific currency conversion record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the currency conversion record.</param>
        /// <returns>
        /// The currency conversion record if found, otherwise a 404 NotFound response.
        /// - 200 OK: If the conversion record is found.
        /// - 404 NotFound: If the conversion record does not exist.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConversion(int id)
        {
            var conversion = await _context.CurrencyConversions.FindAsync(id);
            if (conversion == null)
            {
                return NotFound();
            }

            return Ok(conversion);
        }

        /// <summary>
        /// Creates a new currency conversion record.
        /// </summary>
        /// <param name="request">The request object containing the currency conversion details.</param>
        /// <returns>
        /// A newly created currency conversion record.
        /// - 201 Created: If the conversion record is successfully created.
        /// - 400 BadRequest: If the input data is invalid.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateConversion([FromBody] CurrencyConversionNew request)
        {
            var conversion = new CurrencyConversion
            {
                FromCurrency = request.FromCurrency,
                ToCurrency = request.ToCurrency,
                Amount = request.Amount,
                ConvertedAmount = request.ConvertedAmount,
                ExchangeRate = request.ExchangeRate,
                ConversionDate = DateTime.UtcNow // Automatically set the conversion date.
            };

            _context.CurrencyConversions.Add(conversion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetConversion), new { id = conversion.Id }, conversion);
        }

        /// <summary>
        /// Updates an existing currency conversion record.
        /// </summary>
        /// <param name="id">The unique identifier of the currency conversion record to be updated.</param>
        /// <param name="conversion">The updated currency conversion object.</param>
        /// <returns>
        /// A 204 NoContent response if the update is successful.
        /// - 204 NoContent: If the update is successful.
        /// - 400 BadRequest: If the input data is invalid or IDs don't match.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConversion(int id, [FromBody] CurrencyConversion conversion)
        {
            if (id != conversion.Id)
            {
                return BadRequest();
            }

            _context.Entry(conversion).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes a currency conversion record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the currency conversion record to delete.</param>
        /// <returns>
        /// A 204 NoContent response if the deletion is successful.
        /// - 204 NoContent: If the deletion is successful.
        /// - 404 NotFound: If the record to delete is not found.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConversion(int id)
        {
            var conversion = await _context.CurrencyConversions.FindAsync(id);
            if (conversion == null)
            {
                return NotFound();
            }

            _context.CurrencyConversions.Remove(conversion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
