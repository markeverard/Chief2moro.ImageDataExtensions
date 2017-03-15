using System;
using System.IO;
using EPiServer.Framework.Blobs;
using EPiServer.ServiceLocation;
using ImageResizer;

namespace Chief2moro.ImageDataExtensions.ImageResizer
{
    public class ImageResizerProcessor : IResizeBlobProcessor
    {
        public Blob CreateImageBlob(Blob originalBlob, Dimensions requestedDimensions, string name)
        {
            var resizeSettings = new ResizeSettings()
            {
                Width = requestedDimensions.Width,
                Height = requestedDimensions.Height
            };

            var blobFactory = ServiceLocator.Current.GetInstance<IBlobFactory>();
            var blob = blobFactory.GetBlob(CreateResizedBlobUri(originalBlob, name));
            using (var stream = blob.OpenWrite())
            {
                ImageBuilder.Current.Build(originalBlob.OpenRead(), stream, resizeSettings);
            }

            return blob;
        }

        public static Uri CreateResizedBlobUri(Blob originalBlob, string name)
        {
            var uriString = $"{Blob.GetContainerIdentifier(originalBlob.ID)}{Path.GetFileNameWithoutExtension(originalBlob.ID.LocalPath)}_{name}.png";
            return new Uri(uriString);
        }
    }
}
