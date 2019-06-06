using System.Drawing;
using System.ComponentModel;
using Object_Classes;


namespace Game_Logic_Class
{
    public static class SpaceRaceGame
    {
        // Minimum and maximum number of players.
        public const int MIN_PLAYERS = 2;
        public const int MAX_PLAYERS = 6;
   
        private static int numberOfPlayers = 2;  //default value for test purposes only 
        public static int NumberOfPlayers
        {
            get
            {
                return numberOfPlayers;
            }
            set
            {
                numberOfPlayers = value;
            }
        }

        public static string[] names = { "One", "Two", "Three", "Four", "Five", "Six" };  // default values
        
        // Only used in Part B - GUI Implementation, the colours of each player's token
        private static Brush[] playerTokenColours = new Brush[MAX_PLAYERS] { Brushes.Yellow, Brushes.Red,
                                                                       Brushes.Orange, Brushes.White,
                                                                      Brushes.Green, Brushes.DarkViolet};
        /// <summary>
        /// A BindingList is like an array which grows as elements are added to it.
        /// </summary>
        private static BindingList<Player> players = new BindingList<Player>();
        public static BindingList<Player> Players
        {
            get
            {
                return players;
            }
        }

        // The pair of die
        private static Die die1 = new Die(), die2 = new Die();
       

        /// <summary>
        /// Set up the conditions for this game as well as
        ///   creating the required number of players, adding each player 
        ///   to the Binding List and initialize the player's instance variables
        ///   except for playerTokenColour and playerTokenImage in Console implementation.
        ///   
        ///     
        /// Pre:  none
        /// Post:  required number of players have been initialsed for start of a game.
        /// </summary>
        public static void SetUpPlayers() 
        {
            // for number of players
            //      create a new player object
            //      initialize player's instance variables for start of a game
            //      add player to the binding list

            // Create New Player Objects depending on how many players join the game
            for (int createPlayer = 0; createPlayer < NumberOfPlayers; createPlayer++) {

                // New Player Object 
                Player gamePlayer = new Player();
                // Assign Starting Properties
                gamePlayer.Position = Board.START_SQUARE_NUMBER;
                gamePlayer.Location = Board.Squares[Board.START_SQUARE_NUMBER];
                gamePlayer.RocketFuel = Player.INITIAL_FUEL_AMOUNT;
                gamePlayer.HasPower = true;
               

                // Add Player to Binding 
                players.Add(gamePlayer);

            }

        }


        /// <summary>
        ///  Plays one round of a game
        /// </summary>
        public static void PlayOneRound()
        {
            // Player Step
            if (playerStep)
            {
                // Roll followed by Player Movement
                Players[PlayerNum].Play(die1, die2);

                // End Round if Player is Last to Roll               
                if (PlayerNum == (NumberOfPlayers - 1))
                {
                    // Check For Game Over
                    CheckIfGameFinished();
                    // Reset Player Turns for Upcoming Round
                    PlayerNum = 0;
                }
                else
                {
                    // Next Player If Round Active
                    PlayerNum++;
                }
            }
            else
            {
                foreach (Player player in Players)
                {
                    // Roll followed by Player Movement
                    player.Play(die1, die2);
                }
                // Check For Game Over
                CheckIfGameFinished();
            }
        }


        private static bool gamefinished;
        public static bool GameFinished
        {
            get
            {
                return gamefinished;
            }
            set
            {
                gamefinished = value;
            }
        }

        // Player Step
        private static bool singleStep;
        public static bool playerStep
        {
            get
            {
                return singleStep;
            }
            set
            {
                singleStep = value;
            }
        }

        // The Player Number
        // Starts at 0
        private static int playerNum = 0;
        public static int PlayerNum
        {
            get
            {
                return playerNum;
            }
            set
            {
                playerNum = value;
            }
        }

        public static string DisplayGameResults()
        {
            bool playerReachFinish = false;
            string endGameOutput = "";
            string playerWinners = "";
            // Check if Player(s) Reach the Finish | May be None*
            for (int player = 0; player < NumberOfPlayers; player++)
            {
                if (Players[player].AtFinish)
                {
                    // Append Players to List if Reach Finish
                    playerWinners += string.Format("\n\t\t{0}", Players[player].Name);
                    playerReachFinish = true;
                }
            }
            // Display Winners & End Game Output
            if (playerReachFinish)
            {
                endGameOutput += ("\n\n\tThe following player(s) finished the game.\n");
                endGameOutput += (playerWinners + "\n\n");
            }
            else //No players finished the game
            {
                endGameOutput += ("\n\n\tNo players finished the game\n"); // ******** UNNECESSARY? ********
            }

            //Send the message
            return endGameOutput;
        }

        /// <summary>
        /// At the end of a round, checks that any player is at the finish or if all players cannot move (out of fuel)
        /// Sets the property 'GameFinished' to true if the game is finished.
        /// </summary>
        private static void CheckIfGameFinished()
        {
            // Check After Round Finished
            bool allPlayersNoPower = true;

            foreach (Player player in Players)
            {
                // Check for Players Available Power
                if (player.HasPower)
                {
                    allPlayersNoPower = false;
                }
                // Check for Game Over or Player Round
                if (player.AtFinish || allPlayersNoPower)
                {
                    GameFinished = true;
                }
            }
        }

    }
}