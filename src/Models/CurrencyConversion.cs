namespace currency_exchange_calculator.Models
{
    /// <summary>
    /// Represents a currency conversion between two currencies.
    /// Includes details such as the conversion rate, amounts, and the date of the conversion.
    /// </summary>
    public class CurrencyConversion
    {
        /// <summary>
        /// Gets or sets the unique identifier for the currency conversion.
        /// </summary>
        public int Id { get; set; }

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

        /// <summary>
        /// Gets or sets the date and time when the conversion was performed.
        /// </summary>
        public DateTime ConversionDate { get; set; }
    }
}
