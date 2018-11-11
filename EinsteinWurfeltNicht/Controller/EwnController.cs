using EinsteinWurfeltNicht.Model;
using EinsteinWurfeltNicht.View;
using System;
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

        public void MoveTo(int posId)
        {
            bool hasChess = false;
            foreach (Object o in player1.Chesses)
            {
                if ((o as Chess).posId == posId)
                    hasChess = true;
            }
            foreach (Object o in player2.Chesses)
            {
                if ((o as Chess).posId == posId)
                    hasChess = true;
            }

            if (hasChess)
            {
                MessageBox.Show("Cannot move here");
                return;
            }
            /*int movChessNum = 0;

            player1.SetChessPos(movChessNum, posId);*/

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
