using currency_exchange_calculator.Interfaces;
using currency_exchange_calculator.Models;
using Microsoft.Extensions.Configuration;

namespace currency_exchange_calculator.Services
{
    /// <summary>
    /// Implements the ICurrencyService interface to handle currency conversion operations.
    /// This service uses HttpClient to fetch exchange rates from an external API.
    /// </summary>
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyService"/> class.
        /// </summary>
        /// <param name="httpClient">HttpClient used to make requests to the external API.</param>
        /// <param name="configuration">Configuration to retrieve the API key from settings.</param>
        /// <exception cref="Exception">Thrown when API_KEY is not set in the configuration settings.</exception>
        public CurrencyService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["API_KEY"] 
                        ?? throw new Exception("API_KEY is not set in configuration.");
        }

        /// <summary>
        /// Converts currency based on the request details by making an HTTP request to an external API.
        /// </summary>
        /// <param name="request">The currency conversion request containing the source and target currencies and the amount.</param>
        /// <returns>A <see cref="CurrencyConversionResponse"/> object with the conversion details.</returns>
        /// <exception cref="Exception">Thrown if the currency conversion fails or if the target currency is not found in the API response.</exception>
        public async Task<CurrencyConversionResponse> ConvertCurrencyAsync(CurrencyConversionRequest request)
        {
            // Constructs the API URL for fetching exchange rates with the provided base currency and API key.
            var url = $"https://api.exchangerate-api.com/v4/latest/{request.FromCurrency}?apiKey={_apiKey}";

            // Sends an asynchronous HTTP GET request to the API.
            var response = await _httpClient.GetAsync(url);
            // Ensures that the response indicates success.
            response.EnsureSuccessStatusCode();

            // Reads and deserializes the response content into an ExchangeRateApiResponse object.
            var data = await response.Content.ReadFromJsonAsync<ExchangeRateApiResponse>();
            
            // Checks if the data is not null and contains the exchange rate for the target currency.
            if (data != null && data.Rates.TryGetValue(request.ToCurrency, out var rate))
            {
                // Calculates the converted amount using the exchange rate.
                var convertedAmount = request.Amount * rate;

                // Returns a response with the conversion details.
                return new CurrencyConversionResponse
                {
                    FromCurrency = request.FromCurrency,
                    ToCurrency = request.ToCurrency,
                    Amount = request.Amount,
                    ConvertedAmount = convertedAmount,
                    ExchangeRate = rate
                };
            }

            // Throws an exception if the currency conversion fails or the target currency is not found in the API response.
            throw new Exception("Currency conversion failed.");
        }
    }
}
