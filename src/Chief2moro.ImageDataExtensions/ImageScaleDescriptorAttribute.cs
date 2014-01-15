using System;
using System.Diagnostics.Contracts;

namespace Chief2moro.ImageDataExtensions
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ImageScaleDescriptorAttribute : Attribute
    {
        public ImageScaleDescriptorAttribute(short percent)
        {
            Contract.Requires(percent > 0);
            Percent = percent;
        }

        public ImageScaleDescriptorAttribute() : this(100)
        {

        }

        public short Percent { get; set; }
        public double DimensionMultiplier { get { return ((float)Percent / 100); } }
    }
}