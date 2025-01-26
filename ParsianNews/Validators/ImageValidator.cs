using SkiaSharp;

namespace ParsianNews.Validators;

public static class ImageValidator
{
    public static bool IsImage(this IFormFile file)
    {
        try
        {
            using (var stream = file.OpenReadStream())
            {
                using (var managedStream = new SKManagedStream(stream))
                {
                    var codec = SKCodec.Create(managedStream);
                    return codec != null;
                }
            }
        }
        catch
        {
            return false;
        }
    }
}