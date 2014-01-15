using Chief2moro.ImageDataExtensions;
using NUnit.Framework;

namespace Chief2moro.ImageData.Extensions.Test
{
	[TestFixture]
	public class ImageOperatorTests
	{
		[Test]
		public void Resize_Should_Resize_Width_To_Expected()
		{
			var expectedWidth = 220;
			var actualDimensions = ImageResizeUtility.ResizeWidthMaintainAspectRatio(new Dimensions(300, 122), 220);
			Assert.AreEqual(expectedWidth, actualDimensions.Width);
		}

		[Test]
		public void Resize_Should_Maintain_AspectRatio_After_Width_Resize()
		{
			var expectedWidth = 220;
			var originalHeight = 299;
			var originalWidth = 214;

			var expectedAspectRatio = (float) originalWidth/(float) originalHeight;
            var actualDimensions = ImageResizeUtility.ResizeWidthMaintainAspectRatio(new Dimensions(originalWidth, originalHeight), expectedWidth);

			Assert.AreEqual(expectedAspectRatio, actualDimensions.AspectRatio);
		}
	
		[Test]
		public void Resize_Should_Resize_Height_To_Expected()
		{
			var expectedHeight = 700;
            var actualDimensions = ImageResizeUtility.ResizeHeightMaintainAspectRatio(new Dimensions(350, 2100), expectedHeight);
			Assert.AreEqual(expectedHeight, actualDimensions.Height);
		}

		[Test]
		public void Resize_Should_Maintain_AspectRatio_After_Height_Resize()
		{
			var expectedHeight = 700;
			var originalHeight = 1400;
			var originalWidth = 688;

			var expectedAspectRatio = (float)originalWidth / (float)originalHeight;
            var actualDimensions = ImageResizeUtility.ResizeHeightMaintainAspectRatio(new Dimensions(originalWidth, originalHeight), expectedHeight);

			Assert.AreEqual(expectedAspectRatio, actualDimensions.AspectRatio);
		}

		[Test]
		public void Resize_Should_Resize_Image_Height_According_To_Target_AspectRatio()
		{
			const int expectedHeight = 147;
			var originalDimensions = new Dimensions(800, 150);
			var targetDimensions = new Dimensions(220, expectedHeight);

            var actualDimensions = ImageResizeUtility.ResizeToTargetAspectRatio(originalDimensions, targetDimensions);

			Assert.AreEqual(actualDimensions.Height, expectedHeight);
		}

		[Test]
		public void Resize_Should_Resize_Image_Width_According_To_Target_AspectRatio()
		{
			const int expectedWidth = 200;
			var originalDimensions = new Dimensions(1000, 400);
			var targetDimensions = new Dimensions(expectedWidth, 100);

            var actualDimensions = ImageResizeUtility.ResizeToTargetAspectRatio(originalDimensions, targetDimensions);

			Assert.AreEqual(actualDimensions.Height, targetDimensions.Height);
			Assert.Greater(actualDimensions.Width, expectedWidth);
		}	

		[Test]
		public void Crop_Should_Start_Crop_At_Zero_If_Original_Image_Is_Smaller()
		{
			const int originalLength = 100;
			const int targetLength = 99;
			var expectedCropCoordinate = 0;

            var actualCropCoordinate = ImageResizeUtility.ReturnCentredCropCoordinate(originalLength, targetLength);
			Assert.AreEqual(expectedCropCoordinate, actualCropCoordinate);
		}

		[Test]
		public void Crop_Should_Start_Crop_At_NonZero_If_Original_Image_Is_Bigger()
		{
			const int originalLength = 300;
			const int targetLength = 220;

            var actualCropCoordinate = ImageResizeUtility.ReturnCentredCropCoordinate(originalLength, targetLength);
			Assert.Greater(actualCropCoordinate, 0);
		}

	}
}
