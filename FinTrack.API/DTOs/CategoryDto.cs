namespace FinTrack.API.DTOs;

public class CategoryDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}