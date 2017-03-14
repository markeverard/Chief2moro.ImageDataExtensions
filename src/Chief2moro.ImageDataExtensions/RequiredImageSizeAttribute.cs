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
            ErrorMessage = "The image used in reference '{0}' must have a size of ({1},{2}) pixels";
        }

        public int Width { get; set; }
        public int Height { get; set; }
        
        public override string FormatErrorMessage(string name)
        {
            if (Height < 1)
                return string.Format(CultureInfo.CurrentCulture, ErrorMessage, name, Width, "?");

            if (Width < 1)
                return string.Format(CultureInfo.CurrentCulture, ErrorMessage, name, "?", Height);

            return string.Format(CultureInfo.CurrentCulture, ErrorMessage, name, Width, Height);
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;
            
            if (Height < 1 && Width < 1)
            {
                ErrorMessage = "The allowed image height or image width must be specified within the validation attribute";
                return false;
            }
            var contentReference = value as ContentReference;
            if (contentReference == null)
            {
                ErrorMessage = "RequiredImageSize attribute should only be applied to ContentReference properties";
                return true;
            }
        
            var content = ServiceLocator.Current.GetInstance<IContentLoader>().Get<ContentData>(contentReference);
            if (content == null)
            {
                ErrorMessage = "The content selected for property '{0}' cannot be found!";
                return false;
            }

            var imageData = content as IContentImage;
            if (imageData == null)
            {
                ErrorMessage = "RequiredImageSize attribute applied to property '{0}' can only be applied to ContentReferences that inherit from IContentImage";
                return false;
            }
                
            var dimensions = ImageBlobUtility.GetDimensions(imageData.BinaryData);

            if (IsNotRequiredHeight(dimensions) && IsNotRequiredWidth(dimensions))
            {
                ErrorMessage = "The image used in reference '{0}' must have a size of ({1},{2}) pixels";
                return false;
            }


            if (IsNotRequiredHeight(dimensions))
            {
                ErrorMessage = "The image used in property '{0}' must have a height of {2} pixels";
                return false;
            }

            if (IsNotRequiredWidth(dimensions))
            {
                ErrorMessage = "The image used in property '{0}' must have a width of {1} pixels";
                return false;
            }

            return true;
        }

        private bool IsNotRequiredWidth(Dimensions dimensions)
        {
            return IsNonZero(Width) && Width != dimensions.Width;
        }

        private bool IsNotRequiredHeight(Dimensions dimensions)
        {
            return IsNonZero(Height) && Height != dimensions.Height;
        }

        private bool IsNonZero(int size)
        {
            return size > 0;
        }
    }
}