public class ImgurUploadRequest
{
    public string title;
    public string description;
    public string album;
    public string image;
    public string video;
    public bool disable_audio;
    public string type = "base64";


    /// <param name="title">The title of the upload</param>
    /// <param name="description">The description of the upload</param>
    /// <param name="album">The deletehash of the album you want to add the upload to, this deletehash is returned at creation of an album</param>
    /// <param name="disable_audio">Remove the audio track from a video file</param>
    public ImgurUploadRequest(string title = "", string description = "", string album = "", bool disable_audio = false)
    {
        this.title = title;
        this.description = description;
        this.album = album;
        this.disable_audio = disable_audio;
    }
}
