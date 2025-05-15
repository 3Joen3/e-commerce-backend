namespace Application.Models;

public class FileUpload
{
    public required string Name { get; set; }
    public required Stream Stream { get; set; }
}