using EinsteinWurfeltNicht.Model;
using EinsteinWurfeltNicht.Util;
using EinsteinWurfeltNicht.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        int moveChessNum;

        public Label player1Label;
        public Label player2Label;
        public EwnController()
        {
            turn = Turn.PLAYER1;
            moveChessNum = 5;
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
                if (m < ChessBoardView.CHESS_BOARD_SIZE - 1 && 
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
                    arrayList.Add((m + 1) * ChessBoardView.CHESS_BOARD_SIZE + n + 1);
            } else
            {
                if (m > 0 &&
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
                    arrayList.Add((m - 1) * ChessBoardView.CHESS_BOARD_SIZE + n - 1);
            }

            return arrayList;
        }

        public void NextTurn()
        {
            turn = (turn == Turn.PLAYER1) ? Turn.PLAYER2 : Turn.PLAYER1;
            moveChessNum = DiceUtil.GetChessNum();
            IPlayer p = (turn == Turn.PLAYER1) ? player1 : player2;

            if (p == player1)
            {
                player1Label.Visible = true;
                player2Label.Visible = false;
            } else
            {
                player1Label.Visible = false;
                player2Label.Visible = true;
            }

            while ((p.Chesses[moveChessNum] as Chess).state == ChessState.ELIMINATED)
                moveChessNum = DiceUtil.GetChessNum();

            diceLabel.Text = moveChessNum.ToString();

            bool canMove = false;

            if (GetMoveRange(p, moveChessNum).Count > 0)
                canMove = true;
            int dis = 1;
            while (!canMove)
            {
                if (GetMoveRange(p, moveChessNum + dis).Count > 0)
                    canMove = true;
                if (canMove)
                {
                    moveChessNum = moveChessNum + dis;
                    break;
                }
                if (GetMoveRange(p, moveChessNum - dis).Count > 0)
                        canMove = true;
                if (canMove)
                {
                    moveChessNum = moveChessNum - dis;
                    break;
                }
                dis++;
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
            
            for (int i = 0; i < tp.Chesses.Count; i++)
                if ((tp.Chesses[i] as Chess).posId == posId)
                {
                    tp.SetChessEliminated(i);
                    break;
                }

            p.SetChessPos(moveChessNum, posId);
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
