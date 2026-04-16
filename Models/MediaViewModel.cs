public class MediaViewerModel
{
    //this is the objectKey in bucket
    public string Url  { get; set; } = "";
    public string Type { get; set; } = "File"; // "Image" | "Video" | "File"
    public string label {get; set; } = "";
}