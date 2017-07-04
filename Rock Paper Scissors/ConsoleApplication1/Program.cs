using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        bool humanOrPCPlayer2; // This bool is true when the game is human v human and false when human v computer.
        bool lizardSpock; //This bool is true when the game is expanded into rock paper scissors lizard spock and false when not.

        enum moves {rock = 0, paper, scissors, spock, lizard}; //Move types stored in enum in order on diagram rather than name of game
        string[] moveStrings = { "rock", "paper", "scissors", "spock", "lizard" };

        moves player1Move; //The move selected by player 1
        moves player2Move; //The move selected by player 2

        static void Main()
        {
            Program p = new Program();
            p.run();
        }

        void run()
        {
            //"Title screen"
            Console.Write("Welcome to Rock Paper Scissors by Ashley Basten!\n");

            //Setup the options for the game
            SetupHumanOrNPC();
            SetupLizardSpock();

            //For tracking turns in each game
            int turns = 0;

            //Game loop
            bool gameOver = false; //Used to track when the game finishes
            while(gameOver == false)
            {
                //Restart turn tracker at the start of a new game
                turns = 1;

                //Display options for player 1
                Console.WriteLine("Player 1, select your move.");
                DisplayMoves();

                //Take Player 1 input
                player1Move = ChooseMove();

                //Player 2's turn

                //Human player 2
                if (humanOrPCPlayer2)
                {
                    //If player 2 is a human enter an "anti-cheat" wall of text 50 lines tall
                    for(int i = 0; i < 50; i++)
                    {
                        Console.WriteLine();
                    }

                    //Display options for player 2
                    Console.WriteLine("Player 2, select your move.");
                    DisplayMoves();

                    //Take Player 2 input
                    player2Move = ChooseMove();
                }

                //Robot player 2's turn
                else
                {
                    //Select a move randomly to be fair.
                    Random rng = new Random();
                    int selection = rng.Next(0, 6); //0 to 6 as exclusive of upper bouind inclusive of lower
                    player2Move = (moves)selection;
                }

                //Output result of the round
                Console.WriteLine("Player one selected " + player1Move.ToString());
                Console.WriteLine("Player two selected " + player2Move.ToString());

                //If a tie occurs
                if (player1Move == player2Move)
                {
                    Console.WriteLine("IT'S A TIE! Kepp going!");
                    turns++;
                }

                //Otherwise the game has been won by someone, game over
                else
                {
                    gameOver = true;
                }
            }

            //Output final results
            int winner = DecideWinner();
            Console.WriteLine("Player " + winner.ToString() + " won in " + turns.ToString() + " turns!");

            //Ask to reset or end
            RestartOrEnd();
        }

        //Option select for no of players
        void SetupHumanOrNPC()
        {
            //Write instructions for human or npc to the console
            Console.WriteLine("Number of human players?");
            Console.WriteLine("1 - Human vs Computer");
            Console.WriteLine("2 - Human vs Human");

            //Get input and decide what to do
            string input = Console.ReadLine();

            //if 1 - computer player 2
            if (input == "1")
            {
                humanOrPCPlayer2 = false;
            }

            //if 2 - human player 2
            else if (input == "2")
            {
                humanOrPCPlayer2 = true;
            }

            //Otherwise bad input try again
            else
            {
                Console.WriteLine();
                Console.WriteLine("That was not a valid option, please try again.");
                SetupHumanOrNPC();
            }
        }

        //Option select for expanding to include lizard and spock
        void SetupLizardSpock()
        {
            //Write instructions for human or npc to the console
            Console.WriteLine("Would you like to add lizards and Spock?");
            Console.WriteLine("1 - No. Standard Rock Paper Scissors.");
            Console.WriteLine("2 - Yes. Rock Paper Scissors Lizard Spock.");

            //Get input and decide what to do
            string input = Console.ReadLine();

            //if 1 - standard
            if (input == "1")
            {
               lizardSpock = false;
            }

            //if 2 - add extras
            else if (input == "2")
            {
                lizardSpock = true;
            }

            //Otherwise bad input try again
            else
            {
                Console.WriteLine();
                Console.WriteLine("That was not a valid option, please try again.");
                SetupLizardSpock();
            }
        }

        //Displays the move options
        void DisplayMoves()
        {
            //Display the first 3 (default) options
            Console.WriteLine("1 - Rock");
            Console.WriteLine("2 - Paper");
            Console.WriteLine("3 - Scissors");

            //Display lizard and spock if requested in the options
            if(lizardSpock)
            {
                Console.WriteLine("4 - Lizard");
                Console.WriteLine("5 - Spock");
            }
        }

        //Returns a move after getting input
        moves ChooseMove()
        {
            //Get input and decide what to do
            string input = Console.ReadLine();
            switch(input)
            {
                case "1": //Rock
                    return moves.rock;

                case "2": //Paper
                    return moves.paper;

                case "3": //Scissors
                    return moves.scissors;

                case "4": //Lizard
                    //If included return
                    if(lizardSpock)
                    {
                        return moves.lizard;
                    }
                    //else pick again
                    else
                    {
                       return ChooseAnotherMove();
                    }

                case "5": //Spock
                    //If included return
                    if (lizardSpock)
                    {
                        return moves.spock;
                    }
                    //else pick again
                    else
                    {
                        return ChooseAnotherMove();
                    }

                default: //Invalid move choice
                    return ChooseAnotherMove();
            }
        }

        //Re displays the moves list and gets the player to choose another move (used when bad input used to choose move)
        moves ChooseAnotherMove()
        {
            Console.WriteLine("That was not a valid choice, please coose again.");
            DisplayMoves();
            return ChooseMove();
        }

        //Works out who won
        int DecideWinner()
        {
            //Outer pentagon of diagram
            if ((player1Move - player2Move + 5) % 5 == 1)   //1 as paper beats rock, this is true until rock beats lizard, an edge case where this = -4
            {                                               //adding 5 then % 5 makes all winning combo's on the outer pentagon = 1
                return 1;                                   //So if this formulae = 1 player 1 has won with an outer move
            }
            //If not check inner pentagram
            else if((player1Move - player2Move + 5) % 5 == 3) //Similar to above but inner pentagram winner - loser = 3 or -2, after +5 %5 always equals 3
            {
                return 1;                                     //Player 1 has an inner win
            }

            //If we reach this line it is not a draw or player 1 win so player 2 wins
            return 2;
        }

        //Restart or end the game
        void RestartOrEnd()
        {
            //Write instructions
            Console.WriteLine("Play again?");
            Console.WriteLine("1 - Yes");
            Console.WriteLine("2 - No");

            //Get input and decide what to do
            string input = Console.ReadLine();

            //if 1 restart
            if (input == "1")
            {
                run();
            }

            //if 2 close program
            else if (input == "2")
            {
                //Do nothing, will bypass next else and program will end and close.
            }

            //Otherwise bad input try again
            else
            {
                Console.WriteLine();
                Console.WriteLine("That was not a valid option, please try again.");
                RestartOrEnd();
            }
        }
    }
}
