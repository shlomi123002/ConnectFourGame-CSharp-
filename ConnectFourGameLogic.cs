using System;

namespace Ex02ConnectFour
{
    class ConnectFourGameLogic
    {
        public bool SomePlayerWin(ConnectFourBoard i_BoardGame, int i_Column, int i_Row, Tokens.eToken i_CurrectToken)
        {
            int fourConnect = 0;
            bool thereIsAWinner = false;

            fourConnect = ColumnSequenceOfTokensCounter(i_BoardGame, i_Column, i_Row, i_CurrectToken);
            if(fourConnect == 4)
            {
                thereIsAWinner = true;
                goto end;
            }

            fourConnect = RowSequenceOfTokensCounter(i_BoardGame, i_Column, i_Row, i_CurrectToken);
            if (fourConnect == 4)
            {
                thereIsAWinner = true;
                goto end;
            }

            fourConnect = AscendingDiagonalSequenceOfTokensCounter(i_BoardGame, i_Column, i_Row, i_CurrectToken);
            if (fourConnect == 4)
            {
                thereIsAWinner = true;
                goto end;
            }

            fourConnect = DescendingDiagonalSequenceOfTokensCounter(i_BoardGame, i_Column, i_Row, i_CurrectToken);
            if (fourConnect == 4)
            {
                thereIsAWinner = true;
                goto end;
            }

            end : return thereIsAWinner;
        }

        public int ColumnSequenceOfTokensCounter(ConnectFourBoard i_BoardGame, int i_Column, int i_Row, Tokens.eToken i_CurrectToken)
        {
            int count = 1;
            
            for (int row = i_Row + 1 ; row < i_BoardGame.Height; row++)  
            {
                if (i_BoardGame.Matrix[row, i_Column] == i_CurrectToken)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            return count;
        }

        public int RowSequenceOfTokensCounter(ConnectFourBoard i_BoardGame, int i_Column, int i_Row, Tokens.eToken i_CurrectToken)
        {
            int count = 1;

            for (int col = i_Column + 1 ; col < i_BoardGame.Width ; col++) // right row 
            {
                if (i_BoardGame.Matrix[i_Row, col] == i_CurrectToken)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            for (int col = i_Column - 1 ; col >= 0 ; col--) // left row 
            {
                if (i_BoardGame.Matrix[i_Row, col] == i_CurrectToken)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            return count;
        }

        public int AscendingDiagonalSequenceOfTokensCounter(ConnectFourBoard i_BoardGame, int i_Column, int i_Row, Tokens.eToken i_CurrectToken)
        {
            int count = 1;
            int row = i_Row - 1;
            int col = i_Column + 1;

            while (col < i_BoardGame.Width && row >= 0) // right ascending diagonal
            {
                if (i_BoardGame.Matrix[row, col] == i_CurrectToken)
                {
                    count++;
                }
                else
                {
                    break;
                }
                row--;
                col++;
            }

            row = i_Row + 1;
            col = i_Column - 1;
            while (col >= 0 && row < i_BoardGame.Height) // left ascending diagonal 
            {
                if (i_BoardGame.Matrix[row, col] == i_CurrectToken)
                {
                    count++;
                }
                else
                {
                    break;
                }
                row++;
                col--;
            }

            return count;
        }

        public int DescendingDiagonalSequenceOfTokensCounter(ConnectFourBoard i_BoardGame, int i_Column, int i_Row, Tokens.eToken i_CurrectToken)
        {
            int count = 1;
            int row = i_Row + 1;
            int col = i_Column + 1;

            while (col < i_BoardGame.Width && row < i_BoardGame.Height) // right descending diagonal
            {
                if (i_BoardGame.Matrix[row, col] == i_CurrectToken)
                {
                    count++;
                }
                else
                {
                    break;
                }
                row++;
                col++;
            }

            row = i_Row - 1;
            col = i_Column - 1;
            while (col >= 0 && row >= 0)// left descending diagonal
            {
                if (i_BoardGame.Matrix[row, col] == i_CurrectToken)
                {
                    count++;
                }
                else
                {
                    break;
                }
                row--;
                col--;
            }

            return count;
        }

        public void SwapTurn(Player i_Player1, Player i_Player2)
        {
            i_Player1.MyTurn = !i_Player1.MyTurn;
            i_Player2.MyTurn = !i_Player2.MyTurn;
        }

        public int RandomColumnToInsert(ConnectFourBoard i_Board)
        {
            int columnToInsert = 0;
            Random column = new Random() ;

            while (true)
            {
                columnToInsert = column.Next(0, i_Board.Width);
                if(!i_Board.IsFullColumn(columnToInsert))
                {
                    break;
                }
            }

            return columnToInsert;
        }

        public int FindBestMoveForComputer(ConnectFourBoard i_Board)
        {
            int scoreOfMove = -1;
            int bestScoreOFmove = -1;
            int columnToInsert = 0;
            int rowToInsert = 0;
            int columnToBlockOtherPlayer = -1;

            for (int col = 0; col < i_Board.Width; col++)
            {
                if (!i_Board.IsFullColumn(col))
                {
                    rowToInsert = i_Board.EmptyRowToInsert(col);
                    scoreOfMove = IfPlayerEnteredTokenHowMatchSequence(i_Board, col, rowToInsert , Tokens.eToken.O);
                }
                if (scoreOfMove > bestScoreOFmove)
                {
                    bestScoreOFmove = scoreOfMove;
                    columnToInsert = col;
                }
                if(bestScoreOFmove == 4)
                {
                    break;
                }
            }

            if (bestScoreOFmove < 4)
            {
                columnToBlockOtherPlayer = ColumnToInsertToBlockPlayer(i_Board);
                if(columnToBlockOtherPlayer != -1)
                {
                    columnToInsert = columnToBlockOtherPlayer;
                }
            }

            return columnToInsert;
        }

        public int IfPlayerEnteredTokenHowMatchSequence(ConnectFourBoard i_Board, int i_Column , int i_Row , Tokens.eToken i_Token) 
        {
            int sequenceLength;
            int maxSequenceLength = 0;

            sequenceLength = ColumnSequenceOfTokensCounter(i_Board, i_Column, i_Row, i_Token);
            if (sequenceLength > maxSequenceLength)
            {
                maxSequenceLength = sequenceLength;
            }

            sequenceLength = RowSequenceOfTokensCounter(i_Board, i_Column, i_Row, i_Token);
            if (sequenceLength > maxSequenceLength)
            {
                maxSequenceLength = sequenceLength;
            }

            sequenceLength = AscendingDiagonalSequenceOfTokensCounter(i_Board, i_Column, i_Row, i_Token);
            if (sequenceLength > maxSequenceLength)
            {
                maxSequenceLength = sequenceLength;
            }

            sequenceLength = DescendingDiagonalSequenceOfTokensCounter(i_Board, i_Column, i_Row, i_Token);
            if (sequenceLength > maxSequenceLength)
            {
                maxSequenceLength = sequenceLength;
            }

            return maxSequenceLength;
        }

        public int ColumnToInsertToBlockPlayer(ConnectFourBoard i_BoardGame)
        {
            int columnToInsert = - 1;
            int countOfTokens = 0;
            int row = 0;

            for (int col = 0; col < i_BoardGame.Width; col++)
            {
                row = i_BoardGame.EmptyRowToInsert(col);
                countOfTokens = IfPlayerEnteredTokenHowMatchSequence(i_BoardGame, col, row, Tokens.eToken.X);
                if(countOfTokens == 4)
                {
                    columnToInsert = col;
                    break;
                }
            }
        
            return columnToInsert;
        }
    }
}
