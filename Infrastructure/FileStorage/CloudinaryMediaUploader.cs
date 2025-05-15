using Application.Interfaces;
using Application.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace Infrastructure.FileStorage;

public class CloudinaryMediaUploader : IFileUploader
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryMediaUploader(IOptions<CloudinarySettings> options)
    {
        var settings = options.Value;

        var account = new Account(settings.Name, settings.Key, settings.Secret);
        _cloudinary = new Cloudinary(account);
    }

    public Task<string> Upload(FileUpload file)
    {
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.Name, file.Stream),
            UseFilename = true,
            UniqueFilename = false,
            Overwrite = true
        };
        //HANDLE Unuploaded FILE?
        var uploadResult = _cloudinary.Upload(uploadParams);
        return Task.FromResult(uploadResult.SecureUrl.ToString());
    }
}