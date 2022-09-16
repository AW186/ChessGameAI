using System;

namespace ChessGame
{
	public class KnightChess: ChessModel
	{
        public static int[,] directions = new int[8, 2] {
                { 1, 2 }, { -1, -2 }, { -1, 2 }, { 1, -2 },
                { 2, 1 }, { -2, -1 }, { -2, 1 }, { 2, -1 } };
        public KnightChess() : base()
        {

        }
        public KnightChess(Point point, Player player) : base(point, player)
        {

        }

        public override ChessType Type => ChessType.Knight;

        public override List<Point> PossibleMoves(ChessModel[,] board)
        {
            List<Point> res = new List<Point>();
            
            for (int i = 0; i < 8; i++)
            {
                res.Add(new Point(directions[i, 0] + Position.X, directions[i, 1] + Position.Y));
            }
            return res;
        }
    }
}

