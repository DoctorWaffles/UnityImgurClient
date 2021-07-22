using UnityEngine;

public class ImgurUploadTester : MonoBehaviour
{
    async void Start()
    {
        Imgur.Authenticate("5362011a8be87ae");




        //   ImgurUploadResponse response = await Imgur.UploadVideoAsync(@"D:\Projects\Imgur\Assets\dIwaxvU.mp4", "Epic Title", "A description", "");

        //Imgur.DeleteImage("", (response) => { Debug.Log("Doesn't work"); });

        ImgurRateLimitResponse response = await Imgur.GetRateLimitAsync();
        // Imgur.UploadVideo
        Debug.Log(response.data.UserRemaining);


        //

        //Imgur.GetRateLimit((response) => {

        //  Debug.LogFormat("{0} credits remaining", response.data.ClientRemaining);
        //});



        //  Debug.Log(response.data.link);
        //// Add images to album
        //for (int i = 0; i < 1; i++)
        //{
        //    Imgur.UploadVideo(@"D:\Projects\Imgur\Assets\dIwaxvU.mp4", "Epic Title", "A description", "", (response) =>
        //    {
        //        Debug.Log(response.data.link);
        //    });
        //}





        //Imgur.GetAlbum(response.data.id, (response) => {
        //    Debug.Log(response.data.privacy);
        //    Debug.Log(response.data.link);
        //})
        //;


    }

    // Update is called once per frame
    void Update()
    {

    }
}
