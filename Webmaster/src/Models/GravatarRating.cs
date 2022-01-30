namespace Wangkanai.Webmaster.Models;

public enum GravatarRating
{
    g,
    pg,
    r,
    x
}

public static class GravatarRatingExtensions
{
    public static string Value(this GravatarRating rating)
        => rating.ToString().ToLower();
}