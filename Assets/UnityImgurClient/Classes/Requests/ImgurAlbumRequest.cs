
public class ImgurAlbumRequest
{
    public string[] deletehashes;
    public string title;
    public string description;
    public string privacy;
    public string cover;


    /// <param name="deletehashes">The deletehashes of the images that you want to be included in the album</param>
    /// <param name="title">The title of the album</param>
    /// <param name="description">The description of the album</param>
    /// <param name="privacy">Sets the privacy level of the album</param>
    /// <param name="cover">The ID of an image that you want to be the cover of the album</param>
    public ImgurAlbumRequest(string[] deletehashes = null, string title = "", string description = "", string privacy = "", string cover = "")
    {
        this.deletehashes = deletehashes;
        this.title = title;
        this.description = description;
        this.privacy = privacy;
        this.cover = cover;
    }

}
