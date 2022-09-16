using System;
namespace ChessGame
{
	public class RookChess: ChessModel
    {
        static int[,] directions = new int[4, 2] { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };

        public RookChess() : base()
        {

        }
        public RookChess(Point point, Player player) : base(point, player)
        {

        }

        public override ChessType Type => ChessType.Rook;

        public override List<Point> PossibleMoves(ChessModel[,] board)
        {
            List<Point> res = new List<Point>();
            for (int i = 0; i < 4; i++)
            {
                Point dir = new Point(directions[i, 0], directions[i, 1]);
                res.AddRange(search(new Point(Position.X + dir.X, Position.Y + dir.Y), dir, board));
            }
            return res;
        }
    }
}

