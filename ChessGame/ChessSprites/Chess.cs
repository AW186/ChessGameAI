using System;
using AWFrameWork;
namespace ChessGame
{
	public enum Player
	{
		Black = -1,
		White = 1
	}
	public enum ChessType
	{
        King = 1,
        Queen = 2,
        Rook = 3,
		Bishop = 4,
		Knight = 5,
		Pawn = 6

	}
	public class Chess: AWSprite
	{
		private Point offset = new Point(20, 20);
		private int blockSize = 55;
        private int chessSize = 55;
		private ChessModel model;
		public ChessModel Model => model;
		public Chess(ChessModel model)
		{
			int x = ((int)model.Type - 1) * 150 + 10;
			int y = 95 + ((int)model.Side) * 64;
			inputFrame = new Rectangle(x, y, 100, 100);
			this.model = model;
		}
        public override Rectangle Frame
		{
			get
			{
				return new Rectangle(
                    model.Position.Y * blockSize + (blockSize - chessSize) / 2 + offset.Y,
                    model.Position.X * blockSize + (blockSize - chessSize) / 2 + offset.X,
					chessSize,
					chessSize);
			}
		}

        public override void Load()
        {
			graphics = Game1.Content.Load<Texture2D>("Chess");
        }
    }
}

