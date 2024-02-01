using Amazon.S3;
using Amazon.S3.Model;
using eStore.Application.Common.Utilities;
using Microsoft.Extensions.Options;

namespace eStore.Application.Common.Services.AWSS3
{
    public class AwsS3Service(IOptions<ConfigModel> configModel) : IAwsS3Service
    {
        private readonly ConfigModel _configModel = configModel.Value;

        private AmazonS3Client InitializeS3()
        {
            var config = new AmazonS3Config
            {
                ServiceURL = _configModel.Aws.ServiceURL,
                RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(_configModel.Aws.Region)
            };
            var publicKey = _configModel.Aws.PublicKey;
            var secretKey = _configModel.Aws.SecretKey;
            AmazonS3Client s3Client = new(publicKey, secretKey, config);
            return s3Client;
        }

        public async Task<bool> PushToAmazonS3ViaRest(string fileNameToUpload, Stream stream)
        {
            stream.Position = 0;
            Exception error;
            try
            {
                AmazonS3Client s3Client = InitializeS3();

                var putRequest = new PutObjectRequest()
                {
                    BucketName = _configModel.Aws.ImageBucketName,
                    Key = fileNameToUpload,
                    InputStream = stream
                };

                await s3Client.PutObjectAsync(putRequest);
                return true;
            }
            catch (AmazonS3Exception awsEx)
            {
                error = awsEx;
                return false;
            }
            catch (Exception Ex)
            {
                error = Ex;
                return false;
            }
        }

        public async Task<bool> DeleteFileS3(string filename)
        {
            try
            {
                bool Exist = await CheckFileExistInFolderS3(filename);
                if (Exist)
                {
                    var S3BucketName = _configModel.Aws.ImageBucketName;
                    var Client = InitializeS3();
                    var Request = new DeleteObjectRequest()
                    {
                        BucketName = S3BucketName,
                        Key = filename
                    };
                    await Client.DeleteObjectAsync(Request);
                    Client.Dispose();
                    return true;
                }
                else
                {
                    return Exist;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CheckFileExistInFolderS3(string filename)
        {
            try
            {
                var S3BucketName = _configModel.Aws.ImageBucketName;
                var Client = InitializeS3();
                var Request = new GetObjectMetadataRequest()
                {
                    BucketName = S3BucketName,
                    Key = filename,
                };
                await Client.GetObjectMetadataAsync(Request);
                Client.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetFileURL(string fileNameToDownload)
        {
            string url = "";
            try
            {
                var Client = InitializeS3();

                var expiryUrlRequest = new GetPreSignedUrlRequest();
                expiryUrlRequest.BucketName = _configModel.Aws.ImageBucketName;
                expiryUrlRequest.Key = fileNameToDownload;
                expiryUrlRequest.Protocol = Amazon.S3.Protocol.HTTPS;
                expiryUrlRequest.Verb = Amazon.S3.HttpVerb.GET;
                expiryUrlRequest.Expires = DateTime.Now.AddMilliseconds(50000);

                url = Client.GetPreSignedURL(expiryUrlRequest);
            }
            catch (Exception ex)
            {
                url = "File Link Error: " + ex.Message;
            }

            return url;
        }
    }
}
