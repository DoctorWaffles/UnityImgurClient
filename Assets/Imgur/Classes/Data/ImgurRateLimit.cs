using System;

[Serializable]
public class ImgurRateLimit
{
    /// <summary>
    /// Total credits that can be allocated.
    /// </summary>
    public int UserLimit;

    /// <summary>
    /// Total credits available.
    /// </summary>
    public int UserRemaining;

    /// <summary>
    /// Timestamp (unix epoch) for when the credits will be reset.
    /// </summary>
    public DateTime UserReset;


    /// <summary>
    /// Total credits that can be allocated for the application in a day.
    /// </summary>
    public int ClientLimit;

    /// <summary>
    /// Total credits remaining for the application in a day.
    /// </summary>
    public int ClientRemaining;
}
