
# UnityImgurClient

A small script that allows for uploading videos, images and creating albums on Imgur's servers similar to CarlHalstead's [Imgur-For-Unity](https://github.com/CarlHalstead/Imgur-for-Unity), however with a few more methods such as updating and deleting previously uploaded content and albums.

- [Getting Started](#getting-started)
  * [Authentication](#authentication)
  * [Example](#example)
- [Functions](#functions)
  * [Uploads](#uploads)
    + [UploadVideo](#uploadvideo)
    + [UploadImage](#uploadimage)
    + [UpdateUpload](#updateupload)
    + [DeleteUpload](#deleteupload)
  * [Albums](#albums)
    + [CreateAlbum](#createalbum)
    + [GetAlbum](#getalbum)
    + [UpdateAlbum](#updatealbum)
    + [DeleteAlbum](#deletealbum)
  * [Misc.](#misc)
    + [GetRateLimit](#getratelimit)

## Getting Started
- Download latest version from [releases](https://github.com/DoctorWaffles/UnityImgur/releases/).
- Import UnityPackage in your Unity Project.

### Authentication
To use UnityImgurClient, you need to have a Imgur client_id which can be optained from [here](https://api.imgur.com/oauth2/addclient). Unfortunately Unity isn't ideal with using OAuth 2.0 tokens, thus all the methods in this client will generate anonymouse uploads and albums. However, these anonymouse uploads and albums do return a deletehash - which can be used as a token to delete or update an upload or album.


### Example
```csharp
// Authenticate
UnityImgurClient ImgurClient = new UnityImgurClient("5362011a8be87ae");

// Upload two images and store their deletehashes
List<string> imageDeletehashes = new List<string>();
ImgurUploadRequest imageUploadRequest = new ImgurUploadRequest(title: "An image title", description: "An image description");
for (int i = 0; i < 2; i++)
{
    ImgurUploadResponse uploadImageResponse = await ImgurClient.UploadImageAsync(Application.dataPath + @"\image.jpg", imageUploadRequest);
    if (uploadImageResponse.success)
    {
        Debug.Log($"Succesfully uploaded an image with deletehash {uploadImageResponse.data.deletehash}");
        imageDeletehashes.Add(uploadImageResponse.data.deletehash);
    }
    else
    {
        Debug.Log($"Something went wrong whilst uploading this upload with code {uploadImageResponse.status}");
        return;
    }
      
}

// If no images were uploaded, we don't want to make an album
if(imageDeletehashes.Count < 0) return;

// Create a new album with those images
ImgurAlbumRequest createAlbumRequest = new ImgurAlbumRequest(title: "An album title", description: "An album description", deletehashes: imageDeletehashes.ToArray());
ImgurAlbumResponse createAlbumResponse = await ImgurClient.CreateAlbumAsync(createAlbumRequest);
if (createAlbumResponse.success)
{
    Debug.Log($"Succesfully created album with deletehash {createAlbumResponse.data.deletehash}");
}
else
{
    Debug.Log($"Something went wrong with creating an album with code {createAlbumResponse.status}");
    return;
}

// Get link of the newly created album
ImgurAlbumResponse getAlbumResponse = await ImgurClient.GetAlbumAsync(createAlbumResponse.data.id);
if (getAlbumResponse.success)
{
    Debug.Log($"Created album can be found on {getAlbumResponse.data.link}");
}
else
{
    Debug.Log($"Something went wrong whilst fetching this album with code {createAlbumResponse.status}");
    return;
}
```

## Functions
All these methods in the readme use an absolute path, however all these functions can also be overloaded with a byte[].

### Uploads
#### UploadVideo
Uploads a new video.

>With callback
```csharp
var request = new ImgurUploadRequest(title: "A video title", description: "A video description");
ImgurClient.UploadVideo(Application.dataPath + @"\video.mp4", request, (response) =>
{
    if (response.success)
    {
        Debug.Log($"Uploaded video can be found on {response.data.link}");
    }
    else
    {
        Debug.Log($"Something went wrong uploading with code {response.status}");
    }
});
```
>With async
```csharp
var request = new ImgurUploadRequest(title: "A video title", description: "A video description");
var response = await ImgurClient.UploadVideoAsync(Application.dataPath + @"\video.mp4", request);
if (response.success)
{
    Debug.Log($"Uploaded video can be found on {response.data.link}");
}
else
{
    Debug.Log($"Something went wrong uploading with code {response.status}");
}
```

#### UploadImage
Uploads a new image.

> With callback
```csharp
var request = new ImgurUploadRequest(title: "An image title", description: "An image description");
ImgurClient.UploadImage(Application.dataPath + @"\image.jpg", request, (response) =>
{
    if (response.success)
    {
        Debug.Log($"Uploaded image can be found on {response.data.link}");
    }
    else
    {
        Debug.Log($"Something went wrong uploading with code {response.status}");
    }
});
```
> With async
```csharp
var request = new ImgurUploadRequest(title: "An image title", description: "An image description");
var response = await ImgurClient.UploadImageAsync(Application.dataPath + @"\image.jpg", request);
if (response.success)
{
    Debug.Log($"Uploaded image can be found on {response.data.link}");
}
else
{
    Debug.Log("Something went wrong");
}
```

#### UpdateUpload
Updates the information of an upload

>With callback
```csharp
ImgurUploadRequest request = new ImgurUploadRequest(title: "A new title", description: "A new description");
ImgurClient.UpdateUpload("Xf3fyug", request, (response) =>
{
    if (response.success)
    {
        Debug.Log($"Updated upload successfully");
    }
    else
    {
        Debug.Log($"Something went wrong updating this upload with code {response.status}");
    }   
});
```
>With async
```csharp
ImgurUploadRequest request = new ImgurUploadRequest(title: "A new title", description: "A new description");
ImgurUploadResponse response = await ImgurClient.UpdateUploadAsync("Xf3fyug", request);
if (response.success)
{
    Debug.Log($"Updated upload successfully");
}
else
{
    Debug.Log($"Something went wrong updating this upload with code {response.status}");
}
```

#### DeleteUpload
Deletes an upload

>With callback
```csharp
 ImgurClient.DeleteUpload("Xfig4gh", (response) =>
{
    if (response.success)
    {
        Debug.Log($"Succesfully deleted upload");
    }
    else
    {
        Debug.Log($"Something went wrong whilst deleting with code {response.status}");
    }
});
```
>With async
```csharp
ImgurResponse response = await ImgurClient.DeleteUploadAsync("Xfig4gh");
if (response.success)
{
    Debug.Log($"Succesfully deleted upload");
}
else
{
    Debug.Log($"Something went wrong whilst deleting with code {response.status}");
}
```

### Albums

#### CreateAlbum
Creates an album

>With callback
```csharp
ImgurAlbumRequest request = new ImgurAlbumRequest(title: "An album title", description: "An album description");
ImgurClient.CreateAlbum(request, (response) => {
    if (response.success)
    {
        Debug.Log($"Succesfully created album with deletehash {response.data.deletehash}");
    }
    else
    {
        Debug.Log($"Something went wrong with creating an album with code {response.status}");
    }
});
```
>With async
```csharp
ImgurAlbumRequest request = new ImgurAlbumRequest(title: "An album title", description: "An album description");
ImgurAlbumResponse response = await ImgurClient.CreateAlbumAsync(request);
if (response.success)
{
    Debug.Log($"Succesfully created album with deletehash {response.data.deletehash}");
}
else
{
    Debug.Log($"Something went wrong with creating an album with code {response.status}");
}
```

#### GetAlbum
Gets additional information about an album

>With callback
```csharp
ImgurClient.GetAlbum("Xfig4gh", (response) =>
{
    if (response.success)
    {
        Debug.Log($"Succesfully received album with id {response.data.deletehash}");
    }
    else
    {
        Debug.Log($"Something went wrong with fetching album with the code {response.status}");
    }
});
```

>With async
```csharp
ImgurAlbumResponse response = await ImgurClient.GetAlbumAsync("Xfig4gh");
if (response.success)
{
    Debug.Log($"Succesfully received album with id {response.data.id}");
}
else
{
    Debug.Log($"Something went wrong with fetching album with the code {response.status}");
}
```

#### UpdateAlbum
Updates the information of an album
>With callback
```csharp
ImgurAlbumRequest request = new ImgurAlbumRequest(title: "An image title", description: "An image description");
ImgurClient.UpdateAlbum("Xfig4gh", request, (response) => {
    if (response.success)
    {
        Debug.Log($"Succesfully updated album with deletehash {response.data.deletehash}");
    }
    else
    {
        Debug.Log($"Something went wrong with updating album with code {response.status}");
    }
});
```
>With async
```csharp
ImgurAlbumRequest request = new ImgurAlbumRequest(title: "An album title", description: "An album description");
ImgurAlbumResponse response = await ImgurClient.UpdateAlbumAsync("Xfig4gh", request);
if (response.success)
{
    Debug.Log($"Succesfully updated album with deletehash {response.data.deletehash}");
}
else
{
    Debug.Log($"Something went wrong with updating album with code {response.status}");
}
```

#### DeleteAlbum
Deletes an album with a given deletehash.
>With callback
```csharp
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
```
>With async
```csharp
ImgurResponse response = await ImgurClient.DeleteAlbumAsync("Xfig4gh");
if (response.success)
{
    Debug.Log($"Succesfully deleted album");
}
else
{
    Debug.Log($"Something went wrong with deleting album with code {response.status}");
}
```


### Misc.

#### GetRateLimit
Gets the current credit budget of the current user and application, for more details read more about Imgur's Rate Limits on [Imgur's API Docs](https://apidocs.imgur.com/#intro)
>With callback
```csharp
ImgurClient.GetRateLimit((response) => {
    if (response.success)
    {
        Debug.Log($"{response.data.UserRemaining} credits remaining");
    }
    else
    {
        Debug.Log($"Something went wrong obtaining rate limits with code {response.status}");
    }
});
```

>With async
```csharp
ImgurRateLimitResponse response = await ImgurClient.GetRateLimitAsync();
if (response.success)
{
    Debug.Log($"{response.data.UserRemaining} credits remaining");
}
else
{
    Debug.Log($"Something went wrong obtaining rate limits with code {response.status}");
}
```