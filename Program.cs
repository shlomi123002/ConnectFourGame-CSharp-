using System;

namespace Ex02ConnectFour
{
    class Program
    {
        public static void Main(string[] args)
        {
            StartGameConnectFour();
        }

        public static void StartGameConnectFour()
        {
            UserInterFaceInputOutput connectFourGameUI = new UserInterFaceInputOutput();
            connectFourGameUI.StartGame();
        }
    }
}

