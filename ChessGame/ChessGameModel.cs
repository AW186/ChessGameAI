using System;
namespace ChessGame
{
	public class ChessGameModel
	{
		public Player turn = Player.White;
		private ChessModel[,] board = new ChessModel[8, 8];
		public ChessModel[,] Board => board;
        private Point choosenChess = new Point(-1, -1);
		public Player Turn => turn;
        public ChessGameModel()
		{
			board = new ChessModel[8, 8]
			{ {new RookChess(), new KnightChess(), new BishopChess(), new QueenChess(),
					new KingChess(), new BishopChess(), new KnightChess(), new RookChess() },
			{ new PawnChess(), new PawnChess(), new PawnChess(), new PawnChess(),
					new PawnChess(), new PawnChess(), new PawnChess(), new PawnChess() },
			{ null, null, null, null, null, null, null, null },
			{ null, null, null, null, null, null, null, null },
			{ null, null, null, null, null, null, null, null },
			{ null, null, null, null, null, null, null, null },
			{ new PawnChess(), new PawnChess(), new PawnChess(), new PawnChess(),
					new PawnChess(), new PawnChess(), new PawnChess(), new PawnChess() },
			{new RookChess(), new KnightChess(), new BishopChess(), new QueenChess(),
					new KingChess(), new BishopChess(), new KnightChess(), new RookChess() } };
			for (int x = 0; x < 2; x++)
			{
                for (int y = 0; y < 8; y++)
                {
					board[x, y].Side = Player.Black;
					board[x, y].Position = new Point(x, y);
                }
            }
            for (int x = 6; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    board[x, y].Side = Player.White;
                    board[x, y].Position = new Point(x, y);
                }
            }

        }
		public bool ChooseChess(Point point)
		{
			ChessModel chess = board[point.X, point.Y];

            if (chess != null && chess.Side == turn)
			{
				choosenChess = point;
				return true;
			}
			return false;
		}
		public bool MoveTo(Point point)
		{
			if (choosenChess.X < 0 || choosenChess.Y >= 8)
			{
				return false;
			}
			if (board[choosenChess.X, choosenChess.Y].moveTo(point, board)) {
				turn = (Player)(-(int)turn);
				choosenChess = new Point(-1, -1);

				return true;
			}
			return false;
		}
		
	}
}

