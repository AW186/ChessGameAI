using System;
namespace ChessGame
{
	public class BishopChess: ChessModel
	{
        public BishopChess() : base()
        {

        }
        public BishopChess(Point point, Player player) : base(point, player)
        {

        }

        public override ChessType Type => ChessType.Bishop;

        public override List<Point> PossibleMoves(ChessModel[,] board)
        {
            List<Point> res = new List<Point>();
            int[,] directions = new int[4, 2] { { 1, 1 }, { -1, -1 }, { -1, 1 }, { 1, -1 } };
            for (int i = 0; i < 4; i++)
            {
                Point dir = new Point(directions[i, 0], directions[i, 1]);
                res.AddRange(search(new Point(Position.X + dir.X, Position.Y + dir.Y), dir, board));
            }
            return res;
        }
    }
}

