using EinsteinWurfeltNicht.Controller;
using EinsteinWurfeltNicht.Model;
using EinsteinWurfeltNicht.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EinsteinWurfeltNicht.Util.Algorithm
{
    public class Minimax
    {

        public enum Turn { AI, USER };

        public ChessOwner[,] chessBoard;
        EwnController mainController;

        public Minimax(EwnController controller)
        {
            mainController = controller;
            chessBoard = new ChessOwner[ChessBoardView.CHESS_BOARD_SIZE, ChessBoardView.CHESS_BOARD_SIZE];
            for (int i = 0; i < ChessBoardView.CHESS_BOARD_SIZE; i++)
            {
                for (int j = 0; j < ChessBoardView.CHESS_BOARD_SIZE; j++)
                {
                    chessBoard[i, j] = controller.chessBoardView.chessBoardHash[i, j];
                }
            }
        }

        public int Calc(int curAiPos)
        {
            int m = curAiPos / ChessBoardView.CHESS_BOARD_SIZE;
            int n = curAiPos % ChessBoardView.CHESS_BOARD_SIZE;

            int maxx = int.MinValue;
            ChessOwner obk;

            int pos = curAiPos;

            if (m < ChessBoardView.CHESS_BOARD_SIZE - 1)
            {
                obk = chessBoard[m + 1, n];
                chessBoard[m, n] = ChessOwner.EMPTY;
                chessBoard[m + 1, n] = ChessOwner.PLAYER1;
                int c = CalPlayer();
                Console.WriteLine(c);
                if (c > maxx)
                {
                    maxx = c;
                    pos = (m + 1) * ChessBoardView.CHESS_BOARD_SIZE + n;
                }
                chessBoard[m + 1, n] = obk;
                chessBoard[m, n] = ChessOwner.PLAYER1;
            }
            if (n < ChessBoardView.CHESS_BOARD_SIZE - 1)
            {
                obk = chessBoard[m, n + 1];
                chessBoard[m, n] = ChessOwner.EMPTY;
                chessBoard[m, n + 1] = ChessOwner.PLAYER1;
                int c = CalPlayer();
                Console.WriteLine(c);
                if (c > maxx)
                {
                    maxx = c;
                    pos = m * ChessBoardView.CHESS_BOARD_SIZE + n + 1;
                }
                chessBoard[m, n + 1] = obk;
                chessBoard[m, n] = ChessOwner.PLAYER1;
            }
            if (m < ChessBoardView.CHESS_BOARD_SIZE - 1 && n < ChessBoardView.CHESS_BOARD_SIZE - 1)
            {
                obk = chessBoard[m + 1, n + 1];
                chessBoard[m, n] = ChessOwner.EMPTY;
                chessBoard[m + 1, n + 1] = ChessOwner.PLAYER1;
                int c = CalPlayer();
                Console.WriteLine(c);
                if (c > maxx)
                {
                    maxx = c;
                    pos = (m + 1) * ChessBoardView.CHESS_BOARD_SIZE + n + 1;
                }
                chessBoard[m + 1, n + 1] = obk;
                chessBoard[m, n] = ChessOwner.PLAYER1;
            }
            return pos;
        }

        private int CalcAi(int d = 0)
        {
            //Console.WriteLine(d);

            if (d == 5)
                return Eval();

            int maxx = int.MinValue;
            if (chessBoard[ChessBoardView.CHESS_BOARD_SIZE - 1, ChessBoardView.CHESS_BOARD_SIZE - 1] == ChessOwner.PLAYER1)
                return 10;
            else if (chessBoard[0, 0] == ChessOwner.PLAYER2)
                return -10;

            int cnt1 = 0, cnt2 = 0;

            for (int i = 0; i < ChessBoardView.CHESS_BOARD_SIZE; i++)
            {
                for (int j = 0; j < ChessBoardView.CHESS_BOARD_SIZE; j++)
                {
                    if (chessBoard[i, j] == ChessOwner.PLAYER1)
                    {
                        cnt1++;
                        ArrayList list = GetMoveRange(Turn.AI, i, j);

                        foreach (Object o in list)
                        {
                            int m = ((int)o) / ChessBoardView.CHESS_BOARD_SIZE;
                            int n = ((int)o) % ChessBoardView.CHESS_BOARD_SIZE;
                            ChessOwner obk = chessBoard[m, n];
                            chessBoard[i, j] = ChessOwner.EMPTY;
                            chessBoard[m, n] = ChessOwner.PLAYER1;
                            maxx = Math.Max(CalcAi(d + 1), maxx);
                            chessBoard[m, n] = obk;
                            chessBoard[i, j] = ChessOwner.PLAYER1;
                        }
                    }
                    if (chessBoard[i, j] == ChessOwner.PLAYER2)
                        cnt2++;
                }
            }

            if (cnt1 == 0)
                return -10;
            if (cnt2 == 0)
                return 10;

            return maxx;
        }

        private int CalPlayer(int d = 0)
        {
            //Console.WriteLine(d);
            

            int mini = int.MaxValue;

            if (chessBoard[ChessBoardView.CHESS_BOARD_SIZE - 1, ChessBoardView.CHESS_BOARD_SIZE - 1] == ChessOwner.PLAYER1)
                return 10;
            else if (chessBoard[0, 0] == ChessOwner.PLAYER2)
                return -10;

            int cnt1 = 0, cnt2 = 0;

            for (int i = 0; i < ChessBoardView.CHESS_BOARD_SIZE; i++)
            {
                for (int j = 0; j < ChessBoardView.CHESS_BOARD_SIZE; j++)
                {
                    if (chessBoard[i, j] == ChessOwner.PLAYER2)
                    {
                        cnt2++;
                        ArrayList list = GetMoveRange(Turn.USER, i, j);

                        foreach (Object o in list)
                        {
                            int m = ((int)o) / ChessBoardView.CHESS_BOARD_SIZE;
                            int n = ((int)o) % ChessBoardView.CHESS_BOARD_SIZE;
                            ChessOwner obk = chessBoard[m, n];
                            chessBoard[i, j] = ChessOwner.EMPTY;
                            chessBoard[m, n] = ChessOwner.PLAYER2;
                            mini = Math.Min(CalcAi(d + 1), mini);
                            chessBoard[m, n] = obk;
                            chessBoard[i, j] = ChessOwner.PLAYER2;
                        }
                    }
                    if (chessBoard[i, j] == ChessOwner.PLAYER1)
                        cnt1++;
                }
            }
            if (cnt1 == 0)
                return -10;
            if (cnt2 == 0)
                return 10;
            return mini;
        }

        public ArrayList GetMoveRange(Turn turn, int m, int n)
        {
            ArrayList arrayList = new ArrayList();
            if (turn == Turn.AI)
            {
                if (m < ChessBoardView.CHESS_BOARD_SIZE - 1 && n < ChessBoardView.CHESS_BOARD_SIZE - 1)
                {
                    arrayList.Add((m + 1) * ChessBoardView.CHESS_BOARD_SIZE + n + 1);
                }
                if (m < ChessBoardView.CHESS_BOARD_SIZE - 1)
                {
                    arrayList.Add((m + 1) * ChessBoardView.CHESS_BOARD_SIZE + n);
                }
                if (n < ChessBoardView.CHESS_BOARD_SIZE - 1)
                {
                    arrayList.Add(m * ChessBoardView.CHESS_BOARD_SIZE + n + 1);
                }
                
            }
            else
            {
                if (m >= 1 && n >= 1)
                {
                    arrayList.Add((m - 1) * ChessBoardView.CHESS_BOARD_SIZE + n - 1);
                }
                if (m >= 1)
                {
                    arrayList.Add((m - 1) * ChessBoardView.CHESS_BOARD_SIZE + n);
                }
                if (n >= 1)
                {
                    arrayList.Add(m * ChessBoardView.CHESS_BOARD_SIZE + n - 1);
                }
                
            }

            return arrayList;
        }

        private int Eval()
        {
            int dis = 0;
            for (int i = 0; i < ChessBoardView.CHESS_BOARD_SIZE; i++)
            {
                for (int j = 0; j < ChessBoardView.CHESS_BOARD_SIZE; j++)
                {
                    if (chessBoard[i, j] == ChessOwner.PLAYER2)
                    {
                        dis -= ((ChessBoardView.CHESS_BOARD_SIZE - 1 - i) * (ChessBoardView.CHESS_BOARD_SIZE - 1 - i) + (ChessBoardView.CHESS_BOARD_SIZE - j) * (ChessBoardView.CHESS_BOARD_SIZE - j));
                    }
                    if (chessBoard[i, j] == ChessOwner.PLAYER1)
                    {
                        dis += (i * i + j * j);
                    }
                }
            }
            //Console.WriteLine(dis);
            return dis;
        }
    }
}
