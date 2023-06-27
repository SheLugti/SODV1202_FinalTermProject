using System;

namespace Connect4_FinalProj
{
    // Abstract Player Base Class
    abstract class PlayerBase
    {
        public string Name { get; set; }
        public char Piece { get; private set; }

        public PlayerBase(string name, char piece)
        {
            Name = name;
            Piece = piece;
        }

        public abstract void DropPiece(Board board);
    }

    // Game Play Class
    class Game
    {
        private Board board;
        private Player playerOne;
        private Player playerTwo;
        private bool gameEnded;

        public Game()
        {
            board = new Board();
            playerOne = new Player("Player One", 'X');
            playerTwo = new Player("Player Two", 'O');
            gameEnded = false;
        }

        public void Play()
        {
            Console.WriteLine("Welcome to Connect 4 Game!"); //updated message
            Console.WriteLine();
            Console.Write("Player One, please enter your name: ");
            playerOne.Name = Console.ReadLine();
            Console.Write("Player Two, please enter your name: ");
            playerTwo.Name = Console.ReadLine();
            Console.Clear();
            board.Display();

            while (!gameEnded)
            {
                playerOne.DropPiece(board);
                board.Display();
                if (board.CheckWin(playerOne))
                {
                    Console.WriteLine();
                    Console.WriteLine($"{playerOne.Name} wins!");
                    gameEnded = true;
                    break;
                }
                if (board.IsFull())
                {
                    Console.WriteLine();
                    Console.WriteLine("Board full! It's a draw!");
                    gameEnded = true;
                    break;
                }

                playerTwo.DropPiece(board);
                board.Display();
                if (board.CheckWin(playerTwo))
                {
                    Console.WriteLine();
                    Console.WriteLine($"{playerTwo.Name} wins!");
                    gameEnded = true;
                    break;
                }
                if (board.IsFull())
                {
                    Console.WriteLine();
                    Console.WriteLine("Board full! It's a draw!");
                    gameEnded = true;
                    break;
                }
            }

            Console.Write("Would you like to play again (Y/N)? ");
            string input = Console.ReadLine();
            if (input.ToUpper() == "Y")
            {
                Console.Clear();
                board.Reset();
                gameEnded = false;
                Play();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Thank you for playing!");
            }
        }
    }

    // Game Board Class
    class Board
    {
        private char[,] board;

        public Board()
        {
            board = new char[6, 7];
            Reset();
        }

        public void Display()
        {
            Console.WriteLine();
            Console.WriteLine(">> Now Playing Connect 4 Game <<");
            Console.WriteLine();
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    Console.Write($" | {board[row, col]}");
                }
                Console.WriteLine(" | ");
            }
            Console.WriteLine("   1   2   3   4   5   6   7");

        }

        public bool DropPiece(int col, PlayerBase player)
        {
            for (int row = 5; row >= 0; row--)
            {
                try // added try catch to require input of numbers bet 1 - 7 only
                { 
                if (board[row, col] == ' ')
                {
                    board[row, col] = player.Piece;
                    return true;
                }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 7.");
                    return false;
                }
            }
            return false;
        }

        public bool CheckWin(PlayerBase player)
        {
            char piece = player.Piece;

            // check horizontal
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (board[row, col] == piece && board[row, col + 1] == piece &&
                        board[row, col + 2] == piece && board[row, col + 3] == piece)
                    {
                        return true;
                    }
                }
            }

            // check vertical
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if (board[row, col] == piece && board[row + 1, col] == piece &&
                        board[row + 2, col] == piece && board[row + 3, col] == piece)
                    {
                        return true;
                    }
                }
            }

            // check diagonal (bottom left to top right)
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (board[row, col] == piece && board[row + 1, col + 1] == piece &&
                        board[row + 2, col + 2] == piece && board[row + 3, col + 3] == piece)
                    {
                        return true;
                    }
                }
            }

            // check diagonal (top left to bottom right)
            for (int row = 3; row < 6; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (board[row, col] == piece && board[row - 1, col + 1] == piece &&
                        board[row - 2, col + 2] == piece && board[row - 3, col + 3] == piece)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsFull()
        {
            for (int col = 0; col < 7; col++)
            {
                if (board[0, col] == ' ')
                {
                    return false;
                }
            }
            return true;
        }

        public void Reset()
        {
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    board[row, col] = ' ';
                }
            }
        }
    }

    // Player Class
    class Player: PlayerBase
    {
        public Player(string name, char piece) : base(name, piece)
        {

        }

        public override void DropPiece(Board board)
        {
            Console.WriteLine();
            Console.WriteLine($"{Name}'s turn ({Piece})");
            int col;
            bool validInput = false;
            do
            {
                Console.Write("Enter column (1-7): ");
                try
                {
                    col = int.Parse(Console.ReadLine()) - 1;
                    validInput = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 7.");
                    col = -1;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 7.");
                    col = -1;
                }
            } while (!validInput || !board.DropPiece(col, this));
            Console.Clear(); //clear the screen to avoid repetitive board printing
        }
    }

    // Main Program Class
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Play();
        }
    }
}















