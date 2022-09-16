using System;
using Microsoft.Xna.Framework.Graphics;
using static ChessGame.ChessAI;

namespace ChessGame
{
	public class ChessAI
	{
		static int[] scoreBoard = new int[] { 9000, 900, 500, 300, 300, 100 };
        public class Node
        {
            public ChessModel[,] model;
            public int score;
            public Node (ChessModel[,] m, int s)
            {
                model = m;
                score = s;
            }
        }
		public ChessAI()
		{
		}
		public int minimaxSearch(ChessModel[,] model, int alpha, int beta)
        {
            return minSearch(model, alpha, beta, 0);
        }
        private int minSearch(ChessModel[,] model, int alpha, int beta, int depth)
        {
            if (depth >= 6)
            {
                return evaluate(model, Player.Black);
            }
            ChessModel[,] bestCase = new ChessModel[8,8];
            List<Node> nodes = new List<Node>();
            int min = 100000;
            for (int x = 7; x >= 0; x--)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (model[x, y] != null && model[x, y].Side == Player.Black)
                    {
                        foreach (Point p in model[x, y].AvailableMoves(model))
                        {
                            ChessModel[,] node = new ChessModel[8, 8];
                            modelCopy(node, model);
                            node[x, y].moveTo(p, node);
                            nodes.Add(new Node(node, predictScore(node, Player.White)));
                        }
                    }
                }
            }
            nodes.Sort((a, b) => a.score - b.score);
            for (int i = 0; i < (nodes.Count > 20 ? 20 : nodes.Count); i++)
            {
                ChessModel[,] rnode = new ChessModel[8, 8];
                modelCopy(rnode, nodes[i].model);
                int score = maxSearch(rnode, alpha, beta, depth + 1);
                if (score < min)
                {
                    bestCase = nodes[i].model;
                    min = score;
                    beta = min < beta ? min : beta;
                }
                if (min <= alpha)
                {
                    modelCopy(model, bestCase);
                    return min;
                }
            }
            modelCopy(model, bestCase);
            return min;
        }
        private int maxSearch(ChessModel[,] model, int alpha, int beta, int depth)
        {
            if (depth >= 6)
            {
                return evaluate(model, Player.White);
            }
            ChessModel[,] bestCase = new ChessModel[8, 8];
            int max = -100000;
            List<Node> nodes = new List<Node>();
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (model[x, y] != null && model[x, y].Side == Player.White)
                    {
                        foreach (Point p in model[x, y].AvailableMoves(model))
                        {
                            ChessModel[,] node = new ChessModel[8, 8];
                            modelCopy(node, model);
                            node[x, y].moveTo(p, node);
                            nodes.Add(new Node(node, predictScore(node, Player.Black)));
                        }
                    }
                }
            }
            nodes.Sort((a, b) => a.score - b.score);
            for (int i = 0; i < 12; i++)
            {
                ChessModel[,] rnode = new ChessModel[8, 8];
                modelCopy(rnode, nodes[i].model);
                int score = minSearch(rnode, alpha, beta, depth + 1);
                if (score > max)
                {
                    bestCase = nodes[i].model;
                    max = score;
                    alpha = max > alpha ? max : alpha;
                }
                if (max >= beta)
                {
                    modelCopy(model, bestCase);
                    return max;
                }
            }
            modelCopy(model, bestCase);
            return max;
        }
        private void modelCopy(ChessModel[,] dst, ChessModel[,] src)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    ChessModel model;
                    if ((model = src[x, y]) != null)
                    {
                        dst[x, y] = ChessModel.creat(model);
                        
                    } else
                    {
                        dst[x, y] = null;
                    }
                }
            }
        }
        private int predictScore(ChessModel[,] model, Player side)
        {
            int score = 0;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    score += model[x, y] != null ?
                        model[x, y].motionScore(model, side) : 0;
                    score += model[x, y] != null ?
                        (scoreBoard[(int)model[x, y].Type - 1] * (int)model[x, y].Side) : 0;
                }
            }
            return score;
        }
        private int evaluate(ChessModel[,] model, Player side)
		{
			int score = 0;
			for (int x = 0; x < 8; x++)
			{
                for (int y = 0; y < 8; y++)
                {
                    score += model[x, y] != null ?
                        model[x, y].motionScore(model, side) : 0;
                    score += model[x, y] != null ?
						(scoreBoard[(int)model[x, y].Type - 1] * (int)model[x, y].Side) : 0;
                }
            }
			return score;
		}
	}
}

