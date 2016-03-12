using System;

namespace BananaForScale
{
	public class NeighAI: RobotAI
	{

		private static readonly Random rand = new Random ();

		private double lastTemp;

		public NeighAI (int mapWidth, int mapHeight) :base(mapWidth, mapHeight)
		{
			lastTemp = 1000;
		}

		public override AIWorld.Direction GetMove()
		{
			double currentTemp = world.GetAt (xPos, yPos);
			if (currentTemp < lastTemp)
				return RandomDirection ();
			else
				return ComputeBestDir ();
		}

		private AIWorld.Direction ComputeBestDir()
		{
			double currentTemp = world.GetAt (xPos, yPos);

			int xTotMove = 0;
			int yTotMove = 0;

			for (int i = -1; i <= 1; i++) {
				for (int j = -1; j <= 1; j++) {
					double t = world.GetAt (xPos + i, yPos + j);

					if (t != AIWorld.NO_TEMP) {
						double dt = currentTemp - t;

						if (dt < 0) {
							// want to go in that direction
							xTotMove += i;
							yTotMove += j;
						}
						else {
							// want to avoid that direction
							xTotMove -= i;
							yTotMove -= j;
						}

					}
				}
			}

			return DirectionForMove (xPos, yPos, xTotMove, yTotMove);
		}

		private AIWorld.Direction RandomDirection()
		{
			switch (rand.Next (1, 4)) {
			case 1:
				return AIWorld.Direction.UP;
			case 2:
				return AIWorld.Direction.DOWN;
			case 3:
				return AIWorld.Direction.LEFT;
			case 4:
				Console.Write ("Got random right");
				return AIWorld.Direction.RIGHT;
			default:
				Console.Write ("Error: Random value not in range");
				return AIWorld.Direction.NONE;
			}
		}

	}
}

