using System;

namespace Connect4_FinalProj
{
    // Game Menu Class
    class GameMenu
    {
        public void Display()
        {
            Console.WriteLine("Welcome to Connect 4 Game!");
            Console.WriteLine();
            Console.WriteLine("Please select an option:");
            Console.WriteLine("(1) Human vs Human");
            Console.WriteLine("(2) Human vs AI");
            Console.WriteLine("(3) Exit");
            Console.Write("Enter your choice (1-3): ");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.Clear();
                    Console.Write("Enter Player One's name: ");
                    string playerOneName = Console.ReadLine();
                    Console.Write("Enter Player Two's name: ");
                    string playerTwoName = Console.ReadLine();
                    Console.Clear();
                    Game game1 = new Game(new Player(playerOneName, 'X'), new Player(playerTwoName, 'O'));
                    game1.Play();
                    break;
                case "2":
                    Console.Clear();
                    Console.Write("Enter Player One's name: ");
                    string playerName = Console.ReadLine();
                    Game game2 = new Game(new Player(playerName, 'X'), new AIPlayer("AI", 'O'));
                    game2.Play();
                    break;
                case "3":
                    Console.WriteLine();
                    Console.WriteLine("Thank you for playing!");
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Invalid input. Please select a valid option.");
                    Console.WriteLine();
                    Display();
                    break;
            }
        }
    }

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
        private PlayerBase playerOne;
        private PlayerBase playerTwo;
        private bool gameEnded;

        public Game(PlayerBase playerOne, PlayerBase playerTwo)
        {
            board = new Board();
            this.playerOne = playerOne;
            this.playerTwo = playerTwo;
            gameEnded = false;
        }

        public void Play()
        {
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
    class Player : PlayerBase
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

    // AI Player Class
    class AIPlayer : PlayerBase
    {
        public AIPlayer(string name, char piece) : base(name, piece)
        {
        }

        public override void DropPiece(Board board)
        {
            Console.WriteLine();
            Console.WriteLine($"{Name}'s turn ({Piece})");
            //AI random move
            Random random = new Random();
            int col;
            do
            {
                col = random.Next(7);
            } while (!board.DropPiece(col, this));
            Console.WriteLine($"AI placed piece in column {col + 1}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear(); //clear the screen to avoid repetitive board printing
        }
    }

    // Main Program Class
    class Program
    {
        static void Main(string[] args)
        {
            GameMenu menu = new GameMenu();
            menu.Display();
        }
    }
}
