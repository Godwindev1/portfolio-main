using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;

public partial class BucketService
{

    public string GetPresignedUrl(string objectKey, int expiryMinutes = 60)
    {
        var request = new GetPreSignedUrlRequest
        {
            Protocol = Amazon.S3.Protocol.HTTP,
            BucketName = _bucketName,
            Key        = objectKey,
            Expires    = DateTime.UtcNow.AddMinutes(expiryMinutes)
        };

        return _s3Client.GetPreSignedURL(request);
    }

    public string GetVideoUrl(string fileName, int expiryMinutes = 60)
        => GetPresignedUrl($"{fileName}", expiryMinutes);

    public string GetScreenshotUrl(string fileName, int expiryMinutes = 60)
        => GetPresignedUrl($"{fileName}", expiryMinutes);

    public string GetFileUrl(string fileName, int expiryMinutes = 60)
        => GetPresignedUrl($"{fileName}", expiryMinutes);



    public async Task<(Stream stream, string contentType)?> GetObjectStreamAsync(string objectKey)
    {
        try
        {
            var response = await _s3Client.GetObjectAsync(new GetObjectRequest
            {
                BucketName = _bucketName,
                Key        = objectKey
            });

            return (response.ResponseStream, response.Headers.ContentType);
        }
        catch (AmazonS3Exception e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public Task<(Stream stream, string contentType)?> GetVideoStreamAsync(string fileName)
        => GetObjectStreamAsync($"{fileName}");

    public Task<(Stream stream, string contentType)?> GetScreenshotStreamAsync(string fileName)
        => GetObjectStreamAsync($"{fileName}");

    public Task<(Stream stream, string contentType)?> GetFileStreamAsync(string fileName)
        => GetObjectStreamAsync($"{fileName}");



    public async Task<GetObjectMetadataResponse?> GetObjectMetadataAsync(string objectKey)
    {
        try
        {
            return await _s3Client.GetObjectMetadataAsync(new GetObjectMetadataRequest
            {
                BucketName = _bucketName,
                Key        = objectKey
            });
        }
        catch (AmazonS3Exception e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }



    public async Task<List<S3Object>> ListObjectsAsync(UploadType type, string? prefix = null)
    {
        var folder = type switch
        {
            UploadType.Video      => "Videos/",
            UploadType.Screenshot => "Screenshots/",
            UploadType.File       => "Files/",
            _                     => ""
        };

        var request = new ListObjectsV2Request
        {
            BucketName = _bucketName,
            Prefix     = prefix is null ? folder : $"{folder}{prefix}"
        };

        var results = new List<S3Object>();

        ListObjectsV2Response response;
        do
        {
            response = await _s3Client.ListObjectsV2Async(request);
            results.AddRange(response.S3Objects);
            request.ContinuationToken = response.NextContinuationToken;
        }
        while ((bool)response.IsTruncated);

        return results;
    }



    public async Task<bool> DeleteObjectAsync(string objectKey)
    {
        try
        {
            await _s3Client.DeleteObjectAsync(new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key        = objectKey
            });
            return true;
        }
        catch (AmazonS3Exception e)
        {
            Console.WriteLine($"Delete error: {e.Message}");
            return false;
        }
    }

    public Task<bool> DeleteVideoAsync(string fileName)      => DeleteObjectAsync($"Videos/{fileName}");
    public Task<bool> DeleteScreenshotAsync(string fileName) => DeleteObjectAsync($"Screenshots/{fileName}");
    public Task<bool> DeleteFileAsync(string fileName)       => DeleteObjectAsync($"Files/{fileName}");
}