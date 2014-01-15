namespace Chief2moro.ImageDataExtensions
{
	public static class ImageResizeUtility
	{
		public static Dimensions ResizeHeightMaintainAspectRatio(Dimensions originalDimensions, int maximumHeight)
		{
			var resizedHeight = originalDimensions.Height > maximumHeight
								? maximumHeight
								: originalDimensions.Height;

			var resizedWidth = ((float)resizedHeight) / ((float)originalDimensions.Height);
			resizedWidth = resizedWidth * originalDimensions.Width;

			return new Dimensions((int)resizedWidth, resizedHeight);
		}

		public static Dimensions ResizeWidthMaintainAspectRatio(Dimensions originalDimensions, int maximumWidth)
		{
			int resizedWidth = originalDimensions.Width > maximumWidth 
								? maximumWidth
								: originalDimensions.Width;

			float resizedHeight = ((float)resizedWidth) / ((float)originalDimensions.Width);
			resizedHeight = resizedHeight * originalDimensions.Height;

			return new Dimensions(resizedWidth, (int)resizedHeight);
		}

		public static Dimensions ResizeToTargetAspectRatio(Dimensions originalDimensions, Dimensions targetDimensions)
		{
			return originalDimensions.AspectRatio > targetDimensions.AspectRatio
			       	? ResizeHeightMaintainAspectRatio(originalDimensions, targetDimensions.Height)
			       	: ResizeWidthMaintainAspectRatio(originalDimensions, targetDimensions.Width);
		}

		public static int ReturnCentredCropCoordinate(int originalLength, int targetLength)
		{
			return targetLength >= originalLength 
				? 0 
				: (originalLength - targetLength)/2;
		}
	}
}