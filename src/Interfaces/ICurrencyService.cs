using currency_exchange_calculator.Models;

namespace currency_exchange_calculator.Interfaces
{
    /// <summary>
    /// Defines a contract for a currency conversion service.
    /// This service provides functionality to convert one currency to another.
    /// </summary>
    public interface ICurrencyService
    {
        /// <summary>
        /// Converts an amount from one currency to another asynchronously.
        /// </summary>
        /// <param name="request">The request containing conversion details such as the amount and currencies.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains a <see cref="CurrencyConversionResponse"/> with the conversion details.
        /// </returns>
        Task<CurrencyConversionResponse> ConvertCurrencyAsync(CurrencyConversionRequest request);
    }
}
