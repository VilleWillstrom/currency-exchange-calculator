namespace currency_exchange_calculator.Models
{
    /// <summary>
    /// Represents the response of a currency conversion operation.
    /// This model includes details about the conversion result.
    /// </summary>
    public class CurrencyConversionResponse
    {
        /// <summary>
        /// Gets or sets the currency code from which the conversion was made (e.g., "USD").
        /// </summary>
        public required string FromCurrency { get; set; }

        /// <summary>
        /// Gets or sets the currency code to which the conversion was made (e.g., "EUR").
        /// </summary>
        public required string ToCurrency { get; set; }

        /// <summary>
        /// Gets or sets the amount that was converted from the source currency.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the result of the conversion in the target currency.
        /// </summary>
        public decimal ConvertedAmount { get; set; }

        /// <summary>
        /// Gets or sets the exchange rate used for the conversion.
        /// </summary>
        public decimal ExchangeRate { get; set; }
    }
}
