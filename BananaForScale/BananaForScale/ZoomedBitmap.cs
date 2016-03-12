using System;
using System.Drawing;

namespace BananaForScale
{
	public class ZoomedBitmap
	{
		private readonly int xFactor;
		private readonly int yFactor;
		private Bitmap bmp;

		public ZoomedBitmap (int width, int height, int xFactor, int yFactor)
		{
			this.xFactor = xFactor;
			this.yFactor = yFactor;
			bmp = new Bitmap (width * xFactor, height * yFactor);
		}

		public Bitmap GetRealBitmap()
		{
			return bmp;
		}

		public void SetPixel(int x, int y, Color c)
		{
			int rx = x * xFactor;
			int ry = y * yFactor;
			for (int i = 0; i < xFactor; i++) {
				for (int j = 0; j < yFactor; j++) {
					bmp.SetPixel (rx + i, ry + j, c);
				}
			}
		}

		public Color GetPixel(int x, int y)
		{
			return bmp.GetPixel (x * xFactor, y * yFactor);
		}
	}
}

