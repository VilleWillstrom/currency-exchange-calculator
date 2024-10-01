namespace currency_exchange_calculator.Models
{
    /// <summary>
    /// Represents a request to convert a specified amount from one currency to another.
    /// This model includes the necessary details for the currency conversion.
    /// </summary>
    public class CurrencyConversionRequest
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
        /// Gets or sets the amount to be converted from the source currency.
        /// </summary>
        public decimal Amount { get; set; }
    }   
}
