using System;

namespace BananaForScale
{
	class MainClass
	{
		private const string PATH = "/home/hugo/ImageTest/maps";
		private const int N_STEPS = 30;

		public static void Main (string[] args)
		{
			//Random r = new Random();
			int size = 100;
			/*double[,] tempMap = new double[size, size];
			for (int i = 0; i < size; i++) {
				for (int j = 0; j < size; j++) {
					tempMap [i, j] = r.NextDouble () * 30.0;
				}
			}*/
			HotSpot spot;
			spot.x = 5;
			spot.y = 5;
			spot.T = 30;
			double[,] tempMap = MapGenerator.Generate (size, size, spot);

			RobotAI ai = new NeighAI (size, size);
			RunAndDraw (tempMap, ai, "/a");

			ai = new PredictBot (size, size);
			RunAndDraw (tempMap, ai, "/b");
		}

		public static void RunAndDraw(double[,] tempMap, RobotAI ai, string filename)
		{
			StateDrawer drawer = new StateDrawer (PATH + filename, tempMap);
			int x = ai.GetX();
			int y = ai.GetY();
			drawer.DrawWithPos (x, y);
			for (int i = 0; i < N_STEPS; i++) {
				int oldX = x;
				int oldY = y;

				ai.MakeMove(tempMap[x,y]);
				x = ai.GetX();
				y = ai.GetY();

				drawer.MoveRobot (oldX, oldY, x, y);
				drawer.Save ();
			}
		}
	}
}
