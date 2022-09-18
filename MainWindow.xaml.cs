using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region private members

        /// <summary>
        /// Hold the current result of cell in active game
        /// </summary>
        private MarkType[] mResults;
        /// <summary>
        /// True if it is player 1 turn (X), and false if it is player 2 turn (0)
        /// </summary>
        private bool mPlayer1Turn;
        /// <summary>
        /// tTrue if the game has ended
        /// </summary>
        private bool mGameOver;

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        #endregion

        private void NewGame()
        {
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;
            }

            mPlayer1Turn = true;

            Container.Children.Cast<Button>().ToList().ForEach( button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            mGameOver = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameOver)
            {
                NewGame();
                return;
            }

            var myButton = (Button)sender;

            int column = Grid.GetColumn(myButton);
            int row = Grid.GetRow(myButton);

            int index = column + (3 * row);

            //don't do anything if the cell has a value in it
            if(mResults[index]!= MarkType.Free)
            {
                return;
            }

            //set the cell value based on which player turn is
            if (mPlayer1Turn)
            {
                mResults[index] = MarkType.Cross;
            }
            else
            {
                mResults[index] = MarkType.Nought;
            }

            //set the button content acording to the player turn
            myButton.Content = mPlayer1Turn ? "X" : "0";

            //change nought to green
            if (!mPlayer1Turn)
            {
                myButton.Foreground = Brushes.LightGreen;
            }

            mPlayer1Turn ^= true;

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            #region Row Wins
            //Row 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0]) 
            {
                mGameOver = true;
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            //Row 1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameOver = true;
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            //Row 2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameOver = true;
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Vertical Wins

            //Coloumn 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameOver = true;
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            //Coloumn 1
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameOver = true;
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            //Coloumn 2
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameOver = true;
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }

            #endregion

            #region Diagonal Wins

            //Top-left, buttom-right
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameOver = true;
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            //top-right, buttom left
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameOver = true;
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            #endregion

            //check for the winner and to see if the board if full
            if (!mResults.Any(f => f ==MarkType.Free))
            {
                mGameOver = true;

                Container.Children.Cast<Button>().ToList().ForEach(button =>
               {
                   button.Background = Brushes.Orange;
               });
            }


        }
    }
}
