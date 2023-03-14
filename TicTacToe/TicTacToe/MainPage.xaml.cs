using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TicTacToe
{
    public partial class MainPage : ContentPage
    {
        char[,] board = new char[3, 3];
        char currentMove = 'X';
        int numberOfMoves = 0;
        Boolean paused = false;
        public MainPage()
        {
            InitializeComponent();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            string passed = ((Button)sender).BindingContext as string;
            Button button = (Button)sender;
            string[] position = passed.Split(',');
            int row = int.Parse(position[0]);
            int col = int.Parse(position[1]);
            if (!paused)
            {
                if (board[row, col] == '\0')
                {
                    button.Text = currentMove.ToString();
                    board[row, col] = currentMove;  
                    if (currentMove == 'X')
                    {
                        currentMove = 'O';
                        button.BackgroundColor = Color.Red;
                        XActionText.Text = "X player wait";
                        OActionText.Text = "O player's move";
                    }
                    else
                    {
                        button.BackgroundColor = Color.Blue;
                        currentMove = 'X';
                        XActionText.Text = "X player's move";
                        OActionText.Text = "O player wait";
                    }
                    numberOfMoves++;
                    char winning = CheckWinner();
                    if (winning != '\0')
                    {
                        DeclareWinner(winning);
                    }
                }
            }
        }

        void Reset()
        {
            foreach (Button button in BoardGrid.Children.Cast<Button>())
            {
                button.Text = "-";
                button.BackgroundColor = Color.LightGray;
            }
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = default(char);
                }
            }
            currentMove = 'X';
            numberOfMoves = 0;
            XActionText.Text = "X player's move";
            OActionText.Text = "O player wait";
        }

        void DeclareWinner(char winning)
        {
            if (winning == 'X')
            {
                XActionText.Text = "X player won";
                OActionText.Text = "O player lost";
                numberOfMoves = 0;
            }
            else
            {
                XActionText.Text = "X player lost";
                OActionText.Text = "O player won";
                numberOfMoves = 0;
            }
            currentMove = 'X';
            paused = true;
        }


        char CheckWinner()
        {
            // Check rows
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] != '\0' && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                {
                    return board[i, 0];
                }
            }

            // Check columns
            for (int j = 0; j < 3; j++)
            {
                if (board[0, j] != '\0' && board[0, j] == board[1, j] && board[1, j] == board[2, j])
                {
                    return board[0, j];
                }
            }

            // Check diagonals
            if (board[0, 0] != '\0' && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                return board[0, 0];
            }

            if (board[0, 2] != '\0' && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                return board[0, 2];
            }

            // If there is no winner
            return '\0';
        }

        void Reset_Pressed(System.Object sender, System.EventArgs e)
        {
            Reset();
            paused = false;
        }
    }
}

