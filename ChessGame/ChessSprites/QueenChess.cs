using System;

namespace ChessGame
{
	public class QueenChess: ChessModel
    {
        public QueenChess() : base()
        {

        }

        public QueenChess(Point point, Player player) : base(point, player)
        {

        }

        public override ChessType Type => ChessType.Queen;

        
        public override List<Point> PossibleMoves(ChessModel[,] board)
        {
            List<Point> res = new List<Point>();
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 0; y <= 2; y++)
                {
                    res.AddRange(search(new Point(this.Position.X - 1 + x,
                        this.Position.Y - 1 + y), new Point(x - 1, y - 1), board));
                }
            }
            return res;
        }
    }
}

