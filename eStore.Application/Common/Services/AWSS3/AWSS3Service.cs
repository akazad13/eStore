using Amazon.S3;
using Amazon.S3.Model;
using eStore.Application.Common.Utilities;
using Microsoft.Extensions.Options;

namespace eStore.Application.Common.Services.AWSS3
{
    public class AWSS3Service : IAWSS3Service
    {
        private readonly ConfigModel _configModel;

        public AWSS3Service(IOptions<ConfigModel> configModel)
        {
            _configModel = configModel.Value;
        }

        private AmazonS3Client InitializeS3()
        {
            var config = new AmazonS3Config
            {
                ServiceURL = _configModel.AWS?.ServiceURL,
                RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(_configModel.AWS?.Region)
            };
            var publicKey = _configModel.AWS?.PublicKey;
            var secretKey = _configModel.AWS?.SecretKey;
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
                    BucketName = _configModel.AWS?.ImageBucketName,
                    Key = fileNameToUpload,
                    InputStream = stream
                };

                PutObjectResponse response = await s3Client.PutObjectAsync(putRequest);

                //return $"https://{_configModel.AWS?.ImageBucketName}.s3.{_configModel.AWS?.Region}.amazonaws.com/{fileNameToUpload}";
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
                    var S3BucketName = _configModel.AWS?.ImageBucketName;
                    var Client = InitializeS3();
                    var Request = new DeleteObjectRequest()
                    {
                        BucketName = S3BucketName,
                        Key = filename
                    };
                    DeleteObjectResponse response = await Client.DeleteObjectAsync(Request);
                    Client.Dispose();
                    return true;
                }
                else
                {
                    return Exist;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CheckFileExistInFolderS3(string filename)
        {
            try
            {
                var S3BucketName = _configModel.AWS?.ImageBucketName;
                var Client = InitializeS3();
                var Request = new GetObjectMetadataRequest()
                {
                    BucketName = S3BucketName,
                    Key = filename,
                };
                GetObjectMetadataResponse response = await Client.GetObjectMetadataAsync(Request);
                ///TODO: check the meta data response
                Client.Dispose();
                return true;
            }
            catch (Exception ex)
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
                expiryUrlRequest.BucketName = _configModel.AWS?.ImageBucketName;
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
