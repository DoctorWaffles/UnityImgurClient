using System;
using System.Collections.Generic;

[Serializable]
public class ImgurAlbum
{
    public string id;
    public string deletehash;
    public string title;
    public object description;
    public int datetime;
    public string cover;
    public string account_url;
    public int account_id;
    public string privacy;
    public string layout;
    public int views;
    public string link;
    public int images_count;
    public List<ImgurUpload> images;
}
