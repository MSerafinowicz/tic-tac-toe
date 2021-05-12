using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace tic_tac_toe
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if it is player's 1 turn (X) or player's 2 turn (0)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if the game is ended
        /// </summary>
        private bool mGameEnded;

        #endregion


        #region Starts new game
        private void NewGame()
        {
            //Create a new blank array of free cells
            mResults = new MarkType[9];

            for (var i=0;i<mResults.Length;i++)
            {
                mResults[i] = MarkType.Free;
            }

            //Make sure that player 1 will start
            mPlayer1Turn = true;

            //Initiate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(Button =>
            {
                //Clear all buttons content
                Button.Content = string.Empty;
                Button.Background = Brushes.White;
                Button.Foreground = Brushes.Blue;
            });

            //Game status is not finished
            mGameEnded = false;
        }

        #endregion


        /// <summary>
        /// Handles the button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click (object sender, RoutedEventArgs e)
        {
            //Start a new game on the click after it is finished
            if (mGameEnded==true)
            {
                NewGame();
            }


            //Cast the sender to the button
            var button = (Button)sender;
            //Finds the buttons positions in array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            //Assigns index to right position in array and in Result tab(mResults[])
            var index = column + (row * 3);

            //Does nothing when the cell has already value in it
            if (mResults[index] != MarkType.Free)
                return;
            //Set cell's value based on which player's turn is ||krótka wersja : mResults[index]=mPlayer1Turn?MarkType.Cross:MarkType.Nought;
            if(mPlayer1Turn==true)
            {
                mResults[index] = MarkType.Cross;
            }
            else
            {
                mResults[index] = MarkType.Nought;
            }

            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            //Example of short version if else
            //If it's 1player turn write x and 2player write O
            button.Content = mPlayer1Turn ? "X" : "O";

            //Changes players turn
            //Krótka wersja: mPlayer1Turn ^=true;
            if (mPlayer1Turn == true)
                mPlayer1Turn = false;
            else mPlayer1Turn = true;

            //Check for a winner
            CheckForWinner();
        }


        /// <summary>
        /// Checks if there is a winner
        /// </summary>
        private void CheckForWinner()
        {

            #region Draw
            //Check for no winner and full board
            if (!mResults.Any(f => f == MarkType.Free))
            {
                //Ends the game
                mGameEnded = true;
                //Changes colour of background
                //Initiate every button on the grid
                Container.Children.Cast<Button>().ToList().ForEach(Button =>
                {
                    Button.Background = Brushes.LightCoral;
                });
            }

            #endregion

            #region Horizontal wins
            //Check for a horizontal wins
            //
            //Row - 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0]&mResults[1]&mResults[2])==mResults[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            //
            //Row - 1
            //
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            //
            //Row - 2
            //
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }

            #endregion

            #region Vertical wins
            //Check for vertical wins
            //
            //Column - 0
            //
            if (mResults[0]!=MarkType.Free && (mResults[0]&mResults[3]&mResults[6])==mResults[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            //
            //Column - 1
            //
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            //
            //Column - 2
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Diagonal Wins
            //
            //Check for Diagonal Wins
            //
            if (mResults[0]!=MarkType.Free&&(mResults[0]&mResults[4]&mResults[8])==mResults[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }
            #endregion
        }
    }
}