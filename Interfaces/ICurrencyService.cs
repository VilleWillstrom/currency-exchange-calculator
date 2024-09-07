namespace CurrencyConverterAPI.Interfaces
{
    public interface ICurrencyService
    {
        Task<CurrencyConversionResponse> ConvertCurrencyAsync(CurrencyConversionRequest request);
    }
}
