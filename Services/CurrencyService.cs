using CurrencyConverterAPI.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyConverterAPI.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "6df8261d2094863630899368"; // Kovakoodattu API-avain

        public CurrencyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CurrencyConversionResponse> ConvertCurrencyAsync(CurrencyConversionRequest request)
        {
            var url = $"https://api.exchangerate-api.com/v4/latest/{request.FromCurrency}?apiKey={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<ExchangeRateApiResponse>();
            if (data != null && data.Rates.TryGetValue(request.ToCurrency, out var rate))
            {
                var convertedAmount = request.Amount * rate;

                return new CurrencyConversionResponse
                {
                    FromCurrency = request.FromCurrency,
                    ToCurrency = request.ToCurrency,
                    Amount = request.Amount,
                    ConvertedAmount = convertedAmount,
                    ExchangeRate = rate
                };
            }

            throw new Exception("Currency conversion failed.");
        }
    }
}
