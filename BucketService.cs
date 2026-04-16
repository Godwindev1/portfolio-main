using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;

public enum UploadType
{
    Video,
    Screenshot,
    File
}

public partial class BucketService
{
    private readonly IAmazonS3 _s3Client;
    private readonly AmazonS3Config _s3Config;
    private readonly string _bucketName = "portfolio-bucket";

    private Dictionary<string, string> _FileNameToProgressMapping = new Dictionary<string, string>();
    public BucketService(IAmazonS3 s3Client, AmazonS3Config s3Config)
    {
        _s3Client = s3Client;
        _s3Config = s3Config;
    }

    public async Task MakeBucketPublicAsync(string bucketName)
    {
        // Define the policy (ensure the bucket name is correct in the Resource string)
        string publicPolicy = $@"{{
            ""Version"": ""2012-10-17"",
            ""Statement"": [
                {{
                    ""Effect"": ""Allow"",
                    ""Principal"": ""*"",
                    ""Action"": ""s3:GetObject"",
                    ""Resource"": ""arn:aws:s3:::{bucketName}/*""
                }}
            ]
        }}";

        try
        {
            var request = new PutBucketPolicyRequest
            {
                BucketName = bucketName,
                Policy = publicPolicy
            };

            await _s3Client.PutBucketPolicyAsync(request);
            Console.WriteLine($"Bucket '{bucketName}' is now public.");
        }
        catch (AmazonS3Exception e)
        {
            Console.WriteLine($"Error setting policy: {e.Message}");
        }
    }

    public async Task MakeBucketPrivateAsync(string bucketName)
    {
        try
        {
            // Deleting the policy reverts to default — deny all public access
            await _s3Client.DeleteBucketPolicyAsync(new DeleteBucketPolicyRequest
            {
                BucketName = bucketName
            });

            Console.WriteLine($"Bucket '{bucketName}' is now private.");
        }
        catch (AmazonS3Exception e)
        {
            Console.WriteLine($"Error making bucket private: {e.Message}");
        }
    }
    public async Task CreateBucketAsync()
    {
        var request = new PutBucketRequest
        {
            BucketName = _bucketName,
            UseClientRegion = true
        };

        if(!await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, _bucketName))
        {
            Console.WriteLine($"Creating bucket '{_bucketName}'...");
            await _s3Client.PutBucketAsync(request);
        }

        await MakeBucketPrivateAsync(_bucketName);
    }

    public async Task<string> UploadVideo(Stream MultiPartVidStream, string fileName, string ContentType)
    {
        try
        {
           return await UploadFile(MultiPartVidStream, fileName, ContentType,  UploadType.Video);            
        }
        catch (AmazonS3Exception e)
        {
            Console.WriteLine($"S3 Error: {e.Message}");
        }

        return "";
    }

    public async Task<string> UploadScreenShot(Stream MultiPartImgStream, string fileName, string ContentType )
    {
        try
        {
           return await  UploadFile(MultiPartImgStream, fileName, ContentType, UploadType.Screenshot);
        }
        catch (AmazonS3Exception e)
        {
            Console.WriteLine($"S3 Error: {e.Message}");
        }

        return "";
    }

    public async Task<string> UploadFile( Stream MultiPartFileStream, string fileName,  string ContentType = "application/octet-stream", UploadType Objecttype = UploadType.File )
    {
        try
        {
            var ObjectKey = Objecttype switch
            {
                UploadType.Video => $"Videos/{fileName}",
                UploadType.Screenshot => $"Screenshots/{fileName}",
                UploadType.File => $"Files/{fileName}",
                _ => $"Files/{fileName}"
            };

            var fileTransferUtility = new TransferUtility(_s3Client);

            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = MultiPartFileStream,
                Key = ObjectKey,
                BucketName = _bucketName,
                ContentType = ContentType,
                PartSize = 6291456, // 6 MB chunks
            };

            uploadRequest.UploadProgressEvent += (s, e) =>
            {
                //Console.WriteLine($"Uploaded {e.PercentDone}%...");
                _FileNameToProgressMapping[fileName] = $"{e.PercentDone}%";
            };


            await fileTransferUtility.UploadAsync(uploadRequest);
            
            switch (Objecttype)
            {
                case UploadType.Video:
                    Console.WriteLine("Video upload complete!");
                    break;
                case UploadType.Screenshot:
                    Console.WriteLine("Screenshot upload complete!");
                    break;
                case UploadType.File:
                    Console.WriteLine("File upload complete!");
                    break;
            }

            // Example Construction
            //string FileUrl = $"{_s3Config.ServiceURL}{_bucketName}/{ObjectKey}";
            string FileUrl = $"{ObjectKey}";

            return FileUrl;
        }
        catch (AmazonS3Exception e)
        {
            Console.WriteLine($"S3 Error: {e.Message}");
        }

        return "null";
    }

    public string GetUploadProgress(string fileName)
    {
        return _FileNameToProgressMapping.TryGetValue(fileName, out var progress) ? progress : "No upload in progress for this file.";
    }
}