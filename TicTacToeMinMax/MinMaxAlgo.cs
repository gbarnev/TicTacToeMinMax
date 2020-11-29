using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeMinMax
{
    public class MinMaxAlgo
    {
        public const char EmptySymbol = '_';
        public const char RobotTurnSymbol = 'O';
        public const char PersonTurnSymbol = 'X';

        public void NextMove(char[,] board)
        {
            int bestRow = -1;
            int bestCol = -1;
            var bestScore = int.MinValue;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] != EmptySymbol)
                    {
                        continue;
                    }
                    board[row, col] = RobotTurnSymbol;
                    var curScore = this.MinScore(board, int.MinValue, int.MaxValue, 1);
                    board[row, col] = EmptySymbol;
                    if (bestScore < curScore)
                    {
                        bestRow = row;
                        bestCol = col;
                        bestScore = curScore;
                    }
                }
            }
            board[bestRow, bestCol] = RobotTurnSymbol;
        }

        private int MaxScore(char[,] board, int alpha, int beta, int depth)
        {
            (var hasEnded, var score) = this.HasGameEnded(board);
            if (hasEnded)
            {
                return this.ModifyScoreWithDepth(score.Value, depth);
            }

            var curMax = int.MinValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] != EmptySymbol)
                    {
                        continue;
                    }

                    board[i, j] = RobotTurnSymbol;
                    var min = this.MinScore(board, alpha, beta, depth + 1);
                    board[i, j] = EmptySymbol;
                    curMax = Math.Max(min, curMax);
                    alpha = Math.Max(min, alpha);
                    if (alpha >= beta)
                    {
                        return curMax;
                    }
                }
            }
            return curMax;
        }

        private int MinScore(char[,] board, int alpha, int beta, int depth)
        {
            (var hasEnded, var score) = this.HasGameEnded(board);
            if (hasEnded)
            {
                return this.ModifyScoreWithDepth(score.Value, depth);
            }

            var curMin = int.MaxValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] != EmptySymbol)
                    {
                        continue;
                    }

                    board[i, j] = PersonTurnSymbol;
                    var max = this.MaxScore(board, alpha, beta, depth + 1);
                    board[i, j] = EmptySymbol;
                    curMin = Math.Min(max, curMin);
                    beta = Math.Min(max, beta);
                    if (alpha >= beta)
                    {
                        return curMin;
                    }
                }
            }
            return curMin;
        }

        public (bool HasEnded, int? Score) HasGameEnded(char[,] board)
        {
            var score = CalculateScore(board);
            if (score != 0)
            {
                return (true, score);
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == EmptySymbol)
                    {
                        return (false, null);
                    }
                }
            }
            return (true, score);
        }

        private int CalculateScore(char[,] board)
        {
            //right diagonal
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != EmptySymbol)
            {
                return board[0, 0] == RobotTurnSymbol ? 10 : -10;
            }

            //left diagonal
            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[2, 0] != EmptySymbol)
            {
                return board[0, 2] == RobotTurnSymbol ? 10 : -10;
            }

            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != EmptySymbol)
                {
                    return board[i, 0] == RobotTurnSymbol ? 10 : -10;
                }
                if (board[0, i] == board[1, i] && board[1, i] == board[2, i] && board[0, i] != EmptySymbol)
                {
                    return board[0, i] == RobotTurnSymbol ? 10 : -10;
                }
            }
            return 0;
        }

        private int ModifyScoreWithDepth(int score, int depth)
        {
            if (score == 0)
            {
                return score;
            }
            else if (score > 0)
            {
                return score - depth;
            }
            else
            {
                return score + depth;
            }
        }
    }
}
