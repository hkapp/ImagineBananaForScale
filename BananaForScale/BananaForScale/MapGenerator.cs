using System;

namespace BananaForScale
{
	public struct HotSpot {
		public int x;
		public int y;
		public double T;
	}

	public class MapGenerator
	{
		private const double avgTmp = 15;

		public static double[,] Generate (int width, int height, HotSpot spot)
		{
			double[,] map = new double[width, height];
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					double dist = Math.Sqrt (Math.Pow (x - spot.x, 2) + Math.Pow (y - spot.y, 2));
					dist = Math.Max (dist, 0.001);
					double dt = (spot.T - avgTmp) / dist;
					map [x, y] = avgTmp + dt;
				}
			}
			return map;
		}
	}
}

