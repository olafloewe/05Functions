using System;
namespace Assignment_1 {
    public class Class1 {
        ﻿
            static void Main(string[] args) {

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

                    // take turn and toggle turn order
                    gameState[input - 1] = (xTurn) ? 'X' : 'O';
                    xTurn = !xTurn;

                    // jagged char array for board
                    char[][] board = {
                    new char [] { ' ', gameState[0],' ','|',' ', gameState[1], ' ','|',' ', gameState[2], ' '},
                    new char [] { '-','-','-','+','-','-','-','+','-','-','-' },
                    new char [] {' ', gameState[3], ' ','|',' ', gameState[4], ' ','|',' ', gameState[5], ' '},
                    new char [] {'-','-','-','+','-','-','-','+','-','-','-'},
                    new char [] {' ', gameState[6], ' ','|',' ', gameState[7], ' ','|',' ', gameState[8], ' '}
                };

                    PrintBoard(board);

                    if (EndOfGame(gameState)) {
                        Console.WriteLine("Game Over!");
                        break;
                    }
                }
            }

            // checks to find an empty space in gamestate and returns false
            private static bool EndOfGame(char[] gameState) {
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