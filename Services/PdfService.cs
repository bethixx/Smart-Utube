using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Smart_Utube.DTOs.Movie;

public class PdfService
{
    public byte[] GenerateMoviesPdf(List<MovieReadDto> movies)
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Content().Column(col =>
                {
                    col.Item().Text("Movies list").FontSize(20);

                    foreach (var m in movies)
                    {
                        col.Item().Text($"{m.Title} - {m.Duration} min");
                    }
                });
            });
        })
        .GeneratePdf();
    }
}