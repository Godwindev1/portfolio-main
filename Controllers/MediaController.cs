

using System.Net.Http.Headers;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.ViewModels;


public enum MediaType { Image, Video, File }



[Route("media")]
public class MediaController : Controller
{
    BucketService _bucketService;
    public MediaController(BucketService bucket)
    {
        _bucketService = bucket;
    }

    //[LocalOnlyFilter]
    [HttpGet("stream")]
    public async Task<IActionResult> Stream([FromQuery]string ObjectKey, [FromQuery]MediaType type, [FromServices]IConfiguration configuration)
    {
        /*var referer = Request.Headers["Referer"].ToString();
        if (string.IsNullOrEmpty(referer) || !referer.StartsWith($"https://{configuration["DOMAIN_NAME"]}"))
        {
            return Forbid(); 
        }*/ 
        

        var resultStream =  type switch  
        {
            MediaType.Image => await _bucketService.GetScreenshotStreamAsync(ObjectKey),
            MediaType.Video => null,
            _ => await _bucketService.GetFileStreamAsync(ObjectKey)
        };

        if (type == MediaType.Image)
        {
            Response.Headers.Append("Cache-Control", "public, max-age=31536000, immutable");
            Response.Headers.Append("ETag", $"\"{ObjectKey}\"");
        }

        if (type == MediaType.Video)
        {
            ByteRange? range = null;
            var Meta = await _bucketService.GetObjectMetadataAsync(ObjectKey); // You need the total size
            long totalLength = Meta.ContentLength;

            if (Request.Headers.TryGetValue("Range", out var rangeHeader))
            {
                if (RangeHeaderValue.TryParse(rangeHeader, out var parsed))
                {
                    var r = parsed.Ranges.First();
                    long start = r.From ?? 0;
                    long end = r.To ?? (totalLength - 1);
                    
                    // Ensure end doesn't exceed total length
                    end = Math.Min(end, totalLength - 1);
                    
                    range = new ByteRange(start, end);

                    long chunkLength = (end - start) + 1;

                    Response.StatusCode = StatusCodes.Status206PartialContent;
                    Response.Headers.Append("Content-Range", $"bytes {start}-{end}/{totalLength}");
                    Response.Headers.Append("Content-Length", chunkLength.ToString());
                }
            }
            else 
            {
                Response.Headers.Append("Content-Length", totalLength.ToString());
            }

            var result = await _bucketService.GetVideoStreamAsync(ObjectKey, range);
            
            Response.Headers.Append("Accept-Ranges", "bytes");
            
            // Use the stream from the bucket service
            return File(result.Value.stream, result.Value.contentType, enableRangeProcessing: false);
        }

        if (type == MediaType.Video)
        {
            Response.Headers.Append("Accept-Ranges", "bytes");
            //return File(resultStream.Value.stream,  resultStream.Value.contentType, enableRangeProcessing: true);
        }



        return File(resultStream.Value.stream, resultStream.Value.contentType, fileDownloadName: ObjectKey.Split("/").ElementAt(1) );
    }


}