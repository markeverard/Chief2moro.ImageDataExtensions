namespace Chief2moro.ImageDataExtensions
{
	/// <summary>
	/// A class describing an image's properties
	/// </summary>
	public class Dimensions
	{
		public Dimensions(int width, int height)
		{
			Width = width;
			Height = height;
		}

		public int Width { get; set; }
		public int Height { get; set; }
		public float AspectRatio { get { return (float)Width / (float)Height; } }

		public Orientation Orientation
		{
			get
			{
				if (Width == Height) return Orientation.Square;
				return Width > Height ? Orientation.Landscape : Orientation.Portrait;
			}
		}

	    public static Dimensions Default { get { return new Dimensions(1, 1); } }
	}
}
