using CurrencyConverterAPI.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CurrencyConverterAPI.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        // Constructor that initializes HttpClient and retrieves API key from configuration.
        // Throws an exception if the API key is not set in configuration settings.
        public CurrencyService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["API_KEY"] 
                      ?? throw new Exception("API_KEY is not set in configuration.");
        }

        // Converts currency based on the request details.
        // Makes an HTTP GET request to the external API to fetch exchange rates.
        // Parses the response and calculates the converted amount based on the provided exchange rate.
        // Returns a CurrencyConversionResponse with the conversion details.
        // Throws an exception if the conversion fails or the API response is invalid.
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
