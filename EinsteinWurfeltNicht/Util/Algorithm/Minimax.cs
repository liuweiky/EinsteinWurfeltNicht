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
        static int CalcTime = 0;
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
            Console.WriteLine("Eval" + Eval());
            int m = curAiPos / ChessBoardView.CHESS_BOARD_SIZE;
            int n = curAiPos % ChessBoardView.CHESS_BOARD_SIZE;

            double maxx = double.MinValue;
            ChessOwner obk;

            int pos = curAiPos;
            if (m < ChessBoardView.CHESS_BOARD_SIZE - 1 && n < ChessBoardView.CHESS_BOARD_SIZE - 1)
            {
                obk = chessBoard[m + 1, n + 1];
                chessBoard[m, n] = ChessOwner.EMPTY;
                chessBoard[m + 1, n + 1] = ChessOwner.PLAYER1;
                double c = CalPlayer();
                Console.WriteLine(c);
                if (c > maxx)
                {
                    maxx = c;
                    pos = (m + 1) * ChessBoardView.CHESS_BOARD_SIZE + n + 1;
                }
                chessBoard[m + 1, n + 1] = obk;
                chessBoard[m, n] = ChessOwner.PLAYER1;
            }
            if (m < ChessBoardView.CHESS_BOARD_SIZE - 1)
            {
                obk = chessBoard[m + 1, n];
                chessBoard[m, n] = ChessOwner.EMPTY;
                chessBoard[m + 1, n] = ChessOwner.PLAYER1;
                double c = CalPlayer();
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
                double c = CalPlayer();
                Console.WriteLine(c);
                if (c > maxx)
                {
                    maxx = c;
                    pos = m * ChessBoardView.CHESS_BOARD_SIZE + n + 1;
                }
                chessBoard[m, n + 1] = obk;
                chessBoard[m, n] = ChessOwner.PLAYER1;
            }
            return pos;
        }

        private double CalcAi(int d = 0)
        {
            //Console.WriteLine(d);

            if (d >= CalcTime * 0.7 + 5)
                return Eval();

            double maxx = double.MinValue;
            if (chessBoard[ChessBoardView.CHESS_BOARD_SIZE - 1, ChessBoardView.CHESS_BOARD_SIZE - 1] == ChessOwner.PLAYER1)
                return Eval() + 20;
            else if (chessBoard[0, 0] == ChessOwner.PLAYER2)
                return Eval() - 20;

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
                            maxx = Math.Max(CalPlayer(d + 1), maxx);
                            chessBoard[m, n] = obk;
                            chessBoard[i, j] = ChessOwner.PLAYER1;
                        }
                    }
                    if (chessBoard[i, j] == ChessOwner.PLAYER2)
                        cnt2++;
                }
            }

            if (cnt1 == 0)
                return Eval();
            if (cnt2 == 0)
                return Eval();

            return maxx;
        }

        private double CalPlayer(int d = 0)
        {
            //Console.WriteLine(d);


            double mini = double.MaxValue;

            if (chessBoard[ChessBoardView.CHESS_BOARD_SIZE - 1, ChessBoardView.CHESS_BOARD_SIZE - 1] == ChessOwner.PLAYER1)
                return Eval() + 20;
            else if (chessBoard[0, 0] == ChessOwner.PLAYER2)
                return Eval() - 20;

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
                return Eval();
            if (cnt2 == 0)
                return Eval();
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

        public double Eval()
        {
            double dis_ai = 0, dis_usr = 0, cnt_ai = 0, cnt_usr = 0;
            for (int i = 0; i < ChessBoardView.CHESS_BOARD_SIZE; i++)
            {
                for (int j = 0; j < ChessBoardView.CHESS_BOARD_SIZE; j++)
                {
                    if (chessBoard[i, j] == ChessOwner.PLAYER2)
                    {
                        cnt_usr++;
                        double dis = Math.Sqrt((double) (i * i + j * j));
                        dis_usr += dis;
                        // 让 usr 远离中线
                        dis_usr -= Math.Abs(i - j);
                    }
                    if (chessBoard[i, j] == ChessOwner.PLAYER1)
                    {
                        cnt_ai++;
                        double dis = Math.Sqrt(
                            (ChessBoardView.CHESS_BOARD_SIZE - i) * (ChessBoardView.CHESS_BOARD_SIZE - i) 
                            + (ChessBoardView.CHESS_BOARD_SIZE - j) * (ChessBoardView.CHESS_BOARD_SIZE - j));
                        dis_ai += dis;
                        // ai 尽量靠近中线
                        dis_ai += Math.Abs(i - j) * 2;
                    }
                }
            }
            double ai_avg = cnt_ai == 0 ? int.MaxValue : dis_ai / cnt_ai;
            double usr_avg = cnt_usr == 0 ? int.MaxValue : dis_usr / cnt_usr;
            //Console.WriteLine(dis);
            return ((usr_avg - ai_avg) * 10 + (cnt_ai - cnt_usr) * 5);
        }
    }
}
