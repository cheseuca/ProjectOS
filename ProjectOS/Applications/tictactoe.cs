using Cosmos.System;
using ProjectOS.Commands;

namespace ProjectOS.Applications
{
    internal class TicTacToe : Command
    {
        private char[,] board;
        private char currentPlayer;

        public TicTacToe(string name) : base(name)
        {
            board = new char[3, 3];
            currentPlayer = 'X';
            InitializeBoard();
        }

        public override string execute(string[] args)
        {
            System.Console.Clear();
            DrawBoard();
            PlayGame();
            return " ";
        }

        private void InitializeBoard()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = ' ';
                }
            }
        }

        private void DrawBoard()
        {
            System.Console.WriteLine("===============");
            for (int row = 0; row < 3; row++)
            {
                System.Console.Write("| ");
                for (int col = 0; col < 3; col++)
                {
                    System.Console.Write(board[row, col] + " | ");
                }
                System.Console.WriteLine("\n===============");
            }
        }

        private void PlayGame()
        {
            while (true)
            {
                System.Console.WriteLine($"Player {currentPlayer}'s turn");
                System.Console.Write("Enter row (0-2): ");
                int row = int.Parse(System.Console.ReadLine());
                System.Console.Write("Enter column (0-2): ");
                int col = int.Parse(System.Console.ReadLine());

                if (IsValidMove(row, col))
                {
                    board[row, col] = currentPlayer;
                    if (CheckWin())
                    {
                        System.Console.WriteLine($"Player {currentPlayer} wins!");
                        break;
                    }
                    else if (IsBoardFull())
                    {
                        System.Console.WriteLine("The game is a tie!");
                        break;
                    }
                    else
                    {
                        SwitchPlayer();
                        System.Console.Clear();
                        DrawBoard();
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid move. Try again.");
                }
            }
        }

        private bool IsValidMove(int row, int col)
        {
            return row >= 0 && row < 3 && col >= 0 && col < 3 && board[row, col] == ' ';
        }

        private bool CheckWin()
        {
            // Check rows, columns, and diagonals for a win
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer)
                    return true;

                if (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer)
                    return true;
            }

            if (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer)
                return true;

            if (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer)
                return true;

            return false;
        }

        private bool IsBoardFull()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == ' ')
                        return false;
                }
            }
            return true;
        }

        private void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }
    }
}
