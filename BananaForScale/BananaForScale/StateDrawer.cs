using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace BananaForScale
{
	public class StateDrawer
	{
		private const int stretchFactor = 7;

		private string pathPrefix;
		private double[,] tempMap;
		private ZoomedBitmap bmp;
		private int fileCount = 0;

		public StateDrawer (string pathPrefix, double[,] realTempMap)
		{
			this.pathPrefix = pathPrefix;
			this.tempMap = realTempMap;

			int width = realTempMap.GetLength(0);
			int height = realTempMap.GetLength (1);
			bmp = new ZoomedBitmap (width, height, stretchFactor, stretchFactor);

			for (int i = 0; i < width; i++) {
				for (int j = 0; j < height; j++) {
					bmp.SetPixel (i, j, ColorForTemp(tempMap[i,j]));
				}
			}
		}

		private Color ColorForTemp(double temp)
		{
			temp = Math.Min (temp, 30);
			int a;
			int b;
			int c;

			if (temp <= 15) {
				double r = temp / 15;
				a = (int)((1-r) * 255);
				b = (int)(r * 255);
				c = 0;
			} else {
				double r = (temp - 15) / 15;
				a = 0;
				b = (int)((1-r) * 255);
				c = (int)(r * 255);
			}
			return Color.FromArgb(c,b,a);
			//return Color.FromArgb ((int)(temp * Math.Pow(2, 19)));
		}

		public void MoveRobot(int oldX, int oldY, int newX, int newY)
		{
			bmp.SetPixel (oldX, oldY, Color.Black);
			bmp.SetPixel (newX, newY, Color.White);
		}

		public void DrawWithPos(int x, int y)
		{
			Color oldColor = bmp.GetPixel (x, y);
			bmp.SetPixel (x, y, Color.White);
			Save ();
			bmp.SetPixel (x, y, oldColor);
		}

		public void Save()
		{
			bmp.GetRealBitmap().Save (pathPrefix + fileCount.ToString() + ".bmp", ImageFormat.Bmp);
			fileCount += 1;
		}
	}
}

