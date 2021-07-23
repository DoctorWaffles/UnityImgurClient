using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Imgur
{
    static readonly string IMGUR_BASE_URL = "https://api.imgur.com/3/";
    static readonly string UPLOAD_PATH = "upload";
    static readonly string ALBUM_PATH = "album";
    static readonly string CREDITS_PATH = "credits";

    private static string _clientId;

    /// <summary>
    ///  Updates the information of an album
    /// </summary>
    /// <param name="pDeleteHash">The deletehash of the album</param>
    /// <param name="pTitle">The title of the ablum</param>
    /// <param name="pDescription">The description of the ablum</param>
    /// <param name="pDeleteHashes">The deletehashes of the images that you want to be included in the album</param>
    /// <param name="pCallback">Response callback</param>
    public static void UpdateAlbum(string pDeleteHash, string pTitle, string pDescription = "", string[] pDeleteHashes = null, Action<ImgurAlbumResponse> pCallback = null)
    {
        var values = new NameValueCollection
        {
             { "title", pTitle },
             { "description", pDescription },
             { "privacy", "public " },
             { "deletehashes[]", JsonUtility.ToJson(pDeleteHashes) }

        };
        Request(string.Format("{0}/{1}", ALBUM_PATH, pDeleteHash), ImgurRequestMethod.POST, values, pCallback);
    }

    /// <summary>
    /// Updates the information of an album
    /// </summary>
    /// <param name="pDeleteHash">The deletehash of the album</param>
    /// <param name="pTitle">The title of the ablum</param>
    /// <param name="pDescription">The description of the ablum</param>
    /// <param name="pDeleteHashes">The deletehashes of the images that you want to be included in the album</param>
    public static async Task<ImgurAlbumResponse> UpdateAlbumAsync(string pDeleteHash, string pTitle, string pDescription = "", string[] pDeleteHashes = null)
    {
        var values = new NameValueCollection
        {
             { "title", pTitle },
             { "description", pDescription },
             { "privacy", "public " },
             { "deletehashes[]", JsonUtility.ToJson(pDeleteHashes) }

        };
        return await RequestAsync<ImgurAlbumResponse>(string.Format("{0}/{1}", ALBUM_PATH, pDeleteHash), ImgurRequestMethod.POST, values);
    }



    /// <summary>
    /// Gets additional information about an album
    /// </summary>
    /// <param name="pDeleteHash">The deletehash of the album</param>
    /// <param name="pCallback">Response callback</param>
    public static void GetAlbum(string pDeleteHash, Action<ImgurAlbumResponse> pCallback = null)
    {
        Request(string.Format("{0}/{1}", ALBUM_PATH, pDeleteHash), ImgurRequestMethod.GET, null, pCallback);
    }

    /// <summary>
    /// Gets additional information about an album
    /// </summary>
    /// <param name="pDeleteHash">The deletehash of the album</param>
    public static async Task<ImgurAlbumResponse> GetAlbumAsync(string pDeleteHash)
    {
        return await RequestAsync<ImgurAlbumResponse>(string.Format("{0}/{1}", ALBUM_PATH, pDeleteHash), ImgurRequestMethod.GET, null);
    }



    /// <summary>
    /// Updates the information of an upload
    /// </summary>
    /// <param name="pDeleteHash">The deletehash of the upload</param>
    /// <param name="pTitle">The new title of the upload</param>
    /// <param name="pDescription">The new description of the upload</param>
    public static async Task<ImgurUploadResponse> UpdateUploadAsync(string pDeleteHash, string pTitle, string pDescription)
    {
        var values = new NameValueCollection
        {
             { "title", pTitle },
             { "description", pDescription },

        };
        return await RequestAsync<ImgurUploadResponse>(string.Format("{0}/{1}", UPLOAD_PATH, pDeleteHash), ImgurRequestMethod.POST, values);
    }

    /// <summary>
    /// Updates the information of an upload
    /// </summary>
    /// <param name="pDeleteHash">The deletehash of the upload</param>
    /// <param name="pTitle">The new title of the upload</param>
    /// <param name="pDescription">The new description of the upload</param>
    public static void UpdateUpload(string pDeleteHash, string pTitle, string pDescription, Action<ImgurUploadResponse> pCallback)
    {
        var values = new NameValueCollection
        {
             { "title", pTitle },
             { "description", pDescription },

        };
        Request(string.Format("{0}/{1}", UPLOAD_PATH, pDeleteHash), ImgurRequestMethod.POST, values, pCallback);
    }




    /// <summary>
    /// Deletes an album with a given deletehash.
    /// </summary>
    /// <param name="pDeleteHash">The deletehash of the album</param>
    /// <param name="pCallback">Response callback</param>
    public static void DeleteAlbum(string pDeleteHash, Action<ImgurResponse> pCallback = null)
    {
        Request(string.Format("{0}/{1}", ALBUM_PATH, pDeleteHash), ImgurRequestMethod.DELETE, null, pCallback);
    }

    /// <summary>
    /// Deletes an album with a given deletehash.
    /// </summary>
    /// <param name="pDeleteHash">The deletehash of the album</param>
    public static async Task<ImgurResponse> DeleteAlbumAsync(string pDeleteHash)
    {
        return await RequestAsync<ImgurResponse>(string.Format("{0}/{1}", ALBUM_PATH, pDeleteHash), ImgurRequestMethod.DELETE, null);
    }


    /// <summary>
    /// Creates a new album.
    /// </summary>
    /// <param name="pTitle">The title of the ablum</param>
    /// <param name="pDescription">The description of the ablum</param>
    /// <param name="pDeleteHashes">The deletehashes of the images that needs to be added to the album</param>
    /// <param name="pCallback">Response callback</param>
    public static void CreateAlbum(string pTitle, string pDescription = "", string[] pDeleteHashes = null, Action<ImgurAlbumResponse> pCallback = null)
    {
        var values = new NameValueCollection
        {
             { "title", pTitle },
             { "description", pDescription },
             { "privacy", "public " },
             { "deletehashes[]", JsonUtility.ToJson(pDeleteHashes) }

        };
        Request(ALBUM_PATH, ImgurRequestMethod.POST, values, pCallback);
    }


    /// <summary>
    /// Creates a new album.
    /// </summary>
    /// <param name="pTitle">The title of the ablum</param>
    /// <param name="pDescription">The description of the ablum</param>
    /// <param name="pDeleteHashes">The deletehashes of the images that needs to be added to the album</param>
    public static async Task<ImgurAlbumResponse> CreateAlbumAsync(string pTitle, string pDescription = "", string[] pDeleteHashes = null)
    {
        var values = new NameValueCollection
        {
             { "title", pTitle },
             { "description", pDescription },
             { "privacy", "public " },
             { "deletehashes[]", JsonUtility.ToJson(pDeleteHashes) }

        };
        return await RequestAsync<ImgurAlbumResponse>(ALBUM_PATH, ImgurRequestMethod.POST, values);
    }


    /// <summary>
    /// Uploads an image to Imgur
    /// </summary>
    /// <param name="pBytes">byte[] of the image</param>
    /// <param name="pTitle">The title of the image</param>
    /// <param name="pDescription">The description of the image</param>
    /// <param name="pAlbum">The deletehash of the album that was returned at creation</param>
    /// <param name="pCallback">Response callback</param>
    public static void UploadImage(byte[] pBytes, string pTitle = "", string pDescription = "", string pAlbum = "", Action<ImgurUploadResponse> pCallback = null)
    {
        string base64Image = Convert.ToBase64String(pBytes);
        var values = new NameValueCollection
        {
            { "image", base64Image },
            { "type", "base64" },
            { "title", pTitle },
            { "description", pDescription },
            { "album", pAlbum },
        };

        Request(UPLOAD_PATH, ImgurRequestMethod.POST, values, pCallback);
    }

    /// <summary>
    /// Uploads an image to Imgur
    /// </summary>
    /// <param name="pPath">Full path of the image</param>
    /// <param name="pTitle">The title of the image</param>
    /// <param name="pDescription">The description of the image</param>
    /// <param name="pAlbum">The deletehash of the album that was returned at creation that this image has to be added to</param>
    /// <param name="pCallback">Response callback</param>
    public static void UploadImage(string pPath, string pTitle = "", string pDescription = "", string pAlbum = "", Action<ImgurUploadResponse> pCallback = null)
    {
        UploadImage(File.ReadAllBytes(pPath), pTitle, pDescription, pAlbum, pCallback);
    }



    /// <summary>
    /// Uploads an image to Imgur
    /// </summary>
    /// <param name="pBytes">byte[] of the image</param>
    /// <param name="pTitle">The title of the image</param>
    /// <param name="pDescription">The description of the image</param>
    /// <param name="pAlbum">The deletehash of the album that was returned at creation that this image has to be added to</param>
    public static async Task<ImgurUploadResponse> UploadImageAsync(byte[] pBytes, string pTitle = "", string pDescription = "", string pAlbum = "")
    {
        string base64Image = Convert.ToBase64String(pBytes);
        var values = new NameValueCollection
        {
            { "image", base64Image },
            { "type", "base64" },
            { "title", pTitle },
            { "description", pDescription },
            { "album", pAlbum },
        };

        return await RequestAsync<ImgurUploadResponse>(UPLOAD_PATH, ImgurRequestMethod.POST, values);
    }

    /// <summary>
    /// Uploads an image to Imgur
    /// </summary>
    /// <param name="pPath">Full path of the image</param>
    /// <param name="pTitle">The title of the image</param>
    /// <param name="pDescription">The description of the image</param>
    /// <param name="pAlbum">The deletehash of the album that was returned at creation that this image has to be added to</param>
    public static async Task<ImgurUploadResponse> UploadImageAsync(string pPath, string pTitle = "", string pDescription = "", string pAlbum = "")
    {
        return await UploadImageAsync(File.ReadAllBytes(pPath), pTitle, pDescription, pAlbum);
    }


    /// <summary>
    /// Remove uploads from an album
    /// </summary>
    /// <param name="pDeleteHash">The deletehash of the album</param>
    /// <param name="pIDs">The ids that need to be removed from the album.</param>
    /// <param name="pCallback">Response callback</param>
    public static void RemoveUploadsFromAlbum(string pDeleteHash, string[] pIDs, Action<ImgurAlbumResponse> pCallback = null)
    {
        var values = new NameValueCollection
        {
            { "ids[]", JsonUtility.ToJson(pIDs) },
        };


        Request(string.Format("{0}/{1}/remove_images/", ALBUM_PATH, pDeleteHash), ImgurRequestMethod.POST, values, pCallback);
    }

    /// <summary>
    /// Remove uploads from an album
    /// </summary>
    /// <param name="pDeleteHash">The deletehash of the album</param>
    /// <param name="pIDs">The ids that need to be removed from the album.</param>
    public static async Task<ImgurAlbumResponse> RemoveUploadsFromAlbum(string pDeleteHash, string[] pIDs)
    {
        var values = new NameValueCollection
        {
            { "ids[]", JsonUtility.ToJson(pIDs) },
        };

        return await RequestAsync<ImgurAlbumResponse>(string.Format("{0}/{1}/remove_images/", ALBUM_PATH, pDeleteHash), ImgurRequestMethod.POST, values);
    }


    /// <summary>
    /// Uploads an video to Imgur
    /// </summary>
    /// <param name="pBytes">byte[] of the given video</param>
    /// <param name="pTitle">The title of the video</param>
    /// <param name="pDescription">The description of the video</param>
    /// <param name="pAlbum">The deletehash of the album that was returned at creation</param>
    /// <param name="pCallback">Response callback</param>
    public static void UploadVideo(byte[] pBytes, bool pDisableAudio = false, string pTitle = "", string pDescription = "", string pAlbum = "", Action<ImgurUploadResponse> pCallback = null)
    {
        string base64Video = Convert.ToBase64String(pBytes);
        var values = new NameValueCollection
        {
            { "video", base64Video },
            { "type", "base64" },
            { "title", pTitle },
            { "description", pDescription },
            { "album", pAlbum },
            { "disable_audio", pDisableAudio ? "1" : "0" }
        };

        Request(UPLOAD_PATH, ImgurRequestMethod.POST, values, pCallback);
    }


    /// <summary>
    /// Uploads an video to Imgur
    /// </summary>
    /// <param name="pPath">The full path of the video</param>
    /// <param name="pTitle">The title of the video</param>
    /// <param name="pDescription">The description of the video</param>
    /// <param name="pAlbum">The deletehash of the album that was returned at creation that this video has to be added to</param>
    /// <param name="pCallback">Response callback</param>
    public static void UploadVideo(string pPath, bool pDisableAudio = false, string pTitle = "", string pDescription = "", string pAlbum = "", Action<ImgurUploadResponse> pCallback = null)
    {
        UploadVideo(File.ReadAllBytes(pPath), pDisableAudio, pTitle, pDescription, pAlbum, pCallback);
    }


    /// <summary>
    /// Uploads an video to Imgur
    /// </summary>
    /// <param name="pBytes">byte[] of the given video</param>
    /// <param name="pTitle">The title of the video</param>
    /// <param name="pDescription">The description of the video</param>
    /// <param name="pAlbum">The deletehash of the album that was returned at creation that this video has to be added to</param>
    public static async Task<ImgurUploadResponse> UploadVideoAsync(byte[] pBytes, string pTitle = "", string pDescription = "", string pAlbum = "")
    {
        string base64Video = Convert.ToBase64String(pBytes);
        var values = new NameValueCollection
        {
            { "video", base64Video },
            { "type", "base64" },
            { "title", pTitle },
            { "description", pDescription },
            { "album", pAlbum },
        };

        return await RequestAsync<ImgurUploadResponse>(UPLOAD_PATH, ImgurRequestMethod.POST, values);
    }

    /// <summary>
    /// Uploads an video to Imgur
    /// </summary>
    /// <param name="pPath">The full path of the video</param>
    /// <param name="pTitle">The title of the video</param>
    /// <param name="pDescription">The description of the video</param>
    /// <param name="pAlbum">The deletehash of the album that was returned at creation that this video has to be added to</param>
    public static async Task<ImgurUploadResponse> UploadVideoAsync(string pPath, string pTitle = "", string pDescription = "", string pAlbum = "")
    {
        return await UploadVideoAsync(File.ReadAllBytes(pPath), pTitle, pDescription, pAlbum);
    }

    /// <summary>
    /// Deletes an upload
    /// </summary>
    /// <param name="pDeleteHash">The deletehash of the upload</param>

    public static void DeleteUpload(string pDeleteHash, Action<ImgurResponse> pCallback = null)
    {
        
        Request(string.Format("{0}/{1}/remove_images/", UPLOAD_PATH, pDeleteHash), ImgurRequestMethod.DELETE, null, pCallback);
    }

    /// <summary>
    /// Deletes an upload
    /// </summary>
    /// <param name="pDeleteHash">The deletehash of the upload</param>
    public static async Task<ImgurResponse> DeleteUploadAsync(string pDeleteHash)
    {
        return await RequestAsync<ImgurResponse>(string.Format("{0}/{1}/remove_images/", UPLOAD_PATH, pDeleteHash), ImgurRequestMethod.DELETE, null);
    }


    private static void Request<T>(string pPath, ImgurRequestMethod pMethod, NameValueCollection pValues = null, Action<T> pResponse = null)
    {
        if (!IsAuthenticated())
        {
            Debug.LogError("You have not yet authenticated, call Imgur.Authenticate first");
            return;
        }

        using (WebClient client = new WebClient())
        {

            client.Headers.Add("Authorization", "Client-ID " + _clientId);
            string json = "";
            switch (pMethod)
            {
                case ImgurRequestMethod.POST:
                    json = Encoding.UTF8.GetString(client.UploadValues(IMGUR_BASE_URL + pPath, pMethod.ToString(), pValues));
                    break;
                case ImgurRequestMethod.GET:
                    json = client.DownloadString(IMGUR_BASE_URL + pPath);
                    break;
                case ImgurRequestMethod.DELETE:
                    json = client.DownloadString(IMGUR_BASE_URL + pPath);
                    break;
                default:
                    break;
            }
            pResponse?.Invoke(JsonUtility.FromJson<T>(json));
        }

    }

    private static async Task<T> RequestAsync<T>(string pPath, ImgurRequestMethod pMethod, NameValueCollection pValues = null)
    {
        if (!IsAuthenticated())
        {
            Debug.LogError("You have not yet authenticated, call Imgur.Authenticate first");
            return default;
        }
        using (WebClient client = new WebClient())
        {

            client.Headers.Add("Authorization", "Client-ID " + _clientId);
            string json = "";
            switch (pMethod)
            {
                case ImgurRequestMethod.POST:
                    json = Encoding.UTF8.GetString(await client.UploadValuesTaskAsync(IMGUR_BASE_URL + pPath, pMethod.ToString(), pValues));
                    break;
                case ImgurRequestMethod.GET:
                    json = await client.DownloadStringTaskAsync(IMGUR_BASE_URL + pPath);
                    break;
                case ImgurRequestMethod.DELETE:
                    json = await client.DownloadStringTaskAsync(IMGUR_BASE_URL + pPath);
                    break;
                default:
                    break;
            }
            return JsonUtility.FromJson<T>(json);
        }
    }

    /// <summary>
    /// Gets the current credit budget of the current user and application
    /// </summary>
    /// <param name="pCallback">Response callback</param>

    public static void GetRateLimit(Action<ImgurRateLimitResponse> pCallback = null)
    {
        Request(CREDITS_PATH, ImgurRequestMethod.GET, null, pCallback);
    }

    /// <summary>
    /// Gets the current credit budget of the current user and application
    /// </summary>
    public static async Task<ImgurRateLimitResponse> GetRateLimitAsync()
    {
        return await RequestAsync<ImgurRateLimitResponse>(CREDITS_PATH, ImgurRequestMethod.GET, null);
    }

    public static void Authenticate(string pClientID)
    {
        if (pClientID.Equals(string.Empty))
            Debug.LogError("You need a client ID to use the API! You can get one from here: " + "https://api.imgur.com/oauth2/addclient");

        _clientId = pClientID;
    }

    private static bool IsAuthenticated()
    {
        return _clientId != null && !_clientId.Equals(string.Empty);
    }

}