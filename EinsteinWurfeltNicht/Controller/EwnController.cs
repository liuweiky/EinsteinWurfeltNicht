using EinsteinWurfeltNicht.Model;
using EinsteinWurfeltNicht.Util;
using EinsteinWurfeltNicht.Util.Algorithm;
using EinsteinWurfeltNicht.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EinsteinWurfeltNicht.Controller
{
    public class EwnController
    {
        enum Turn { PLAYER1, PLAYER2 };

        public ChessBoardView chessBoardView;
        public IPlayer player1, player2;
        public EventHandler chessButtonHandler;
        public Label diceLabel;
        Turn turn;
        public int moveChessNum;

        public Label player1Label;
        public Label player2Label;
        public EwnController()
        {
            turn = Turn.PLAYER2;
            moveChessNum = DiceUtil.GetChessNum();
            player1 = new AiPlayer();
            player2 = new UserPlayer();
            chessButtonHandler = new System.EventHandler(OnButtonClick);
        }

        public void Start()
        {
            
        }

        private ArrayList GetMoveRange(IPlayer player, int num)
        {
            ArrayList arrayList = new ArrayList();

            int curPos = (player.Chesses[num] as Chess).posId;

            int m = curPos / ChessBoardView.CHESS_BOARD_SIZE;
            int n = curPos % ChessBoardView.CHESS_BOARD_SIZE;

            if (player == player1)
            {
                /*if (m < ChessBoardView.CHESS_BOARD_SIZE - 1 && 
                    (chessBoardView.chessBoardHash[m + 1, n] == ChessOwner.PLAYER2 ||
                    chessBoardView.chessBoardHash[m + 1, n] == ChessOwner.EMPTY))
                    arrayList.Add((m + 1) * ChessBoardView.CHESS_BOARD_SIZE + n);
                if (n < ChessBoardView.CHESS_BOARD_SIZE - 1 &&
                    (chessBoardView.chessBoardHash[m, n + 1] == ChessOwner.PLAYER2 ||
                    chessBoardView.chessBoardHash[m, n + 1] == ChessOwner.EMPTY))
                    arrayList.Add(m * ChessBoardView.CHESS_BOARD_SIZE + n + 1);
                if (m < ChessBoardView.CHESS_BOARD_SIZE - 1 &&
                    n < ChessBoardView.CHESS_BOARD_SIZE - 1 && 
                    (chessBoardView.chessBoardHash[m + 1, n + 1] == ChessOwner.PLAYER2 ||
                    chessBoardView.chessBoardHash[m + 1, n + 1] == ChessOwner.EMPTY))
                    arrayList.Add((m + 1) * ChessBoardView.CHESS_BOARD_SIZE + n + 1);*/
                /*if (curPos < ChessBoardView.CHESS_BOARD_SIZE * ChessBoardView.CHESS_BOARD_SIZE - 1)
                    arrayList.Add(curPos + 1);
                if (curPos < ChessBoardView.CHESS_BOARD_SIZE * ChessBoardView.CHESS_BOARD_SIZE - ChessBoardView.CHESS_BOARD_SIZE)
                    arrayList.Add(curPos + ChessBoardView.CHESS_BOARD_SIZE);
                if (curPos < ChessBoardView.CHESS_BOARD_SIZE * ChessBoardView.CHESS_BOARD_SIZE - ChessBoardView.CHESS_BOARD_SIZE - 1)
                    arrayList.Add(curPos + ChessBoardView.CHESS_BOARD_SIZE + 1);*/
                if (curPos < ChessBoardView.CHESS_BOARD_SIZE * ChessBoardView.CHESS_BOARD_SIZE - ChessBoardView.CHESS_BOARD_SIZE - 1)
                {
                    arrayList.Add(curPos + ChessBoardView.CHESS_BOARD_SIZE + 1);
                    arrayList.Add(curPos + ChessBoardView.CHESS_BOARD_SIZE);
                    arrayList.Add(curPos + 1);
                } else if (curPos < ChessBoardView.CHESS_BOARD_SIZE * ChessBoardView.CHESS_BOARD_SIZE - ChessBoardView.CHESS_BOARD_SIZE)
                {
                    arrayList.Add(curPos + ChessBoardView.CHESS_BOARD_SIZE);
                    arrayList.Add(curPos + 1);
                } else if (curPos < ChessBoardView.CHESS_BOARD_SIZE * ChessBoardView.CHESS_BOARD_SIZE - 1)
                {
                    arrayList.Add(curPos + 1);
                }
            } else
            {
                /*if (m > 0 &&
                    (chessBoardView.chessBoardHash[m - 1, n] == ChessOwner.PLAYER1 ||
                    chessBoardView.chessBoardHash[m - 1, n] == ChessOwner.EMPTY))
                    arrayList.Add((m - 1) * ChessBoardView.CHESS_BOARD_SIZE + n);
                if (n > 0 &&
                    (chessBoardView.chessBoardHash[m, n - 1] == ChessOwner.PLAYER1 ||
                    chessBoardView.chessBoardHash[m, n - 1] == ChessOwner.EMPTY))
                    arrayList.Add(m * ChessBoardView.CHESS_BOARD_SIZE + n - 1);
                if (m > 0 &&
                    n > 0 &&
                    (chessBoardView.chessBoardHash[m - 1, n - 1] == ChessOwner.PLAYER1 ||
                    chessBoardView.chessBoardHash[m - 1, n - 1] == ChessOwner.EMPTY))
                    arrayList.Add((m - 1) * ChessBoardView.CHESS_BOARD_SIZE + n - 1);*/
                if (curPos >= ChessBoardView.CHESS_BOARD_SIZE + 1)
                {
                    arrayList.Add(curPos - ChessBoardView.CHESS_BOARD_SIZE - 1);
                    arrayList.Add(curPos - ChessBoardView.CHESS_BOARD_SIZE);
                    arrayList.Add(curPos - 1);
                } else if (curPos >= ChessBoardView.CHESS_BOARD_SIZE)
                {
                    arrayList.Add(curPos - ChessBoardView.CHESS_BOARD_SIZE);
                    arrayList.Add(curPos - 1);
                } else if (curPos >= 1)
                {
                    arrayList.Add(curPos - 1);
                }
            }

            return arrayList;
        }

        private void AiTurn()
        {
            moveChessNum = DiceUtil.GetChessNum();
            turn = Turn.PLAYER1;
            IPlayer p = player1;
            diceLabel.Text = moveChessNum.ToString();
            player1Label.Visible = true;
            player2Label.Visible = false;

            

            ArrayList candidates = new ArrayList();
            Minimax mm;
            int pos;

            Application.DoEvents();
            //Thread.Sleep(200);
            if ((p.Chesses[moveChessNum] as Chess).state == ChessState.ALIVE)
                candidates.Add(moveChessNum);
            if (candidates.Count != 0)
            {
                mm = new Minimax(this);

                pos = mm.Calc((p.Chesses[moveChessNum] as Chess).posId);
                Thread.Sleep(1000);
                Console.WriteLine((pos / ChessBoardView.CHESS_BOARD_SIZE).ToString() + ", " + (pos % ChessBoardView.CHESS_BOARD_SIZE).ToString());
                MoveTo(pos);
                return;
            }
            /*int dis = 1;
            while (candidates.Count == 0 && dis <= 5)
            {
                if (moveChessNum + dis <= 5 && (p.Chesses[moveChessNum + dis] as Chess).state == ChessState.ALIVE)
                    candidates.Add(moveChessNum + dis);
                if (moveChessNum - dis >= 0 && (p.Chesses[moveChessNum - dis] as Chess).state == ChessState.ALIVE)
                    candidates.Add(moveChessNum - dis);
                dis++;
            }*/

            for (int i = moveChessNum + 1; i <= 5; i++)
            {
                if ((p.Chesses[i] as Chess).state == ChessState.ALIVE)
                {
                    candidates.Add(i);
                    break;
                }
            }

            for (int i = moveChessNum - 1; i >= 0; i--)
            {
                if ((p.Chesses[i] as Chess).state == ChessState.ALIVE)
                {
                    candidates.Add(i);
                    break;
                }
            }

            if (candidates.Count == 0)
            {
                MessageBox.Show("Player2 Win");
                return;
            }

            double bestEval = double.MinValue;
            int bestMoveNum = (int)candidates[0];

            Console.Write("Candidates: ");
            for (int i = 0; i < candidates.Count; i++)
            {
                Console.Write((int)candidates[i]);
                Console.Write("\t");
            }
            Console.WriteLine();
                for (int i = 0; i < candidates.Count; i++)
            {
                moveChessNum = (int)candidates[i];
                ArrayList range = GetMoveRange(player1, moveChessNum);
                int curPos = (p.Chesses[moveChessNum] as Chess).posId;

                int cm = curPos / ChessBoardView.CHESS_BOARD_SIZE;
                int cn = curPos % ChessBoardView.CHESS_BOARD_SIZE;

                for (int j = 0; j < range.Count; j++)
                {
                    int pid = (int)range[j];

                    int pm = pid / ChessBoardView.CHESS_BOARD_SIZE;
                    int pn = pid % ChessBoardView.CHESS_BOARD_SIZE;

                    ChessOwner obk = chessBoardView.chessBoardHash[pm, pn];
                    chessBoardView.chessBoardHash[cm, cn] = ChessOwner.EMPTY;
                    chessBoardView.chessBoardHash[pm, pn] = ChessOwner.PLAYER1;
                    mm = new Minimax(this);
                    if (bestEval < mm.Eval())
                    {
                        bestEval = mm.Eval();
                        bestMoveNum = moveChessNum;
                    }
                    chessBoardView.chessBoardHash[pm, pn] = obk;
                    chessBoardView.chessBoardHash[cm, cn] = ChessOwner.PLAYER1;
                }
                
            }

            moveChessNum = bestMoveNum;
            mm = new Minimax(this);
            pos = mm.Calc((p.Chesses[moveChessNum] as Chess).posId);
            Thread.Sleep(1000);
            Console.WriteLine((pos / ChessBoardView.CHESS_BOARD_SIZE).ToString() + ", " + (pos % ChessBoardView.CHESS_BOARD_SIZE).ToString());
            MoveTo(pos);

            return;
        }

        private void NextTurn()
        {
            if (chessBoardView.chessBoardHash[0, 0] == ChessOwner.PLAYER2)
            {
                MessageBox.Show("Player2 Win");
                return;
            }
            Thread.Sleep(100);
            AiTurn();
            if (chessBoardView.chessBoardHash[ChessBoardView.CHESS_BOARD_SIZE - 1, ChessBoardView.CHESS_BOARD_SIZE - 1] == ChessOwner.PLAYER1)
            {
                MessageBox.Show("Player1 Win");
                return;
            }
            turn = Turn.PLAYER2;
            moveChessNum = DiceUtil.GetChessNum();
            IPlayer p = player2;

            player1Label.Visible = false;
            player2Label.Visible = true;

            diceLabel.Text = moveChessNum.ToString();

            ArrayList candidates = new ArrayList();

            if ((p.Chesses[moveChessNum] as Chess).state == ChessState.ALIVE)
                candidates.Add(moveChessNum);
            if (candidates.Count != 0)
            {
                return;
            }
            /*int dis = 1;
            while (candidates.Count == 0 && dis <= 5)
            {
                if (moveChessNum + dis <= 5 && (p.Chesses[moveChessNum + dis] as Chess).state == ChessState.ALIVE)
                    candidates.Add(moveChessNum + dis);
                if (moveChessNum - dis >= 0 && (p.Chesses[moveChessNum - dis] as Chess).state == ChessState.ALIVE)
                    candidates.Add(moveChessNum - dis);
                dis++;
            }*/

            for (int i = moveChessNum + 1; i <= 5; i++)
            {
                if ((p.Chesses[i] as Chess).state == ChessState.ALIVE)
                {
                    candidates.Add(i);
                    break;
                }
            }

            for (int i = moveChessNum - 1; i >= 0; i--)
            {
                if ((p.Chesses[i] as Chess).state == ChessState.ALIVE)
                {
                    candidates.Add(i);
                    break;
                }
            }

            if (candidates.Count == 0)
                MessageBox.Show(p == player1 ? "Player2 Win" : "Player1 Win");
            else if (candidates.Count == 2)
            {
                DialogResult dialogRes = MessageBox.Show("2 candidates(" + candidates[0].ToString() + ", " + candidates[1].ToString() + "). Select " + candidates[0].ToString() + " ？", "Select", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogRes == DialogResult.Yes)
                {
                    moveChessNum = (int)candidates[0];
                } else
                {
                    moveChessNum = (int)candidates[1];
                }
            } else if (candidates.Count == 1)
            {
                MessageBox.Show("Can only move " + (int)candidates[0] + ".");
                moveChessNum = (int)candidates[0];
            }
        }

        public bool MoveTo(int posId)
        {
            IPlayer p = (turn == Turn.PLAYER1) ? player1 : player2;
            IPlayer tp = (turn == Turn.PLAYER1) ? player2 : player1;
            if (!GetMoveRange(p, moveChessNum).Contains(posId))
            {
                MessageBox.Show("Cannot move here");
                return false;
            }

            for (int i = 0; i < p.Chesses.Count; i++)
                if ((p.Chesses[i] as Chess).posId == posId)
                {
                    p.SetChessEliminated(i);
                    //break;
                }

            for (int i = 0; i < tp.Chesses.Count; i++)
                if ((tp.Chesses[i] as Chess).posId == posId)
                {
                    tp.SetChessEliminated(i);
                    //break;
                }

            p.SetChessPos(moveChessNum, posId);
            if (chessBoardView.chessBoardHash[0, 0] == ChessOwner.PLAYER2)
            {
                MessageBox.Show("Player2 Win");
            }
            return true;
        }

        private void OnButtonClick(object sender, EventArgs args)
        {
            for (int i = 0; i < ChessBoardView.CHESS_BOARD_SIZE; i++)
            {
                for (int j = 0; j < ChessBoardView.CHESS_BOARD_SIZE; j++)
                {
                    if (sender == chessBoardView.chessBoardLattices[i, j])
                    {
                        //MessageBox.Show("Button {" + (i * ChessBoardView.CHESS_BOARD_SIZE + j).ToString() + "} clicked");
                        if (MoveTo(i * ChessBoardView.CHESS_BOARD_SIZE + j))
                            NextTurn();
                        break;
                    }
                }
            }
        }
    }
}
