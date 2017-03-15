using EPiServer.Framework.Blobs;

namespace Chief2moro.ImageDataExtensions
{
    /// <summary>
    /// Provides an interface for creating modified blobs
    /// </summary>
    public interface IResizeBlobProcessor
    {
        Blob CreateImageBlob(Blob originalBlob, Dimensions requestedDimensions, string name);
    }
}
