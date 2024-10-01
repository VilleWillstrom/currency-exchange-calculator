namespace currency_exchange_calculator.Models
{
    /// <summary>
    /// Represents a request to create a new currency conversion.
    /// This model includes the necessary data for converting from one currency to another.
    /// </summary>
    public class CurrencyConversionNew
    {
        /// <summary>
        /// Gets or sets the currency code from which the conversion is made (e.g., "USD").
        /// </summary>
        public required string FromCurrency { get; set; }

        /// <summary>
        /// Gets or sets the currency code to which the conversion is made (e.g., "EUR").
        /// </summary>
        public required string ToCurrency { get; set; }

        /// <summary>
        /// Gets or sets the amount of the original currency to be converted.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the converted amount in the target currency.
        /// </summary>
        public decimal ConvertedAmount { get; set; }

        /// <summary>
        /// Gets or sets the exchange rate used for the conversion.
        /// </summary>
        public decimal ExchangeRate { get; set; }
    }
}
