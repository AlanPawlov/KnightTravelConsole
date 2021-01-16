using System;

namespace knight_travel
{
    class Program
    {
        static void Main(string[] args)
        {
            // создается объект коня с первоначальной его позицией на доске
            KnightTravel knightTravel = new KnightTravel(0, 0);

            int[,] chessBoard = knightTravel.GetChessBoardArray();

            PrintChessBoard(chessBoard);
            Console.ReadLine();
        }

        // печать массива шахматной доски
        static void PrintChessBoard(int[,] board)
        {
            for (int c = 0; c < 8; c++)
            {
                for (int r = 0; r < 8; r++)
                {
                    Console.Write(board[c, r]);
                    Console.Write("\t");
                }
                Console.WriteLine();
            }
        }
    }
}
