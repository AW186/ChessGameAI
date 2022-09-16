﻿using System;
using AWFrameWork;
namespace ChessGame
{
	public class ChessGameScene: AWScene
	{
		AWSprite board = new BoardSprite();
		ChessGameModel model = new ChessGameModel();
        List<Chess> chesses = new List<Chess>();
        ChessAI robot = new ChessAI();
		public ChessGameScene()
		{
		}
        public override void Load()
        {
            base.Load();
			this.AddSprite(board);
            updateChess();
        }
        private void updateChess()
        {
            foreach(Chess chess in chesses)
            {
                chess.RemoveFromScene();
            }
            chesses.Clear();
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (model.Board[x,y] != null)
                    {
                        Chess newChess = new Chess(model.Board[x, y]);
                        chesses.Add(newChess);
                        this.AddSprite(newChess);
                    }
                }
            }
        }
        public override void Update(GameTime time, KeyboardState kstate, MouseState mstate)
        {
            if (model.Turn == Player.Black)
            {
                robot.minimaxSearch(model.Board, -100000, 100000);
                model.turn = Player.White;
                updateChess();
            }
            base.Update(time, kstate, mstate);

        }
        public override void click(MouseState state)
        {
            base.click(state);
            int x = (state.Position.Y - 20) / 55;
            int y = (state.Position.X - 20) / 55;
            
            foreach (Chess chess in chesses)
            {
                chess.Tint = Color.White;
            }
            if (x < 0 || x >= 8 || y < 0 || y >= 8)
            {
                return;
            }
            if (model.MoveTo(new Point(x, y)))
            {
                if (winnerCheck() != 0)
                {
                    model = new ChessGameModel();
                }
                updateChess();
                return;
            }
            if (model.ChooseChess(new Point(x, y))) {
                foreach (Chess chess in chesses)
                {
                    if (chess.Model.Position.X == x && chess.Model.Position.Y == y)
                    {
                        chess.Tint = Color.Cyan;
                    }
                }
            }
        }
        int winnerCheck()
        {
            int res = 0;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    res += (model.Board[x, y] != null && model.Board[x, y].Type == ChessType.King) ? (int)model.Board[x, y].Side : 0;
                }
            }
            return res;
        }
    }
}
