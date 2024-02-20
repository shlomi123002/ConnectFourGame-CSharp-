using System;

namespace Ex02ConnectFour
{
    class ConnectFourBoard
    {
        private readonly int r_Width; 
        private readonly int r_Height;
        private Tokens.eToken?[,] m_BoardMatrix;

        public int Width
        {
            get { return this.r_Width; }
        }

        public int Height
        {
            get { return this.r_Height; }
        }

        public Tokens.eToken?[,] Matrix
        {
            get { return this.m_BoardMatrix; }
            set { this.m_BoardMatrix = value; }
        }

        public ConnectFourBoard(int i_Height , int i_Width)
        {
            this.r_Height = i_Height;
            this.r_Width = i_Width;
            this.m_BoardMatrix = new Tokens.eToken?[this.r_Height, this.r_Width];
        }

        public void Add(Tokens.eToken i_Sign, int i_Column , int i_Row)
        {
            this.m_BoardMatrix[i_Row, i_Column] = i_Sign;
        }

        public int EmptyRowToInsert(int i_Column)
        {
            int row = 0;
            for (int i = this.r_Height - 1; i >= 0; i--)
            {
                if (this.m_BoardMatrix[i, i_Column] == null)
                {
                    row = i;
                    break;
                }
            }

            return row;
        }

        public bool IsFullColumn(int i_Column)
        {
            bool fullColumn = false;

            if (this.m_BoardMatrix[0, i_Column] != null)
            {
                fullColumn = true;
            }
            else
            {
                fullColumn = false;
            }

            return fullColumn;
        }

        public void CleanBoard()
        {
            for (int i = 0; i < this.r_Height; i++)
            {
                for (int j = 0; j < this.r_Width; j++)
                {
                    this.m_BoardMatrix[i, j] = null;
                }
            }
        }
    }
}
