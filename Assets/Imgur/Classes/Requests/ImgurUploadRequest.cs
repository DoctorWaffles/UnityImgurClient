using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImgurUploadRequest 
{

    /// <summary>
    /// The title of the image
    /// </summary>
    public string title;


    /// <summary>
    /// The description of the image
    /// </summary>
    public string description;

    /// <summary>
    /// The deletehash of the album you want to add the image to
    /// </summary>
    public string album;

    /// <summary>
    /// A binary file, base64 data, or a URL for an image. (up to 10MB)
    /// </summary>
    public string image;

    /// <summary>
    /// A binary file(up to 200MB)
    /// </summary>
    public string video;

    /// <summary>
    /// Will remove the audio track from a video file
    /// </summary>
    public bool disable_audio;

    /// <summary>
    /// Will remove the audio track from a video file
    /// </summary>
    public string type = "base64";

    public ImgurUploadRequest(string title = "", string description = "", string album = "", string image = "", string video = "", bool disable_audio = false)
    {
        this.title = title;
        this.description = description;
        this.album = album;
        this.image = image;
        this.video = video;
        this.disable_audio = disable_audio;
    }
}
