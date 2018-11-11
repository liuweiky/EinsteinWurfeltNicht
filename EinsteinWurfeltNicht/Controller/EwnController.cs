using EinsteinWurfeltNicht.Model;
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
        public ChessBoardView chessBoardView;
        public IPlayer player1, player2;
        public EventHandler chessButtonHandler;

        public EwnController()
        {
            player1 = new AiPlayer();
            player2 = new UserPlayer();
            chessButtonHandler = new System.EventHandler(OnButtonClick);
        }

        public void Start()
        {
            
        }

        private bool CanMove(IPlayer player, int moveChessNum, int posId)
        {
            ArrayList range = new ArrayList();

            foreach (Object o in player1.Chesses)
            {
                range.Add((o as Chess).posId);
            }
            foreach (Object o in player2.Chesses)
            {
                range.Add((o as Chess).posId);
            }

            if (range.Contains((int)posId))
                return false;

            int curPos = (player.Chesses[moveChessNum] as Chess).posId;

            if (player == player1)
            {
                if (posId == curPos + ChessBoardView.CHESS_BOARD_SIZE ||
                    posId == curPos + ChessBoardView.CHESS_BOARD_SIZE + 1 ||
                    posId == curPos + 1)
                    return true;
                return false;
            } else
            {
                if (posId == curPos - ChessBoardView.CHESS_BOARD_SIZE ||
                    posId == curPos - ChessBoardView.CHESS_BOARD_SIZE - 1 ||
                    posId == curPos - 1)
                    return true;
                return false;
            }
        }

        public void MoveTo(int posId)
        {

            int movChessNum = 0;
            if (!CanMove(player2, movChessNum, posId))
            {
                MessageBox.Show("Cannot move here");
                return;
            }

            player2.SetChessPos(movChessNum, posId);
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
                        MoveTo(i * ChessBoardView.CHESS_BOARD_SIZE + j);
                    }
                }
            }
        }
    }
}
