using EPiServer.Framework.Blobs;

namespace Chief2moro.ImageDataExtensions
{
    public class ImageBlobUtility
    {
        public static Dimensions GetDimensions(Blob imageBlob)
        {
            var imageDimensions = Dimensions.Default;

            using (var stream = imageBlob.OpenRead())
            {
                var image = System.Drawing.Image.FromStream(stream, false);
               
                imageDimensions.Width = image.Width;
                imageDimensions.Height = image.Height;
                
                image.Dispose();
            }

            return imageDimensions;
        }
    }
}