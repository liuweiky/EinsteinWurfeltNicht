using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EinsteinWurfeltNicht.Model;

namespace EinsteinWurfeltNicht.View
{
    class ChessBoardView : FlowLayoutPanel, IModelObserver
    {
        public const int CHESS_BOARD_SIZE = 5;
        private Button[,] chessBoardLattices;

        private IPlayer player1, player2;

        public ChessBoardView(int height, int width)
        {
            this.Height = height;
            this.Width = width;
            chessBoardLattices = new Button[CHESS_BOARD_SIZE, CHESS_BOARD_SIZE];
            for (int i = 0; i < CHESS_BOARD_SIZE; i++)
            {
                for (int j = 0; j < CHESS_BOARD_SIZE; j++)
                {
                    chessBoardLattices[i, j] = new Button();
                    chessBoardLattices[i, j].Height = this.Height / CHESS_BOARD_SIZE - this.Margin.Vertical;
                    chessBoardLattices[i, j].Width = this.Width / CHESS_BOARD_SIZE - this.Margin.Horizontal;
                    Controls.Add(chessBoardLattices[i, j]);
                }
            }
            ResetLattices();
        }

        public void SetPlayer1(IPlayer p)
        {
            player1 = p;
            Update(null);
        }

        public void SetPlayer2(IPlayer p)
        {
            player2 = p;
            Update(null);
        }

        public void Update(IModelObservable observable)
        {
            ResetLattices();
            if (player1 != null)
            {
                foreach (Object o in player1.chesses)
                {
                    Chess c = o as Chess;
                    SetButtonStyle(chessBoardLattices[c.posId / CHESS_BOARD_SIZE, c.posId % CHESS_BOARD_SIZE], c);
                }
            }
            if (player2 != null)
            {
                foreach (Object o in player2.chesses)
                {
                    Chess c = o as Chess;
                    SetButtonStyle(chessBoardLattices[c.posId / CHESS_BOARD_SIZE, c.posId % CHESS_BOARD_SIZE], c);
                }
            }
        }

        private void ResetLattices()
        {
            for (int i = 0; i < CHESS_BOARD_SIZE; i++)
            {
                for (int j = 0; j < CHESS_BOARD_SIZE; j++)
                {
                    chessBoardLattices[i, j].ForeColor = Color.White;
                    chessBoardLattices[i, j].BackColor = Color.White;
                    chessBoardLattices[i, j].Font = new Font("宋体", 24);
                    chessBoardLattices[i, j].Text = "";
                }
            }
        }

        private void SetButtonStyle(Button b, Chess c)
        {
            b.Text = c.chessNum.ToString();
            switch(c.owner)
            {
                case ChessOwner.EMPTY:
                    b.BackColor = Color.White;
                    break;
                case ChessOwner.AI:
                    b.BackColor = Color.Blue;
                    break;
                case ChessOwner.PLAYER:
                    b.BackColor = Color.Red;
                    break;
            }
        }
    }
}
