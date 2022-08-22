namespace MinimalApi.Entities;

public class Movie
{
    public int id { get; set; }

    public string? title { get; set; }

    public string? description { get; set; }

    public DateTime CreatedDate { get; set; }
}
