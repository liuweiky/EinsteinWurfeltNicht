using EinsteinWurfeltNicht.Model;
using EinsteinWurfeltNicht.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EinsteinWurfeltNicht
{
    public partial class MainForm : Form
    {
        ChessBoardView chessBoard;
        IPlayer player1, player2;
        public MainForm()
        {
            InitializeComponent();
            chessBoard = new ChessBoardView(chessPanel.Height, chessPanel.Width);
            chessPanel.Controls.Add(chessBoard);
            player1 = new AiPlayer();
            player2 = new UserPlayer();
            chessBoard.SetPlayer1(player1);
            chessBoard.SetPlayer2(player2);
        }
    }
}
