using System;

namespace Ex02ConnectFour
{
    class Player
    {
        private int m_PlayerPoints;
        private bool m_MyTurn;

        public int PlayerPoints
        {
            get { return this.m_PlayerPoints; }
            set { this.m_PlayerPoints = value; }
        }

        public bool MyTurn
        {
            get { return this.m_MyTurn; }
            set { this.m_MyTurn = value; }
        }

        public Player()
        {
            this.m_PlayerPoints = 0;
            this.m_MyTurn = false;
        }

        public void Winner()
        {
            this.m_PlayerPoints++;
        }
    }
}
