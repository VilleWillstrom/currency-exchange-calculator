using currency_exchange_calculator.Interfaces;
using currency_exchange_calculator.Models;
using Microsoft.AspNetCore.Mvc;

namespace currency_exchange_calculator.Controllers
{
    /// <summary>
    /// This controller handles currency conversion requests.
    /// It utilizes ICurrencyService to perform the currency conversion logic.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        /// <summary>
        /// Constructor for CurrencyController. 
        /// Injects ICurrencyService to handle business logic for currency conversions.
        /// </summary>
        /// <param name="currencyService">Service responsible for currency conversion operations.</param>
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        /// <summary>
        /// Converts an amount from one currency to another.
        /// Receives a request body that contains the conversion parameters.
        /// </summary>
        /// <param name="request">CurrencyConversionRequest object containing the conversion details.</param>
        /// <returns>
        /// Returns the result of the currency conversion or a corresponding error message.
        /// - 200 OK: Returns the converted amount and rate.
        /// - 400 BadRequest: If the input model is invalid.
        /// - 500 InternalServerError: If an unexpected error occurs during processing.
        /// </returns>
        [HttpPost("convert")]
        public async Task<IActionResult> ConvertCurrency([FromBody] CurrencyConversionRequest request)
        {
            // Validate the request model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Perform the currency conversion using the injected service.
                var result = await _currencyService.ConvertCurrencyAsync(request);

                // Return a 200 OK response with the conversion result.
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error if an exception occurs.
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
