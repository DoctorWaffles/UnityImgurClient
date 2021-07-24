using System;
using System.Collections.Generic;

[Serializable]
public class ImgurUpload
{
    public string id;
    public string deletehash;
    public string account_id;
    public string account_url;
    public string ad_type;
    public string ad_url;
    public string title;
    public string description;
    public string name;
    public string type;
    public int width;
    public int height;
    public int size;
    public int views;
    public string section;
    public string vote;
    public int bandwidth;
    public bool animated;
    public bool favorite;
    public bool in_gallery;
    public bool in_most_viral;
    public bool has_sound;
    public bool is_ad;
    public string nsfw;
    public string link;
    public List<string> tags;
    public int datetime;
    public string mp4;
    public string hls;
}
