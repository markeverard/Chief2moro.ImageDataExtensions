using EPiServer.Core.Internal;
using EPiServer.DataAnnotations;
using EPiServer.Framework.Blobs;
using EPiServer.ServiceLocation;

namespace Chief2moro.ImageDataExtensions
{
    internal class ThumbnailManagerProcessor : IResizeBlobProcessor
    {
        public Blob CreateImageBlob(Blob originalBlob, Dimensions requestedDimensions, string name)
        {
            var imageDescriptor = new ImageDescriptorAttribute(requestedDimensions.Height, requestedDimensions.Width);
            var thumbnailManager = ServiceLocator.Current.GetInstance<ThumbnailManager>();

            return thumbnailManager.CreateImageBlob(originalBlob, name, imageDescriptor);
        }
    }
}
