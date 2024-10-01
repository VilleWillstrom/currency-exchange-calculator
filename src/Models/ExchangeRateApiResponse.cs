namespace currency_exchange_calculator.Models
{
    /// <summary>
    /// Represents the response from an exchange rate API.
    /// This model includes the base currency and the exchange rates for various target currencies.
    /// </summary>
    public class ExchangeRateApiResponse
    {
        /// <summary>
        /// Gets or sets the base currency code used for the exchange rates (e.g., "USD").
        /// </summary>
        public required string Base { get; set; }

        /// <summary>
        /// Gets or sets the dictionary of exchange rates where the key is the target currency code
        /// and the value is the exchange rate relative to the base currency.
        /// </summary>
        public required Dictionary<string, decimal> Rates { get; set; }
    }
}
