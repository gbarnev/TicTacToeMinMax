using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace TicTacToeMinMax
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please, enter who is first(1 for robot, 0 for you):");
            var isRobotTurn = int.Parse(Console.ReadLine()) == 1;
            var board = new char[3, 3];
            InitBoard(board);
            var algo = new MinMaxAlgo();

            while (true)
            {
                var result = algo.HasGameEnded(board);
                if (result.HasEnded)
                {
                    if (result.Score.Value > 0)
                    {
                        Console.WriteLine("Robot won the game!");
                    }
                    else if (result.Score.Value == 0)
                    {
                        Console.WriteLine("Tie!");
                    }
                    else
                    {
                        Console.Write("Congratulations! You won!");
                    }
                    break;
                }

                if (isRobotTurn)
                {
                    Console.WriteLine("Robot played: ");
                    algo.NextMove(board);
                    isRobotTurn = false;
                }
                else
                {
                    Console.WriteLine("Please, place your turn:");
                    PersonMove(board);
                    isRobotTurn = true;
                }
                PrintBoard(board);
            }
        }

        private static void PersonMove(char[,] board)
        {
            var move = Console.ReadLine();
            var rowCol = move.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var parsedRowCol = rowCol.Select(x => int.Parse(x)).ToArray();

            while (
                parsedRowCol[0] > 2 || parsedRowCol[0] < 0 || 
                parsedRowCol[1] > 2 || parsedRowCol[1] < 0 ||
                board[parsedRowCol[0], parsedRowCol[1]] != MinMaxAlgo.EmptySymbol)
            {
                Console.WriteLine("Wrong input. Please enter new values:");
                move = Console.ReadLine();
                rowCol = move.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                parsedRowCol = rowCol.Select(x => int.Parse(x)).ToArray();
            }

            board[parsedRowCol[0], parsedRowCol[1]] = MinMaxAlgo.PersonTurnSymbol;
        }

        private static void InitBoard(char[,] board)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    board[i, j] = MinMaxAlgo.EmptySymbol;
        }

        private static void PrintBoard(char[,] board)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    sb.Append(board[i, j] + " ");
                }
                sb.AppendLine();
            }
            Console.WriteLine(sb.ToString());
        }
    }
}
