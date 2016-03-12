using System;

namespace BananaForScale
{
	public class RobotAI
	{
		public enum Direction {
			UP, DOWN, LEFT, RIGHT, NONE
		}

		private const int MAP_SIZE = 100;
		private const int NO_TEMP = 0;

		private int xPos;
		private int yPos;
		private AIWorld world;

		public RobotAI ()
		{
			world = new AIWorld(MAP_SIZE, MAP_SIZE);
			xPos = MAP_SIZE / 2;
			yPos = MAP_SIZE / 2;
		}

		public Direction MakeMove(int currentTemp) 
		{
			world.AddMesure (xPos, yPos, currentTemp);
			//const int offset = 1;
			//int[][] neighbours = world.GetNeighboringRegion (xPos, yPos, offset);

			// compute target next position
			int xTotMove = 0;
			int yTotMove = 0;

			for (int i = -1; i <= 1; i++) {
				for (int j = -1; j <= 1; j++) {
					int t = world.GetAt (xPos + i, yPos + j);

					if (t != NO_TEMP) {
						int dt = currentTemp - t;

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

			Direction dir = DirectionForMove (xPos, yPos, xTotMove, yTotMove);
			MoveInDirection (dir);
			return dir;
		}

		private Direction DirectionForMove(int xOld, int yOld, int xNew, int yNew) 
		{
			int dx = xOld - xNew;
			Direction xDir;

			if (dx == 0) {
				xDir = Direction.NONE;
			} else if (dx < 0) {
				xDir = Direction.UP;
			} else {
				xDir = Direction.DOWN;
			}

			int dy = yOld - yNew;
			Direction yDir;

			if (dy == 0) {
				yDir = Direction.NONE;
			} else if (dy < 0) {
				yDir = Direction.LEFT;
			} else {
				yDir = Direction.RIGHT;
			}

			if (Math.Abs (dx) > Math.Abs (dy))
				return xDir;
			else
				return yDir;
		}

		private void MoveInDirection(Direction d)
		{
			switch (d)
			{
			case Direction.UP:
				xPos -= 1;
				break;
			case Direction.DOWN:
				xPos += 1;
				break;
			case Direction.LEFT:
				yPos -= 1;
				break;
			case Direction.RIGHT:
				yPos += 1;
				break;
			}
		}
	}
}

