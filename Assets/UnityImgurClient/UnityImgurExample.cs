using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UnityImgurExample : MonoBehaviour
{
    async void Start()
    {
        // Authenticate
        UnityImgurClient ImgurClient = new UnityImgurClient("5362011a8be87ae");


        //// Create a new album with those images
        //ImgurAlbumRequest request = new ImgurAlbumRequest(title: "An album title", description: "An album description");
        //ImgurAlbumResponse response = await ImgurClient.CreateAlbumAsync(request);
        //if (response.success)
        //{
        //    Debug.Log($"Succesfully created album with deletehash {response.data.deletehash}");
        //}
        //else
        //{
        //    Debug.Log($"Something went wrong with creating an album with code {response.status}");
        //}



        ImgurClient.DeleteAlbum("Xfig4gh", (response) =>
        {
            if (response.success)
            {
                Debug.Log($"Succesfully deleted album");
            }
            else
            {
                Debug.Log($"Something went wrong with deleting album with code {response.status}");
            }
        });




    }

}
