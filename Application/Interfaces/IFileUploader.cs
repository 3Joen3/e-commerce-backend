using Application.Models;

namespace Application.Interfaces;

public interface IFileUploader
{
    Task<string> Upload(FileUpload fileUpload);
}