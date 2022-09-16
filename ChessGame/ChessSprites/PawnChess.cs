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
        public List<Point> PromotePossibleMoves(ChessModel[,] board)
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
            for (int i = 0; i < 8; i++)
            {
                res.Add(new Point(KnightChess.directions[i, 0] + Position.X, KnightChess.directions[i, 1] + Position.Y));
            }
            return res;

        }
        public override List<Point> PossibleMoves(ChessModel[,] board)
        {
            if (this.Position.X == 0 || this.Position.X == 7)
            {
                return PromotePossibleMoves(board);
            }
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

