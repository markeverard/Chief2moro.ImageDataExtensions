using System;

namespace Chief2moro.ImageDataExtensions
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ImageWidthDescriptorAttribute : Attribute
    {
        public ImageWidthDescriptorAttribute(int width)
        {
            this.Width = width;
        }

        public ImageWidthDescriptorAttribute()
        {
            this.Width = 100;
        }

        public int Width { get; set; }
    }
}