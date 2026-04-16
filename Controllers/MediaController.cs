

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

    [LocalOnlyFilter]
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
            MediaType.Video => await _bucketService.GetVideoStreamAsync(ObjectKey),
            _ => await _bucketService.GetFileStreamAsync(ObjectKey)
        };

        if (type == MediaType.Video)
        {
            Response.Headers.Append("Accept-Ranges", "bytes");
            return File(resultStream.Value.stream,  resultStream.Value.contentType, enableRangeProcessing: true);
        }

        return File(resultStream.Value.stream, resultStream.Value.contentType, fileDownloadName: ObjectKey.Split("/").ElementAt(1) );
    }


}