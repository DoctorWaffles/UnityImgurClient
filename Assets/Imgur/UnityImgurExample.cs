using System.Collections.Generic;
using UnityEngine;

public class UnityImgurExample : MonoBehaviour
{
    async void Start()
    {
        UnityImgur.Authenticate("5362011a8be87ae");

        List<string> imageDeletehashes = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            ImgurUploadResponse uploadResponse = await UnityImgur.UploadImageAsync(Application.dataPath + @"\" + "image.jpg");
            if (!uploadResponse.success) return;
            imageDeletehashes.Add(uploadResponse.data.deletehash);
            Debug.Log("Successfully uploaded image with deletehash " + uploadResponse.data.deletehash);

        }

        if (imageDeletehashes.Count <= 0) return;

        ImgurAlbumResponse createAlbumResponse = await UnityImgur.CreateAlbumAsync("A new album", "A newly created album!", "", "", imageDeletehashes.ToArray());
        if (!createAlbumResponse.success) return;

        ImgurAlbumResponse getAlbumResponse = await UnityImgur.GetAlbumAsync(createAlbumResponse.data.id);
        if (!getAlbumResponse.success) return;

        Debug.LogFormat("The newly created album can be found on {0}", getAlbumResponse.data.link);
    }
}
