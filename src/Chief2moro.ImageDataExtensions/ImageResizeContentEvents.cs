using System;
using System.Reflection;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.Blobs;
using EPiServer.ServiceLocation;

namespace Chief2moro.ImageDataExtensions
{
    public class ImageResizeContentEvents
    {
        public void contentEvents_PublishingContent(object sender, ContentEventArgs e)
        {
            var image = e.Content as ImageData;

            if (image == null) 
                return;
            
            PropertyInfo[] properties = image.GetType().GetProperties();
            
            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.PropertyType != typeof(Blob))
                    continue;

                //get attribute name
                var imageWidthAttribute = (ImageWidthDescriptorAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(ImageWidthDescriptorAttribute));
                var imageScaleAttribute = (ImageScaleDescriptorAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(ImageScaleDescriptorAttribute));

                if (imageWidthAttribute == null && imageScaleAttribute == null)
                    continue;

                var imageDimensions = ImageBlobUtility.GetDimensions(image.BinaryData);

                var requiredWidth = imageWidthAttribute != null ? 
                    imageWidthAttribute.Width :
                    (int) (imageDimensions.Width * imageScaleAttribute.DimensionMultiplier);
                
                var calculatedDimensions = ImageResizeUtility.ResizeWidthMaintainAspectRatio(imageDimensions, requiredWidth);  
                var imageDescriptor = new ImageDescriptorAttribute(calculatedDimensions.Height, calculatedDimensions.Width);

                var thumbnailManager = ServiceLocator.Current.GetInstance<ThumbnailManager>();
                var resizedBlob = thumbnailManager.CreateImageBlob(image.BinaryData, propertyInfo.Name, imageDescriptor);

                //assign blob to current property
                propertyInfo.SetValue(image, resizedBlob, null);
            }
        }
    }
}
