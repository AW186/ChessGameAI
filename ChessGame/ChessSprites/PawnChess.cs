using System;
namespace ChessGame
{
	public class PawnChess: ChessModel
	{
        public PawnChess() : base()
        {

        }
        private bool firstMove = true;
        public PawnChess(PawnChess chess) : base(chess.Position, chess.Side)
        {
            this.firstMove = chess.firstMove;
        }
        public PawnChess(Point point, Player player) : base(point, player)
        {

        }

        public override ChessType Type => ChessType.Pawn;
        public override bool moveTo(Point point, ChessModel[,] board)
        {
            if (this.Position.X == 0 || this.Position.X == 7)
            {
                return !(firstMove = !promoteMoveTo(point, board));
            }
            return !(firstMove = !base.moveTo(point, board));
        }
        private bool promoteMoveTo(Point point, ChessModel[,] board)
        {
            ChessModel queen = new QueenChess(this.Position, this.Side);
            ChessModel knight = new KnightChess(this.Position, this.Side);
            if (queen.moveTo(point, board))
            {
                return true;
            }
            else if (knight.moveTo(point, board))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override List<Point> PossibleMoves(ChessModel[,] board)
        {
            
            List<Point> res = new List<Point>();
            int dir = -(int)this.Side;
            int newX = this.Position.X + dir;
            if (newX >= 0 && newX < 8 && board[newX, Position.Y] != null)
            {
                return res;
            }
            if (this.firstMove && board[newX + dir, Position.Y] == null)
            {
                res.Add(new Point(newX + dir, Position.Y));
            }
            for (int y = -1; y <= 1; y++)
            {
                int newY = Position.Y + y;
                if (newY < 0 || newY >= 8)
                {
                    continue;
                }
                if (y == 0 || (board[newX, newY] != null && board[newX, newY].Side != this.Side))
                {
                    res.Add(new Point(newX, newY));
                }
            }
            return res;
        }
    }
}

