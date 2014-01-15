using System;
using System.Data;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;

namespace Chief2moro.ImageDataExtensions
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class RequiredImageSizeAttribute : ValidationAttribute
    {
        public RequiredImageSizeAttribute()
        {
            ErrorMessage = "The image used in reference '{0}' must have a size of ({1},{2}) pixels!";
        }

        public int Width { get; set; }
        public int Height { get; set; }
        
        public override string FormatErrorMessage(string name)
        {
            if (Height < 1)
                return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Width, "y");

            if (Width < 1)
                return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, "x", Height);

            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Width, Height);
        }

        public override bool IsValid(object value)
        {
            if (Height < 1 && Width < 1)
                throw new NoNullAllowedException("The allowed image height or image width must be specified within the validation attribute");

            var contentReference = value as ContentReference;
            if (contentReference == null)
                return true;

            var content = ServiceLocator.Current.GetInstance<IContentLoader>().Get<ContentData>(contentReference);
            if (content == null)
                return true;

            var imageData = content as ImageData;
            if (imageData == null)
                return true;

            var dimensions = ImageBlobUtility.GetDimensions(imageData.BinaryData);

            if (IsNonZero(Height) && Height != dimensions.Height)
                return false;

            if (IsNonZero(Width) && Width != dimensions.Width)
                return false;

            return true;
        }

        private bool IsNonZero(int size)
        {
            return size > 0;
        }
    }
}