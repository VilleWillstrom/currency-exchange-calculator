public class ExchangeRateApiResponse
{
    public required string Base { get; set; }
    public required Dictionary<string, decimal> Rates { get; set; }
}