using System;

namespace Connect4_FinalProj
{

    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Play();
        }
    }


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
            Console.WriteLine("Let's Play Connect 4");

            Console.WriteLine("Player One please enter your name: ");
            playerOne.Name = Console.ReadLine();
            Console.WriteLine("Player Two please enter your name: ");
            playerTwo.Name = Console.ReadLine();

            board.Display();

            while (!gameEnded)
            {
                playerOne.DropPiece(board);
                board.Display();
                if (board.CheckWin(playerOne))
                {

                    Console.WriteLine($"{playerOne.Name} wins!");
                    gameEnded = true;
                    break;
                }
                if (board.IsFull())
                {

                    Console.WriteLine("The board is full, it is a draw!");
                    gameEnded = true;
                    break;
                }

                playerTwo.DropPiece(board);
                board.Display();
                if (board.CheckWin(playerTwo))
                {

                    Console.WriteLine($"{playerTwo.Name} wins!");
                    gameEnded = true;
                    break;
                }
                if (board.IsFull())
                {

                    Console.WriteLine("The board is full, it is a draw!");
                    gameEnded = true;
                    break;
                }
            }

            Console.WriteLine("Would you like to play again? (Y/N)");
            string input = Console.ReadLine();
            if (input.ToUpper() == "Y")
            {

                board.Reset();
                gameEnded = false;
                Play();
            }
            else
            {

                Console.WriteLine("Thanks for playing!");
            }
        }
    }


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



            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    Console.Write($"| {board[row, col]} ");
                }
                Console.WriteLine("|");
            }
            //   Console.WriteLine("---------------");
            Console.WriteLine("  1  2  3  4  5  6  7");
        }

        public bool DropPiece(int col, Player player)
        {
            for (int row = 5; row >= 0; row--)
            {
                if (board[row, col] == '\0')
                {
                    board[row, col] = player.Piece;
                    return true;
                }
            }
            return false;
        }

        public bool CheckWin(Player player)
        {
            char piece = player.Piece;

            // Check horizontal
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

            // Check vertical
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

            // Check diagonal (bottom left to top right)
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

            // Check diagonal (top left to bottom right)
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
                if (board[0, col] == '\0')
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
                    board[row, col] = '\0';
                }
            }
        }
    }


    class Player
    {
        public string Name { get; set; }
        public char Piece { get; private set; }

        public Player(string name, char piece)
        {
            Name = name;
            Piece = piece;
        }

        public void DropPiece(Board board)
        {

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

        }
    }
}
