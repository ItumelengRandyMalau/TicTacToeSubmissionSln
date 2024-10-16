using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeRendererLib.Enums;
using TicTacToeRendererLib.Renderer;

namespace TicTacToeSubmissionConole
{
    public class TicTacToe
    {
        private TicTacToeConsoleRenderer _boardRenderer;
        private PlayerEnum currentPlayer;
        private PlayerEnum?[,] board;

        public TicTacToe()
        {
            _boardRenderer = new TicTacToeConsoleRenderer(10, 6);
            currentPlayer = PlayerEnum.X; // default player to start the game
            board = new PlayerEnum?[3, 3]; // game_board
            _boardRenderer.Render();
        }

        public void Run()
        {
            bool gameOver = false;

            do
            {
                Console.SetCursorPosition(2, 19);
                Console.Write($"Player {currentPlayer}");

                // Get row input
                Console.SetCursorPosition(2, 20);
                Console.Write("Please Enter Row (0-2): ");
                int row = GetValidInput(0, 2);

                // Get column input
                Console.SetCursorPosition(2, 22);
                Console.Write("Please Enter Column (0-2): ");
                int column = GetValidInput(0, 2);

                // Checking if the selected cell is a valid move and hasnt been selected before
                if (board[row, column] == null)
                {
                    // adding a move on the game board
                    board[row, column] = currentPlayer;
                    _boardRenderer.AddMove(row, column, currentPlayer, true);

                    // Checking for win or draw move
                    if (CheckWin(row, column))
                    {
                        Console.SetCursorPosition(2, 24);
                        Console.WriteLine($"Player {currentPlayer} wins!");
                        gameOver = true;
                    }
                    else if (IsBoardFull())
                    {
                        Console.SetCursorPosition(2, 24);
                        Console.WriteLine("It's a draw!");
                        gameOver = true;
                    }
                    else
                    {
                        // player switching based on turns
                        currentPlayer = (currentPlayer == PlayerEnum.X) ? PlayerEnum.O : PlayerEnum.X;
                    }
                }
                else
                {
                    Console.SetCursorPosition(2, 24);
                    Console.WriteLine("That cell is already occupied. Try again.");
                    System.Threading.Thread.Sleep(2000); // displaying error message for 2 seconds
                    Console.Clear();
                    _boardRenderer.Render(); // clearing the screen and redrwaing the game board
                }

            } while (!gameOver);
        }

        // gets valid input for row/column
        private int GetValidInput(int min, int max)
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input) || input < min || input > max)
            {
                Console.SetCursorPosition(2, 24);
                Console.Write($"Invalid input. Please enter a number between {min} and {max}: ");
                System.Threading.Thread.Sleep(2000); // displaying error message for 2 seconds
                Console.Clear();
                _boardRenderer.Render(); // clearing the screen and redrawing game board
            }
            return input;
        }

        // Checking if the current player has won
        private bool CheckWin(int row, int column)
        {
            // Checking the row
            bool rowWin = true;
            for (int i = 0; i < 3; i++)
            {
                if (board[row, i] != currentPlayer)
                {
                    rowWin = false;
                    break;
                }
            }

            // Checking the column
            bool colWin = true;
            for (int i = 0; i < 3; i++)
            {
                if (board[i, column] != currentPlayer)
                {
                    colWin = false;
                    break;
                }
            }

            // Checking diagonals
            bool diag1Win = (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer);
            bool diag2Win = (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer);

            return rowWin || colWin || diag1Win || diag2Win;
        }

        // Checking if the board is full (for a draw)
        private bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == null)
                        return false;
                }
            }
            return true;
        }
    }
}