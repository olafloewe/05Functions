using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment_2 {
    public class Class1 {
        
        static void Main(string[] args) {
            InitiateStyle();

            Dictionary<int, String> menu = InitializeMenu();
            DrawMenu(menu, 0);

            int selected;
            do {
                selected = SelectOption(menu);
            } while (selected != menu.Count);

        }

        // style function for console
        private static void InitiateStyle() {
            // Sets the console title.
            Console.Title = "simple menu";

            // set color theme
            Theme();

            // Hides the Cursor
            Console.CursorVisible = false;

            // Clears the background
            Console.Clear();

            // hard coded declaration of window dimensions
            Console.SetWindowSize(100, 20);
        }

        // choose from a color theme
        private static void Theme(String style = "") {
            switch (style) {
                case "highlight":
                    // colors to "highlight"
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                default:
                    // reset colors to default
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
            }
        }

        // menu
        private static Dictionary<int, String> InitializeMenu() {
            // initialize and define menu options
            Dictionary<int, String> options = new Dictionary<int, String>();

            options.Add(1, "New Game");
            options.Add(2, "About the Author");
            options.Add(3, "Quit");
            return options;
        }

        // draw menu
        private static void DrawMenu(Dictionary<int, String> options, int selected = 0) {
            // print header of menu
            Console.SetCursorPosition(2, 2); // indents cursor by 2 from the top and the left
            Console.WriteLine("Menu - Please Select");

            // print options of menu
            foreach (KeyValuePair<int, String> option in options) {
                // highlight selected
                string currSelected = (option.Key == selected) ? "highlight" : "";
                Theme(currSelected);

                Console.SetCursorPosition(2, 2 + option.Key); // indent cursor by 2 from left
                Console.Write($"{option.Key}. {option.Value}");
            }

            Theme(); // reset theme
            Console.WriteLine();
            return;
        }

        private static int SelectOption(Dictionary<int, String> menu) {
            int select = 0;
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            bool start = true;

            // do until Selected
            while (keyInfo.Key.ToString() != "Enter") {

                if (!start) keyInfo = Console.ReadKey(true);
                start = false;

                // break on Esc input
                if (keyInfo.Key == ConsoleKey.Escape) return menu.Count;

                // Arrow key navigation with borders
                if (keyInfo.Key == ConsoleKey.UpArrow && select != 1 && select != 0) select--;
                if (keyInfo.Key == ConsoleKey.DownArrow && select != menu.Count) select++;

                // numerical input in range of Keys
                if (int.TryParse(keyInfo.KeyChar.ToString(), out int i) && menu.ContainsKey(i)) select = int.Parse(keyInfo.KeyChar.ToString());

                DrawMenu(menu, select); // highlight selected input option
            }

            // Runs code for selected program
            Task selectedTask;
            Console.Clear();

            switch (select) {
                case 1:
                    selectedTask = new Task(NewGame);
                    break;
                case 2:
                    selectedTask = new Task(AboutTheAuthor);
                    break;
                default:
                    selectedTask = null;
                    DrawMenu(menu);
                    break;
            }
            if (selectedTask == null) return select;
            selectedTask.Start();
            Task.WaitAll(selectedTask);
            // Thread.Sleep(5000);
            // selectedTask.Wait(1);

            // return to menu
            Console.Clear();
            DrawMenu(menu); // highlight selected input option

            return select;
        }

        private static void NewGame() {
            Console.WriteLine("New Game");

            bool xTurn = true;

            // getting user input and guardian clauses for miss input
            Console.WriteLine("Welcome to tic-tac-toe: ");
            char[] gameState = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', };

            while (true) {
                int input;

                // read gamestate input with guardian clauses
                Console.Write($"{(xTurn ? 'X' : 'O')}'s Turn: > ");
                if (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out input)) continue; // accepts only numerical input
                if (input == 0) continue; // dosnt accept 0 input
                Console.WriteLine();

                // fail if invalid input
                if (gameState[input - 1] == 'X' || gameState[input - 1] == 'O') {
                    Console.WriteLine("Illegal move! Try again.");
                    continue;
                }

                // take turn
                gameState[input - 1] = (xTurn) ? 'X' : 'O';

                // jagged char array for board
                char[][] board = {
                    new char [] { ' ', gameState[0],' ','|',' ', gameState[1], ' ','|',' ', gameState[2], ' '},
                    new char [] { '-','-','-','+','-','-','-','+','-','-','-' },
                    new char [] {' ', gameState[3], ' ','|',' ', gameState[4], ' ','|',' ', gameState[5], ' '},
                    new char [] {'-','-','-','+','-','-','-','+','-','-','-'},
                    new char [] {' ', gameState[6], ' ','|',' ', gameState[7], ' ','|',' ', gameState[8], ' '}
                };
                PrintBoard(board);

                // check for win
                char turnChar = (xTurn) ? 'X' : 'O';
                if (EndOfGame(gameState)) {
                    Console.WriteLine($"{turnChar} has won the game!!");
                    break;
                }

                // toggle turn
                xTurn = !xTurn;

                if (EndOfGame(gameState)) {
                    Console.WriteLine("Game Over!");
                    break;
                }
            }

            Console.WriteLine("Press any key to continue...");
            while (true) {
                // interupt on keypress
                if (Console.KeyAvailable) break;
            }
        }
        private static void AboutTheAuthor() {
            Console.WriteLine("The author of this program is Olaf Loewe");

            Console.WriteLine("Press any key to continue...");
            while (true) {
                // interupt on keypress
                if (Console.KeyAvailable) break;
            }
        }

        // TIC TAC TOE SUPPORT FUNCTIONS

        // checks to find an empty space in gamestate and returns false
        private static bool EndOfGame(char[] gameState) {
            // horizontal win state
            for (int i = 0; i < 6; i += 3) {
                if (gameState[i] == gameState[i + 1] && gameState[i + 1] == gameState[i + 2] && (gameState[i] == 'X' || gameState[i] == 'O')) return true;
            }

            // vertical win state
            for (int i = 0; i < 2; i++) {
                if (gameState[i] == gameState[i + 3] && gameState[i + 3] == gameState[i + 6] && (gameState[i] == 'X' || gameState[i] == 'O')) return true;
            }

            // diagonal win state
            if (gameState[0] == gameState[4] && gameState[4] == gameState[8] && (gameState[0] == 'X' || gameState[0] == 'O')) return true;
            if (gameState[2] == gameState[4] && gameState[4] == gameState[6] && (gameState[2] == 'X' || gameState[2] == 'O')) return true;

            for (int i = 0; i < gameState.Length; i++) {
                if (gameState[i] == ' ') return false;
            }
            return true;
        }

        // method to print the tictactoe board
        private static void PrintBoard(char[][] board) {
            // print board
            for (int i = 0; i < board.Length; i++) {
                for (int j = 0; j < board[i].Length; j++) {
                    Console.Write(board[i][j]);
                }
                Console.WriteLine();
            }
        }

    }
}