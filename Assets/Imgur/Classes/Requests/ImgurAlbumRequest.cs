
public class ImgurAlbumRequest
{
    /// <summary>
    /// The deletehashes of the images that you want to be included in the album.
    /// </summary>
    public string[] deletehashes;

    /// <summary>
    /// The title of the album
    /// </summary>
    public string title;

    /// <summary>
    /// The description of the album
    /// </summary>
    public string description;

    /// <summary>
    /// Sets the privacy level of the album. Values are : public | hidden | secret.
    /// </summary>
    public string privacy;

    /// <summary>
    /// The deletehash of an image that you want to be the cover of the album
    /// </summary>
    public string cover;

    /// <summary>
    /// The image ids that you want to be removed from the album.
    /// </summary>
    public string[] ids;

    public ImgurAlbumRequest(string[] deletehashes = null, string title = "", string description = "", string privacy = "", string cover = "", string[] ids = null)
    {
        this.deletehashes = deletehashes;
        this.title = title;
        this.description = description;
        this.privacy = privacy;
        this.cover = cover;
        this.ids = ids;
    }

}
