using Smart_Utube.DTOs.Quote;

public interface IQuoteService
{
    Task<QuoteDto?> GetRandomQuoteAsync();
}