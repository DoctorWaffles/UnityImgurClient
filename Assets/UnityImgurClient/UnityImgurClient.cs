using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

public class UnityImgurClient
{
    private const string IMGUR_BASE_URL = "https://api.imgur.com/3/";
    private const string UPLOAD_PATH = "upload";
    private const string ALBUM_PATH = "album";
    private const string IMAGE_PATH = "image";
    private const string CREDITS_PATH = "credits";

    private string _clientId;

    public UnityImgurClient(string pClientID)
    {
        if (pClientID.Equals(string.Empty))
        {
            throw new ArgumentException("You need a client ID to use the API! You can get one from here: " + "https://api.imgur.com/oauth2/addclient", nameof(pClientID));
        }
        _clientId = pClientID;
    }

    /// <summary>
    /// Updates the information of an album
    /// </summary>
    /// <param name="pDeleteHash">The deletehash of the album</param>
    /// <param name="pRequest">The information that needs to be updated</param>
    public void UpdateAlbum(string pDeleteHash, ImgurAlbumRequest pRequest, Action<ImgurAlbumResponse> pCallback)
    {
        string request = JsonUtility.ToJson(pRequest);
        Request($"{ALBUM_PATH}/{pDeleteHash}", ImgurRequestMethod.POST, request, pCallback);
    }

    /// <summary>
    /// Updates the information of an album
    /// </summary>
    /// <param name="pDeleteHash">The deletehash of the album</param>
    /// <param name="pRequest">The information that needs to be updated</param>
    public async Task<ImgurAlbumResponse> UpdateAlbumAsync(string pDeleteHash, ImgurAlbumRequest pRequest)
    {
        string request = JsonUtility.ToJson(pRequest);
        return await RequestAsync<ImgurAlbumResponse>($"{ALBUM_PATH}/{pDeleteHash}", ImgurRequestMethod.POST, request);
    }

    /// <summary>
    /// Gets additional information about an album
    /// </summary>
    /// <param name="pAlbumID">The id of the album</param>
    /// <param name="pCallback">Response callback</param>
    public void GetAlbum(string pAlbumID, Action<ImgurAlbumResponse> pCallback = null)
    {
        Request($"{ALBUM_PATH}/{pAlbumID}", ImgurRequestMethod.GET, null, pCallback);
    }

    /// <summary>
    /// Gets additional information about an album
    /// </summary>
    /// <param name="pAlbumID">The id of the album</param>
    public async Task<ImgurAlbumResponse> GetAlbumAsync(string pAlbumID)
    {
        return await RequestAsync<ImgurAlbumResponse>($"{ALBUM_PATH}/{pAlbumID}", ImgurRequestMethod.GET, null);
    }

    /// <summary>
    /// Deletes an album with a given deletehash.
    /// </summary>
    /// <param name="pDeleteHash">The deletehash of the album</param>
    /// <param name="pCallback">Response callback</param>
    public void DeleteAlbum(string pDeleteHash, Action<ImgurResponse> pCallback = null)
    {
        Request($"{ALBUM_PATH}/{pDeleteHash}", ImgurRequestMethod.DELETE, null, pCallback);
    }

    /// <summary>
    /// Deletes an album with a given deletehash.
    /// </summary>
    /// <param name="pDeleteHash">The deletehash of the album</param>
    public async Task<ImgurResponse> DeleteAlbumAsync(string pDeleteHash)
    {
        return await RequestAsync<ImgurResponse>(string.Format("{0}/{1}", ALBUM_PATH, pDeleteHash), ImgurRequestMethod.DELETE);
    }

    /// <summary>
    /// Creates a new album.
    /// </summary>
    /// <param name="pRequest">The information that needs to be added to the new album</param>
    /// <param name="pCallback">Response callback</param>
    public void CreateAlbum(ImgurAlbumRequest pRequest = null, Action<ImgurAlbumResponse> pCallback = null)
    {
        string request = JsonUtility.ToJson(pRequest);
        Request(ALBUM_PATH, ImgurRequestMethod.POST, request, pCallback);
    }

    /// <summary>
    /// Creates a new album.
    /// </summary>
    /// <param name="pRequest">The information that needs to be added to the new album</param>
    public async Task<ImgurAlbumResponse> CreateAlbumAsync(ImgurAlbumRequest pRequest = null)
    {
        string request = JsonUtility.ToJson(pRequest);
        return await RequestAsync<ImgurAlbumResponse>(ALBUM_PATH, ImgurRequestMethod.POST, request);
    }

    /// <summary>
    /// Uploads an image to Imgur
    /// </summary>
    /// <param name="pPath">Full path of the image</param>
    /// <param name="pRequest">The information that needs to be added to the new image</param>
    /// <param name="pCallback">Response callback</param>
    public void UploadImage(string pPath, ImgurUploadRequest pRequest = null, Action<ImgurUploadResponse> pCallback = null)
    {
        UploadImage(File.ReadAllBytes(pPath), pRequest, pCallback);
    }



    /// <summary>
    /// Uploads an image to Imgur
    /// </summary>
    /// <param name="pBytes">byte[] of the image</param>
    /// <param name="pRequest">The information that needs to be added to the new image</param>
    public async Task<ImgurUploadResponse> UploadImageAsync(byte[] pBytes, ImgurUploadRequest pRequest = null)
    {
        pRequest.image = Convert.ToBase64String(pBytes);
        string request = JsonUtility.ToJson(pRequest);
        return await RequestAsync<ImgurUploadResponse>(UPLOAD_PATH, ImgurRequestMethod.POST, request);
    }

    /// <summary>
    /// Uploads an image to Imgur
    /// </summary>
    /// <param name="pBytes">byte[] of the image</param>
    /// <param name="pRequest">The information that needs to be added to the new image</param>
    /// <param name="pCallback">Response callback</param>
    public void UploadImage(byte[] pBytes, ImgurUploadRequest pRequest = null, Action<ImgurUploadResponse> pCallback = null)
    {
        pRequest.image = Convert.ToBase64String(pBytes);
        string request = JsonUtility.ToJson(pRequest);
        Request(UPLOAD_PATH, ImgurRequestMethod.POST, request, pCallback);
    }

    /// <summary>
    /// Uploads an image to Imgur
    /// </summary>
    /// <param name="pPath">Full path of the image</param>
    /// <param name="pRequest">The information that needs to be added to the new image</param>
    public async Task<ImgurUploadResponse> UploadImageAsync(string pPath, ImgurUploadRequest pRequest = null)
    {
        return await UploadImageAsync(File.ReadAllBytes(pPath), pRequest);
    }

    /// <summary>
    /// Updates the information of an upload
    /// </summary>
    /// <param name="pDeleteHash">The deletehash of the upload</param>
    /// <param name="pRequest">The information that needs to be updated</param>
    public async Task<ImgurUploadResponse> UpdateUploadAsync(string pDeleteHash, ImgurUploadRequest pRequest = null)
    {
        string request = JsonUtility.ToJson(pRequest);
        return await RequestAsync<ImgurUploadResponse>($"{IMAGE_PATH}/{pDeleteHash}", ImgurRequestMethod.POST, request);
    }

    /// <summary>
    /// Updates the information of an upload
    /// </summary>
    /// <param name="pDeleteHash">The deletehash of the upload</param>
    /// <param name="pRequest">The information that needs to be updated</param>
    public void UpdateUpload(string pDeleteHash, ImgurUploadRequest pRequest = null, Action<ImgurUploadResponse> pCallback = null)
    {
        string request = JsonUtility.ToJson(pRequest);
        Request($"{IMAGE_PATH}/{pDeleteHash}", ImgurRequestMethod.POST, request, pCallback);
    }


    /// <summary>
    /// Uploads an video to Imgur
    /// </summary>
    /// <param name="pBytes">The byte array of the video</param>
    /// <param name="pRequest">The information that needs to be added to the new image</param>
    /// <param name="pCallback">Response callback</param>
    public void UploadVideo(byte[] pBytes, ImgurUploadRequest pRequest = null, Action<ImgurUploadResponse> pCallback = null)
    {
        pRequest.video = Convert.ToBase64String(pBytes);
        string request = JsonUtility.ToJson(pRequest);
        Request(UPLOAD_PATH, ImgurRequestMethod.POST, request, pCallback);
    }

    /// <summary>
    /// Uploads an video to Imgur
    /// </summary>
    /// <param name="pPath">The full path of the video</param>
    /// <param name="pRequest">The information that needs to be added to the new image</param>
    /// <param name="pCallback">Response callback</param>
    public void UploadVideo(string pPath, ImgurUploadRequest pRequest = null, Action<ImgurUploadResponse> pCallback = null)
    {
        UploadVideo(File.ReadAllBytes(pPath), pRequest, pCallback);
    }


    /// <summary>
    /// Uploads an video to Imgur
    /// </summary>
    /// <param name="pBytes">The byte array of the video</param>
    /// <param name="pRequest">The information that needs to be added to the new image</param>
    public async Task<ImgurUploadResponse> UploadVideoAsync(byte[] pBytes, ImgurUploadRequest pRequest = null)
    {
        pRequest.video = Convert.ToBase64String(pBytes);
        string request = JsonUtility.ToJson(pRequest);
        return await RequestAsync<ImgurUploadResponse>(UPLOAD_PATH, ImgurRequestMethod.POST, request);
    }

    /// <summary>
    /// Uploads an video to Imgur
    /// </summary>
    /// <param name="pPath">The full path of the video</param>
    /// <param name="pRequest">The information that needs to be added to the new image</param>
    public async Task<ImgurUploadResponse> UploadVideoAsync(string pPath, ImgurUploadRequest pRequest = null)
    {
        return await UploadVideoAsync(File.ReadAllBytes(pPath), pRequest);
    }

    /// <summary>
    /// Deletes an upload
    /// </summary>
    /// <param name="pDeleteHash">The deletehash of the upload</param>
    public void DeleteUpload(string pDeleteHash, Action<ImgurResponse> pCallback = null)
    {
        Request(string.Format("{0}/{1}/remove_images/", UPLOAD_PATH, pDeleteHash), ImgurRequestMethod.DELETE, null, pCallback);
    }

    /// <summary>
    /// Deletes an upload
    /// </summary>
    /// <param name="pDeleteHash">The deletehash of the upload</param>
    public async Task<ImgurResponse> DeleteUploadAsync(string pDeleteHash)
    {
        return await RequestAsync<ImgurResponse>(string.Format("{0}/{1}/remove_images/", UPLOAD_PATH, pDeleteHash), ImgurRequestMethod.DELETE, null);
    }

    /// <summary>
    /// Gets the current credit budget of the current user and application
    /// </summary>
    /// <param name="pCallback">Response callback</param>
    public void GetRateLimit(Action<ImgurRateLimitResponse> pCallback = null)
    {
        Request(CREDITS_PATH, ImgurRequestMethod.GET, null, pCallback);
    }

    /// <summary>
    /// Gets the current credit budget of the current user and application
    /// </summary>
    public async Task<ImgurRateLimitResponse> GetRateLimitAsync()
    {
        return await RequestAsync<ImgurRateLimitResponse>(CREDITS_PATH, ImgurRequestMethod.GET, null);
    }

    private void Request<T>(string pPath, ImgurRequestMethod pMethod, string pBody = null, Action<T> pResponse = null)
    {
        AsyncUtil.RunSync(() => RequestAsync(pPath, pMethod, pBody, pResponse));
    }

    private async Task<T> RequestAsync<T>(string pPath, ImgurRequestMethod pMethod, string pBody = null, Action<T> pResponse = null)
    {
        using (WebClient client = new WebClient())
        {
            client.Headers.Add("Authorization", "Client-ID " + _clientId);
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            string json = "";
            switch (pMethod)
            {
                case ImgurRequestMethod.POST:
                    json = await client.UploadStringTaskAsync(new Uri(IMGUR_BASE_URL + pPath), "POST", pBody);
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
            pResponse?.Invoke(JsonUtility.FromJson<T>(json));
            return JsonUtility.FromJson<T>(json);

        }
    }
}