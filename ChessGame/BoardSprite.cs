using System;
using AWFrameWork;
namespace ChessGame
{
	public class BoardSprite: AWSprite
	{
		public BoardSprite()
		{
			
		}

        public override void Load()
        {
			graphics = Game1.Content.Load<Texture2D>("ChessBoard");
			inputFrame = new Rectangle(0, 0, 600, 600);
            Frame = new Rectangle(20, 20, 440, 440);
        }
    }
}

