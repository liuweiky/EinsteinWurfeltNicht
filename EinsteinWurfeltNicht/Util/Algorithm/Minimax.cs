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
                    // 从 chessBoardView 中拷贝当前棋盘状态
                    chessBoard[i, j] = controller.chessBoardView.chessBoardHash[i, j];
                }
            }
        }

        private void PrintHash()
        {
            Console.WriteLine();
            for (int i = 0; i < ChessBoardView.CHESS_BOARD_SIZE; i++)
            {
                for (int j = 0; j < ChessBoardView.CHESS_BOARD_SIZE; j++)
                {
                    Console.Write(chessBoard[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        // 外部调用接口，计算当前处于curAiPos位置的AI棋子向哪个方向走收益最大
        // curAiPos = m * CHESS_BOARD_SIZE + n
        // m是棋盘行号（从0开始），n是棋盘列号（从0开始）
        public int Calc(int curAiPos)
        {
            //PrintHash();
            //Console.WriteLine("Eval" + Eval());
            int m = curAiPos / ChessBoardView.CHESS_BOARD_SIZE;
            int n = curAiPos % ChessBoardView.CHESS_BOARD_SIZE;

            double maxx = double.MinValue;  // 保存向三个方向走的最大收益
            ChessOwner obk;
            double alpha = double.MinValue;
            double beta = double.MaxValue;
            int pos = curAiPos;
            // AI 向右下走
            if (m < ChessBoardView.CHESS_BOARD_SIZE - 1 && n < ChessBoardView.CHESS_BOARD_SIZE - 1)
            {
                obk = chessBoard[m + 1, n + 1]; // 备份右下的棋子
                // 右下移动
                chessBoard[m, n] = ChessOwner.EMPTY;
                chessBoard[m + 1, n + 1] = ChessOwner.PLAYER1;
                // 下一轮用户行棋，计算AI这么走后，接下来的收益 c
                double c = CalPlayer(alpha, beta);
                Console.WriteLine(c);
                // 取最大收益及其对应的落子位置
                if (c >= maxx)
                {
                    maxx = c;
                    pos = (m + 1) * ChessBoardView.CHESS_BOARD_SIZE + n + 1;
                }
                // 估价完成后恢复原来的棋盘状态
                chessBoard[m + 1, n + 1] = obk;
                chessBoard[m, n] = ChessOwner.PLAYER1;
            }
            // AI 向下走
            if (m < ChessBoardView.CHESS_BOARD_SIZE - 1)
            {
                obk = chessBoard[m + 1, n];
                chessBoard[m, n] = ChessOwner.EMPTY;
                chessBoard[m + 1, n] = ChessOwner.PLAYER1;
                double c = CalPlayer(alpha, beta);
                Console.WriteLine(c);
                if (c > maxx)
                {
                    maxx = c;
                    pos = (m + 1) * ChessBoardView.CHESS_BOARD_SIZE + n;
                }
                chessBoard[m + 1, n] = obk;
                chessBoard[m, n] = ChessOwner.PLAYER1;
            }
            // AI 向右走
            if (n < ChessBoardView.CHESS_BOARD_SIZE - 1)
            {
                obk = chessBoard[m, n + 1];
                chessBoard[m, n] = ChessOwner.EMPTY;
                chessBoard[m, n + 1] = ChessOwner.PLAYER1;
                double c = CalPlayer(alpha, beta);
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

        private double CalcAi(double alpha, double beta, int d = 0)
        {
            //PrintHash();
            //Console.WriteLine(d);

            if (d >= CalcTime * 0.7 + 5)
                return Eval();

            //double maxx = double.MinValue;
            // 胜负判断
            if (chessBoard[ChessBoardView.CHESS_BOARD_SIZE - 1, ChessBoardView.CHESS_BOARD_SIZE - 1] == ChessOwner.PLAYER1)
                return 200 + Eval();    // AI胜，返回很大的收益
            else if (chessBoard[0, 0] == ChessOwner.PLAYER2)
                return -200 + Eval();   // AI负，返回很小的收益

            int cnt1 = 0, cnt2 = 0;

            // 对 AI 的棋子向各个方向移动后逐一评估收益，获取最大收益 alpha
            for (int i = 0; i < ChessBoardView.CHESS_BOARD_SIZE; i++)
            {
                for (int j = 0; j < ChessBoardView.CHESS_BOARD_SIZE; j++)
                {
                    // PLAYER1 表示 AI
                    if (chessBoard[i, j] == ChessOwner.PLAYER1)
                    {
                        cnt1++;
                        // 获取棋盘上AI[i, j]棋子可以移动的几个方向
                        ArrayList list = GetMoveRange(Turn.AI, i, j);

                        foreach (Object o in list)
                        {
                            // 将位置 id 转为m、n
                            // posid = m * CHESS_BOARD_SIZE + n
                            int m = ((int)o) / ChessBoardView.CHESS_BOARD_SIZE;
                            int n = ((int)o) % ChessBoardView.CHESS_BOARD_SIZE;
                            ChessOwner obk = chessBoard[m, n];
                            chessBoard[i, j] = ChessOwner.EMPTY;
                            chessBoard[m, n] = ChessOwner.PLAYER1;
                            alpha = Math.Max(CalPlayer(alpha, beta, d + 1), alpha);

                            // 估价完成后恢复原来的棋盘状态
                            chessBoard[m, n] = obk;
                            chessBoard[i, j] = ChessOwner.PLAYER1;

                            // 发生 alpha 剪枝
                            if (alpha >= beta)
                                return alpha;

                        }
                    }
                    if (chessBoard[i, j] == ChessOwner.PLAYER2)
                        cnt2++;
                }
            }

            if (cnt1 == 0)
                return -200 + Eval();
            if (cnt2 == 0)
                return 200 + Eval();

            return alpha;
        }

        private double CalPlayer(double alpha, double beta, int d = 0)
        {
            //Console.WriteLine(d);


            //double mini = double.MaxValue;

            if (chessBoard[ChessBoardView.CHESS_BOARD_SIZE - 1, ChessBoardView.CHESS_BOARD_SIZE - 1] == ChessOwner.PLAYER1)
                return 200 + Eval();
            else if (chessBoard[0, 0] == ChessOwner.PLAYER2)
                return -200 + Eval();

            int cnt1 = 0, cnt2 = 0;

            for (int i = 0; i < ChessBoardView.CHESS_BOARD_SIZE; i++)
            {
                for (int j = 0; j < ChessBoardView.CHESS_BOARD_SIZE; j++)
                {
                    // PLAYER2 表示 用户
                    if (chessBoard[i, j] == ChessOwner.PLAYER2)
                    {
                        cnt2++;
                        // 获取棋盘上USER[i, j]棋子可以移动的几个方向
                        ArrayList list = GetMoveRange(Turn.USER, i, j);

                        foreach (Object o in list)
                        {
                            // 将位置 id 转为m、n
                            // posid = m * CHESS_BOARD_SIZE + n
                            int m = ((int)o) / ChessBoardView.CHESS_BOARD_SIZE;
                            int n = ((int)o) % ChessBoardView.CHESS_BOARD_SIZE;
                            ChessOwner obk = chessBoard[m, n];
                            chessBoard[i, j] = ChessOwner.EMPTY;
                            chessBoard[m, n] = ChessOwner.PLAYER2;
                            beta = Math.Min(CalcAi(alpha, beta, d + 1), beta);

                            // 估价完成后恢复原来的棋盘状态
                            chessBoard[m, n] = obk;
                            chessBoard[i, j] = ChessOwner.PLAYER2;

                            // 发生 beta 剪枝
                            if (alpha >= beta)
                                return beta;
                        }
                    }
                    if (chessBoard[i, j] == ChessOwner.PLAYER1)
                        cnt1++;
                }
            }
            if (cnt1 == 0)
                return -200 + Eval();
            if (cnt2 == 0)
                return 200 + Eval();
            return beta;
        }

        // 根据当前轮次，获取棋盘上[i, j]棋子可以移动的几个方向
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

        /*public double Eval()
        {
            
            double dis_ai = 0, dis_usr = 0, cnt_ai = 0, cnt_usr = 0;
            for (int i = 0; i < ChessBoardView.CHESS_BOARD_SIZE; i++)
            {
                for (int j = 0; j < ChessBoardView.CHESS_BOARD_SIZE; j++)
                {
                    if (chessBoard[i, j] == ChessOwner.PLAYER2)
                    {
                        cnt_usr++;
                        double dis = Math.Max(i, j);
                        dis_usr += dis;
                        // 让 usr 远离中线
                        //dis_usr -= Math.Abs(i - j);
                    }
                    if (chessBoard[i, j] == ChessOwner.PLAYER1)
                    {
                        cnt_ai++;
                        double dis = Math.Max(
                            (ChessBoardView.CHESS_BOARD_SIZE - i - 1) , 
                            (ChessBoardView.CHESS_BOARD_SIZE - j - 1));
                        dis_ai += dis;
                        // ai 尽量靠近中线
                        //dis_ai += Math.Abs(i - j) * 2;
                    }
                }
            }
            //Console.WriteLine(dis);
            return (dis_usr - dis_ai) * 5 + (cnt_ai - cnt_usr) * 3;
        }*/

        // 估价函数
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
                        double dis = Math.Sqrt((double)(i * i + j * j));
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
            // 计算每个棋子离对角点的平均距离
            double ai_avg = cnt_ai == 0 ? int.MaxValue : dis_ai / cnt_ai;
            double usr_avg = cnt_usr == 0 ? int.MaxValue : dis_usr / cnt_usr;
            //Console.WriteLine(dis);
            // 平均距离和剩余棋子数对收益的影响 2:1
            return ((usr_avg - ai_avg) * 10 + (cnt_ai - cnt_usr) * 5);
        }
    }
}
