using System;

namespace BananaForScale
{
	public class AIWorld
	{

		private int[,] tempMap;
		private readonly int width;
		private readonly int height;

		public AIWorld (int width, int height)
		{
			tempMap = new int[width,height];
			this.width = width;
			this.height = height;
		}

		public int[,] GetNeighboringRegion(int x, int y, int offset)
		{
			int retSize = 2 * offset + 1;
			int[,] neighs = new int[retSize,retSize];

			for (int i = 0; i <= offset; i++) {
				for (int j = 0; j <= offset; j++) {
					int xget = x + i - offset;
					int yget = y + j - offset;
					neighs [i,j] = tempMap [xget,yget];
				}
			}

			return neighs;
		}

		public void AddMesure(int x, int y, int temp)
		{
			if (ContainsPos (x, y)) {
				tempMap [x, y] = temp;
			}
		}

		public int GetAt(int x, int y)
		{
			if (ContainsPos(x,y)) {
				return tempMap [x, y];
			} else {
				return 0;
			}
		}

		public bool ContainsPos(int x, int y) 
		{
			return (x >= 0 || x < width || y >= 0 || y < height);
		}

	}
}

