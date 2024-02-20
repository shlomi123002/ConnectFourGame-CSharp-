using System;
using Ex02.ConsoleUtils;

namespace Ex02ConnectFour
{
    class UserInterFaceInputOutput
    {
        private int m_ColumnToInsert;
        private readonly bool r_VsComputer;
        private readonly bool r_TwoPlayers;
        private ConnectFourBoard m_BoardMatrix;
        private ConnectFourGameLogic m_GameLogic;

        public int ColumnToInsert
        {
            get { return m_ColumnToInsert; }
            set { this.m_ColumnToInsert = value; }
        }

        public bool VsComputer
        {
            get { return r_VsComputer; }
        }

        public bool TwoPlayers
        {
            get { return r_TwoPlayers; }
        }

        public ConnectFourBoard BoardMatrix
        {
            get { return this.m_BoardMatrix; }
            set { this.m_BoardMatrix = value; }
        }

        public ConnectFourGameLogic GameLogic
        {
            get { return this.m_GameLogic; }
            set { this.m_GameLogic = value; }
        }

        public UserInterFaceInputOutput()
        {
            string playerChoice;
            Console.WriteLine("        Four In Row Game        ");
            Console.WriteLine("________________________________");
            Console.WriteLine("");
            this.m_BoardMatrix = new ConnectFourBoard(WidthAndHeightInput("height"), WidthAndHeightInput("width"));
            this.m_GameLogic = new ConnectFourGameLogic();
            playerChoice = VsComputerOrTwoPlayers();

            if (playerChoice == "1")
            {
                this.r_VsComputer = true;
            }
            if (playerChoice == "2")
            {
                this.r_TwoPlayers = true;
            }
        }

        public void StartGame()
        {
            Player player1 = new Player();
            Player player2 = new Player();
            bool anotherGame = true;

            while (anotherGame)
            {
                FourInRowGame(player1, player2);
                ShowPoints(player1, player2);
                anotherGame = AskForAnotherGame();
                this.m_BoardMatrix.CleanBoard();
            }
        }

        public void FourInRowGame(Player i_Player1, Player i_Player2)
        {
            int length = this.m_BoardMatrix.Height * this.m_BoardMatrix.Width;
            int rowToInsert = 0;
            int columnToInsert = 0; 
            bool quitGame = false;
            i_Player1.MyTurn = true;
            i_Player2.MyTurn = false;

            while (length > 0)
            {
                Screen.Clear();
                PrintGameBoard();
                if (i_Player1.MyTurn)
                {
                    quitGame = ChooseColumnToInsertOrQuitGame("X");
                }
                else
                {
                    if(this.r_TwoPlayers)
                    {
                        quitGame = ChooseColumnToInsertOrQuitGame("O");
                    }
                }

                columnToInsert = this.m_ColumnToInsert;
                if (quitGame == false)
                {
                    if (i_Player1.MyTurn)
                    {
                        rowToInsert = this.m_BoardMatrix.EmptyRowToInsert(columnToInsert);
                        this.m_BoardMatrix.Add(Tokens.eToken.X, columnToInsert, rowToInsert);
                        if (this.m_GameLogic.SomePlayerWin(this.m_BoardMatrix, columnToInsert, rowToInsert, Tokens.eToken.X))
                        {
                            i_Player1.Winner();
                            Screen.Clear();
                            PrintGameBoard();
                            PlayerWin(Tokens.eToken.X);
                            break;
                        }
                    }
                    else
                    {
                        if (this.r_TwoPlayers)
                        {
                            rowToInsert = this.m_BoardMatrix.EmptyRowToInsert(columnToInsert);
                            this.m_BoardMatrix.Add(Tokens.eToken.O, columnToInsert, rowToInsert);
                        }
                        else
                        {
                            columnToInsert = this.m_GameLogic.FindBestMoveForComputer(this.m_BoardMatrix);
                            rowToInsert = this.m_BoardMatrix.EmptyRowToInsert(columnToInsert);
                            this.m_BoardMatrix.Add(Tokens.eToken.O, columnToInsert, rowToInsert);
                        }
                        if (this.m_GameLogic.SomePlayerWin(this.m_BoardMatrix, columnToInsert, rowToInsert, Tokens.eToken.O))
                        {
                            i_Player2.Winner();
                            Screen.Clear();
                            PrintGameBoard();
                            PlayerWin(Tokens.eToken.O);
                            break;
                        }
                    }

                    length--;
                    if (length == 0)
                    {
                        Screen.Clear();
                        PrintGameBoard();
                        TieGame();
                        break;
                    }

                this.m_GameLogic.SwapTurn(i_Player1, i_Player2);
                }
                else
                {
                    if (i_Player1.MyTurn)
                    {
                        i_Player2.Winner();
                        PlayerWin(Tokens.eToken.O);
                        break;
                    }
                    else
                    {
                        i_Player1.Winner();
                        PlayerWin(Tokens.eToken.X);
                        break;
                    }
                }
            }
        }

        public int WidthAndHeightInput(string i_Length)
        {
            int output = 0;
            bool isNumber = false;

            while (isNumber == false)
            {
                Console.WriteLine("Enter {0} to board game (between 4 to 8) :", i_Length);
                isNumber = int.TryParse(Console.ReadLine(), out output);
                if (!isNumber)
                {
                    Console.WriteLine("Try again");
                }
                else
                {
                    if (output < 4 || output > 8)
                    {
                        Console.WriteLine("Try again");
                        isNumber = false;
                    }
                }
            }

            return output;
        }

        public void PrintGameBoard()
        {
            for (int i = 1; i <= this.m_BoardMatrix.Width; i++)
            {
                if (i == 1)
                {
                    Console.Write("  {0}", i);
                }
                else
                {
                    Console.Write("   {0}", i);
                }
            }

            Console.WriteLine("");
            for (int i = 0; i < this.m_BoardMatrix.Height; i++)
            {
                for (int j = 0; j < this.m_BoardMatrix.Width; j++)
                {
                    if (this.m_BoardMatrix.Matrix[i, j] == null)
                    {
                        Console.Write("|   ");
                    }
                    else
                    {
                        Console.Write("| {0} ", this.m_BoardMatrix.Matrix[i, j]);
                    }
                }

                Console.WriteLine("|");
                for (int h = 0; h < this.m_BoardMatrix.Width; h++)
                {
                    Console.Write("====");
                }

                Console.WriteLine("=");
            }
        }
        
        public string VsComputerOrTwoPlayers()
        {
            string playerChoice = null;
           
            while (playerChoice != "1" && playerChoice != "2")
            {
                Console.WriteLine("How would you like to play the game?(choose 1 or 2)");
                Console.WriteLine("1.Vs computer");
                Console.WriteLine("2.Two players");
                playerChoice = Console.ReadLine();

                if (playerChoice != "1" || playerChoice!= "2")
                {
                    Console.WriteLine("Try again...");
                }
            }

            return playerChoice;
        }

        public bool ChooseColumnToInsertOrQuitGame(string participant)
        {
            bool isNumber = false;
            int column = 0;
            string playerChoice;
            bool quitGame = false;

            while (isNumber == false)
            {
                Console.WriteLine("Player {0}, choose column to insert or press Q to quit the game :", participant); 
                playerChoice = Console.ReadLine();

                if (playerChoice == "Q" || playerChoice == "q")
                {
                    quitGame = true;
                    goto end;
                }

                isNumber = int.TryParse(playerChoice, out column);
                column = column - 1;
                if (isNumber)
                {
                    if (column < 0 || column >= this.m_BoardMatrix.Width)
                    {
                        Console.WriteLine("Column is not in the range, please choose another colmun. ");
                        isNumber = false;
                    }
                    else if (this.m_BoardMatrix.IsFullColumn(column) == true)
                    {
                        Console.WriteLine("the column is full , please choose another colmun.");
                        isNumber = false;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Try again...");
                }
            }

            this.m_ColumnToInsert = column;
            end : return quitGame;
        }

        public void PlayerWin(Tokens.eToken participant)
        {
            Console.WriteLine("Winner!!!");
            if (this.r_TwoPlayers)
            {
                Console.WriteLine("Player ({0}) is the winner", participant);
            }
            else
            {
                Console.WriteLine("Computer ({0}) is the winner", participant);
            }
        }

        public void TieGame()
        {
            Console.WriteLine("The game ended in a tie.");
        }

        public void ShowPoints(Player i_Player1, Player i_Player2)
        {
            Console.WriteLine("     Points");
            Console.WriteLine("================");
            Console.WriteLine("Player 1 : {0}", i_Player1.PlayerPoints);
            if (this.r_TwoPlayers)
            {
                Console.WriteLine("Player 2 : {0}", i_Player2.PlayerPoints);
            }
            else
            {
                Console.WriteLine("Computer : {0}", i_Player2.PlayerPoints);
            }
        }
        
        public bool AskForAnotherGame()
        {
            bool anotherGame = false;
            bool userChooseUnCurrectCharacter = true;
            string userChoise;

            while (userChooseUnCurrectCharacter)
            {
                Console.WriteLine("If you want to countinue new game press C , to quit press Q.");
                userChoise = Console.ReadLine();
                if (userChoise == "C" || userChoise == "c")
                {
                    anotherGame = true;
                    userChooseUnCurrectCharacter = false;
                }
                else if (userChoise == "Q" || userChoise == "q")
                {
                    anotherGame = false;
                    userChooseUnCurrectCharacter = false;
                }
                else
                {
                    Console.WriteLine("Try again...");
                }
            }

            return anotherGame;
        }
    }
}
