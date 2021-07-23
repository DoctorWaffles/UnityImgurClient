
# UnityImgur

A small script that allows for uploading videos, images and creating albums on Imgur's servers.

- [Getting Started](#getting-started)
  * [Authentication](#authentication)
- [Functions](#functions)
  * [Uploads](#uploads)
    + [UploadVideo](#uploadvideo)
    + [UploadImage](#uploadimage)
    + [UpdateUpload](#updateupload)
    + [DeleteUpload](#deleteupload)
  * [Albums](#albums)
    + [CreateAlbum](#createalbum)
    + [UpdateAlbum](#updatealbum)

## Getting Started
- Download latest version from [releases](https://github.com/DoctorWaffles/UnityImgur/releases/).
- Import UnityPackage in your Unity Project.

### Authentication
Imgur allows two types of authentication, however given user-authenicated request require a OAuth2.0 token, which  client_id which can be request [here](https://api.imgur.com/oauth2/addclient).

## Functions
All these examples use a path, however a byte[] is also supported.

### Uploads
#### UploadVideo
Uploads a new video.

>With callback
```csharp
Imgur.UploadVideo(Application.dataPath + "/Assets/" + video.mp4", "A new uploaded video", "A new uploaded video through UnityImgur", "", (response) =>
{
    if (response.success)
        Debug.Log(response.data.link);
);
```
>With async
```csharp
ImgurUploadResponse response = await Imgur.UploadVideoAsync(Application.dataPath + "/Assets/" + video.mp4", "A new uploaded video", "A new uploaded video through UnityImgur");
if (response.success)
    Debug.Log(response.data.link);
```

#### UploadImage
Uploads a new image.

>With callback
```csharp
Imgur.UploadImage(Application.dataPath + "/Assets/" + image.jpg", "A new uploaded image", "A new uploaded image through UnityImgur", "", (response) =>
{
    if (response.success)
        Debug.Log(response.data.link);
);
```
>With async
```csharp
ImgurUploadResponse response = await Imgur.UploadImageAsync(Application.dataPath + "/Assets/" + image.jpg", "A new uploaded image", "A new uploaded image through UnityImgur");
if(response.success)
    Debug.Log(response.data.link);
```


#### UpdateUpload
Updates the information of an upload

>With callback
```csharp
Imgur.UpdateUpload("Xf3fyug", "A new title for this upload", "A new description for this upload", (response) =>
{
    if (response.success)
        Debug.Log(response.data.title);
});
```
>With async
```csharp
ImgurUploadResponse response = await Imgur.UpdateUploadAsync("Xf3fyug", "A new title for this upload", "A new description for this upload");
if (response.success)
    Debug.Log(response.data.title);
```

#### DeleteUpload
Deletes an upload

>With callback
```csharp
Imgur.DeleteUpload("Xf3fyug", (response) =>
{
    if (response.success)
        Debug.Log("Upload deleted");
});
```
>With async
```csharp
ImgurResponse response = await Imgur.DeleteUploadAsync("Xf3fyug");
if (response.success)
    Debug.Log("Upload deleted");
```

### Albums

#### CreateAlbum
Creates an album

>With callback
```csharp
Imgur.CreateAlbum("A newly created album", "A newly created album through UnityImgur", null, (response) => {
    if (response.success)
        Debug.Log(response.data.link);
});
```
>With async
```csharp
ImgurAlbumResponse response = await Imgur.CreateAlbumAsync("A newly created album", "A newly created album through UnityImgur");
if (response.success)
    Debug.Log(response.data.link);
```

#### UpdateAlbum
Updates the information of an album
>With callback
```csharp
Imgur.UpdateAlbum("XyigKf", "A new title for this album", "A new description for this album", null, (response) => {
    if (response.success)
        Debug.Log(response.data.link);
});
```
>With async
```csharp
ImgurAlbumResponse response = await Imgur.UpdateAlbumAsync("XyigKf", "A new title for this album", "A new description for this album", null);
if (response.success)
    Debug.Log(response.data.link);
```

