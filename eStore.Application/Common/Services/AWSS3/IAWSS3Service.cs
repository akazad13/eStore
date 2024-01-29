using Amazon.S3;

namespace eStore.Application.Common.Services.AWSS3
{
    public interface IAWSS3Service
    {
        Task<bool> CheckFileExistInFolderS3(string filename);
        Task<bool> DeleteFileS3(string filename);
        string GetFileURL(string fileNameToDownload);
        Task<bool> PushToAmazonS3ViaRest(string fileNameToUpload, Stream stream);
    }
}
