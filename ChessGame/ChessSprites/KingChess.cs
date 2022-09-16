using System;
using System.Collections.Generic;

namespace ChessGame
{
	public class KingChess: ChessModel
	{
        public KingChess() : base()
        {

        }
        public KingChess(Point point, Player player) : base(point, player)
		{

		}

        public override ChessType Type => ChessType.King;

        public override List<Point> PossibleMoves(ChessModel[,] board)
        {
            List<Point> res = new List<Point>();
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 0; y <= 2; y++)
                {
                    res.Add(new Point(
                        this.Position.X - 1 + x,
                        this.Position.Y - 1 + y));
                }
            }
            return res;
        }
    }
}

