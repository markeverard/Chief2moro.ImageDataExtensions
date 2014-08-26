using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;

namespace Chief2moro.ImageDataExtensions
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class RequiredImageSizeRangeAttribute : ValidationAttribute
    {
        public int MinimumHeight { get; set; }
        public int MaximumHeight { get; set; }
        public int MinimumWidth { get; set; }
        public int MaximumWidth { get; set; }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessage, name);
        }

        public override bool IsValid(object value)
        {
            if (MinimumHeight < 1 && MaximumHeight < 1
                && MinimumWidth < 1 && MaximumWidth < 1)
            {
                ErrorMessage = "The allowed image height or width limits must be specified within the validation attribute";
                return false;
            }

            var contentReference = value as ContentReference;
            if (contentReference == null)
            {
                ErrorMessage = "RequiredImageSize attribute should only be applied to ContentReference properties";
                return false;
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

            if (!IsHeightWithinRange(dimensions) || !IsWidthWithinRange(dimensions))
            {
                ErrorMessage = "The image used in reference '{0}' must have the following dimensions:" + GetRequiredSizes();
                return false;
            }

            return true;
        }

        private bool IsHeightWithinRange(Dimensions dimensions)
        {
            return IsSizeWithinRange(dimensions.Height, MinimumHeight, MaximumHeight);
        }

        private bool IsWidthWithinRange(Dimensions dimensions)
        {
            return IsSizeWithinRange(dimensions.Width, MinimumWidth, MaximumWidth);
        }

        private bool IsSizeWithinRange(int actualSize, int minimumSize, int maximumSize)
        {
            return (minimumSize <= 0 || minimumSize <= actualSize)
                && (maximumSize <= 0 || maximumSize >= actualSize);
        }

        private string GetRequiredSizes()
        {
            var sb = new StringBuilder();

            if (MinimumHeight > 0) { sb.AppendFormat(" minimum height {0} pixels;", MinimumHeight); }
            if (MaximumHeight > 0) { sb.AppendFormat(" maximum height {0} pixels;", MaximumHeight); }
            if (MinimumWidth > 0) { sb.AppendFormat(" minimum width {0} pixels;", MinimumWidth); }
            if (MaximumWidth > 0) { sb.AppendFormat(" maximum width {0} pixels;", MaximumWidth); }

            return sb.ToString();
        }
    }
}