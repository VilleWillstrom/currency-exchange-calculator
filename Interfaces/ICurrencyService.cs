namespace CurrencyConverterAPI.Interfaces
{
    // This interface defines a contract for a currency conversion service.
    // It includes a single method 'ConvertCurrencyAsync' that accepts a CurrencyConversionRequest 
    // and returns a task that resolves to a CurrencyConversionResponse. 
    // This method will handle the logic for converting one currency to another asynchronously.
    public interface ICurrencyService
    {
        Task<CurrencyConversionResponse> ConvertCurrencyAsync(CurrencyConversionRequest request);
    }
}
