using System;
namespace ChessGame
{
	public abstract class ChessModel
	{
        public static ChessModel creat(ChessModel model)
        {

            switch (model.Type) {
                case ChessType.King:
                    return new KingChess(model.Position, model.Side);
                case ChessType.Queen:
                    return new QueenChess(model.Position, model.Side);
                case ChessType.Bishop:
                    return new BishopChess(model.Position, model.Side);
                case ChessType.Knight:
                    return new KnightChess(model.Position, model.Side);
                case ChessType.Rook:
                    return new RookChess(model.Position, model.Side);
                case ChessType.Pawn:
                    return new PawnChess((PawnChess)model);
            }
            return null;
        }
        protected List<Point> search(Point start, Point dir, ChessModel[,] board)
        {
            int x = start.X;
            int y = start.Y;
            if (x < 0 || x >= 8 || y < 0 || y >= 8 || board[x, y] != null)
            {
                List<Point> end = new List<Point>();
                end.Add(start);
                return end;
            }
            List<Point> res = search(new Point(x + dir.X, y + dir.Y), dir, board);
            res.Add(start);
            return res;
        }
        public virtual bool moveTo(Point point, ChessModel[,] board)
        {
            List<Point> available = this.AvailableMoves(board);
            if (available.Contains(point))
            {
                board[point.X, point.Y] = this;
                board[Position.X, Position.Y] = null;
                this.Position = point;
                return true;
            }
            return false;
        }
        static int[] motionBoard = new int[] { 9000, 450, 250, 150, 150, 50 };
        public int motionScore(ChessModel[,] board, Player side)
        {
            int res = 0;
            List<Point> possible = PossibleMoves(board);
            foreach (Point p in possible)
            {
                if (p.X < 0 || p.X >= 8 || p.Y < 0 || p.Y >= 8 || p == this.Position)
                {
                    continue;
                }
                if (board[p.X, p.Y] == null)
                {
                    res += (int)this.Side;
                } else if (this.Side == side && board[p.X, p.Y].Side != this.Side)
                {
                    res += (int)this.Side * motionBoard[(int)board[p.X, p.Y].Type - 1];
                }
            }
            return res;
        }
        public List<Point> AvailableMoves(ChessModel[,] board)
		{
            List<Point> res = new List<Point>();
            List<Point> possible = PossibleMoves(board);
            foreach (Point p in possible)
            {
                if (p.X < 0 || p.X >= 8 || p.Y < 0 || p.Y >= 8 || p == this.Position)
                {
                    continue;
                }
                if (board[p.X, p.Y] == null || board[p.X, p.Y].Side != this.Side)
                {
                    res.Add(p);
                }
            }
            return res;
        }
        public abstract List<Point> PossibleMoves(ChessModel[,] board);
        public abstract ChessType Type { get; }
		public Point Position = new Point(-1, -1);
		public Player Side = Player.White;
        public ChessModel()
		{

		}

        public ChessModel(Point point, Player player)
		{
			this.Position = point;
			this.Side = player;
		}
	}
}

