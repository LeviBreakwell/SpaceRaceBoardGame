using System;
using Game_Logic_Class;
using Object_Classes;


namespace Space_Race
{
    class Console_Class
    {
        /// <summary>
        /// Algorithm below currently plays only one game
        /// 
        /// when have this working correctly, add the abilty for the user to 
        /// play more than 1 game if they choose to do so.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            WelcomeMessage();
            /*                    
             Set up the board in Board class (Board.SetUpBoard)
             Determine number of players - initally play with 2 for testing purposes 
             Create the required players in Game Logic class
              and initialize players for start of a game             
             loop  until game is finished           
                call PlayGame in Game Logic class to play one round
                Output each player's details at end of round
             end loop
             Determine if anyone has won
             Output each player's details at end of the game
           */




            // Initialise Board
            Board.SetUpBoard();


            do
            {
                // Ask for Number of Players
                int totalPlayers = InputPlayerTotal();
               
                // Set Number of Players to Total Game Players
                SpaceRaceGame.NumberOfPlayers = totalPlayers;

                // Assign Players a Name from Player Name Array
                SpaceRaceGame.SetUpPlayers();

                // Set Game Bool to False
                SpaceRaceGame.GameFinished = false;

                // Initialise and Set Initial Round Value
                int RoundNumber = 0;

                // Loop Game until Game Finished is equal to True
                while (!SpaceRaceGame.GameFinished)
                {

                    // Count Round Numbers
                    RoundStart(RoundNumber);
                    // Play Round
                    SpaceRaceGame.PlayOneRound();
                    ShowRoundResults();

                    // round++;
                    RoundNumber++;


                }

                // Show End Game Results
                PrintGameResults();


            } while (NewGame());

            // Display Thankyou Message & Press Enter to Finish
            ThankyouMsg();
            PressEnter();



        }//end Main


        /// <summary>
        /// Display a welcome message to the console
        /// Pre:    none.
        /// Post:   A welcome message is displayed to the console.
        /// </summary>
        static void WelcomeMessage()
        {
            Console.WriteLine("\tWelcome to Space Race.\n");
        } //end DisplayIntroductionMessage

        /// <summary>
        /// Displays a prompt and waits for a keypress.
        /// Pre:  none
        /// Post: a key has been pressed.
        /// </summary>
        public static void PressEnter()
        {
            Console.WriteLine("\tPress Enter to terminate program ...");
            Console.ReadLine();
        } // end PressAny

        /// <summary>
        /// Repeatedly ask for a valid number of players and return the input 
        /// </summary>
        static int InputPlayerTotal()
        {
            // Collect User Input
            //string UserInput = "";
            // Set Initial total to 0
            int totalPlayers = 0;

            // Prompt User for Total Game Players
            Console.WriteLine("\tThis game is for 2 to 6 players.");
            Console.Write("\tEnter Total Number of players to Play Space Race: ");

            // Read User Input for Total Players
            string UserInput = Console.ReadLine();


            // Test Input & Loop until Acceptable Number was Entered
            if ((int.TryParse(UserInput, out totalPlayers)) && (totalPlayers >= SpaceRaceGame.MAX_PLAYERS) && (totalPlayers <= SpaceRaceGame.MAX_PLAYERS))
            {
                return totalPlayers;
            }
            else
            {
                Console.WriteLine("Invalid number of players entered. Please Try Again");
                return InputPlayerTotal();
            }
        }

        /// <summary>
        /// Display the name, position and fuel of every player at that round
        /// </summary>
        static void ShowRoundResults()
        {
            foreach (Player player in SpaceRaceGame.Players)
            {
                Console.WriteLine(String.Format("\t{0} on square {1} with {2} yottawatt of power remaining", player.Name, player.Position, player.RocketFuel));
            }
        }

        static void RoundStart(int round_number)
        {

            Console.WriteLine("\n\nPress Enter to play a round...");
            Console.ReadLine();
            if (round_number == 0)
            {
                Console.WriteLine("\n\tFirst Round\n");
            }
            else
            {
                Console.WriteLine("\n\tNext Round\n");
            }
        }

        /// <summary>
        /// Ask the user to enter Y or N
        /// Returns true if the user wishes to continue
        /// False if they want to exit
        /// Or asks again if they enter an invalid answer
        /// </summary>
        static bool NewGame()
        {
            string newGameInput = "";

            Console.Write("\nWould You Like To Play Again? (Y or N): ");
            newGameInput = Console.ReadLine();

            // If User enters 'Y', a New Game is created
            if (newGameInput == "Y")
            {
                return true;
            }

            // Likewise Game is Finished when User enters 'N'
            else if (newGameInput == "N")
            {
                return false;
            }

            // If Input is Not Valid, Repeat Question
            else
            {
                Console.Write("\nInvalid Input");
                return NewGame();
            }
        }

        /// <summary>
        /// Display the game results and
        /// wait for user to press enter key to advance
        /// </summary>
        static void PrintGameResults()
        {
            // Display Game Results
            Console.WriteLine(SpaceRaceGame.DisplayGameResults());
            //Individual Results
            Console.WriteLine("\n\nIndividual players finished at the locations specified.");
            foreach (Player player in SpaceRaceGame.Players)
            {
                Console.WriteLine(String.Format("\n\n\n{0} with {1} yottawatt of power at square {2}", player.Name, player.RocketFuel, player.Position));
            }

            // Wait for Enter Press
            Console.WriteLine("\n\n\nPress Enter key to continue...");
            Console.ReadLine();
        }

        /// <summary>
        /// Display the exit message.
        /// </summary>
        static void ThankyouMsg()
        {

            Console.WriteLine("\n\n\nThank You For Playing Space Race! See You Next Time");

        }
    } //end Console class
}
