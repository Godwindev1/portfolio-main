using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;

public class DisposeChainStream(Stream inner, IDisposable chain) : Stream
{
    public override bool CanRead => inner.CanRead;
    public override bool CanSeek => false; // S3 streams are never seekable
    public override bool CanWrite => false; // read-only

    // Guard these — they throw on non-seekable streams
    public override long Length => throw new NotSupportedException();
    public override long Position
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }
    public override long Seek(long offset, SeekOrigin origin) 
        => throw new NotSupportedException();
    public override void SetLength(long value) 
        => throw new NotSupportedException();

    // Only what actually matters for streaming
    public override int Read(byte[] buffer, int offset, int count) 
        => inner.Read(buffer, offset, count);
    public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken ct) 
        => inner.ReadAsync(buffer, offset, count, ct);
    public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken ct = default) 
        => inner.ReadAsync(buffer, ct);

    public override void Flush() => inner.Flush();
    public override void Write(byte[] buffer, int offset, int count) 
        => throw new NotSupportedException();

    protected override void Dispose(bool disposing)
    {
        inner.Dispose();
        chain.Dispose();
    }
}
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


    public async Task<(DisposeChainStream stream, string contentType, long contentLength)?> GetObjectStreamAsync(string objectKey, ByteRange ? Range = null, bool isVideo = false)
    {
        try
        {
            var request = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key        = objectKey
            };

            if (Range != null && isVideo)
            {
                request.ByteRange = Range;
            }
            
            var response = await _s3Client.GetObjectAsync(request);

            long? totalLength = null;
            if (response.Headers["Content-Length"] is string contentRange)
            {
                // Format: "bytes 0-1023/94832"
                var total = contentRange.Split('/').LastOrDefault();
                if (long.TryParse(total, out var parsed))
                    totalLength = parsed;
            }

            var combinedStream = new DisposeChainStream(response.ResponseStream, response);
            return (combinedStream, response.Headers.ContentType, totalLength ?? 0);
            
           // return (response.ResponseStream, response.Headers.ContentType, totalLength);
        }
        catch (AmazonS3Exception e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public Task<(DisposeChainStream stream, string contentType, long contentLength)?> GetVideoStreamAsync(string fileName, ByteRange Range)
        => GetObjectStreamAsync($"{fileName}", Range, true);

    public Task<(DisposeChainStream stream, string contentType, long contentLength)?> GetScreenshotStreamAsync(string fileName)
        => GetObjectStreamAsync($"{fileName}");

    public Task<(DisposeChainStream stream, string contentType, long contentLength)?> GetFileStreamAsync(string fileName)
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