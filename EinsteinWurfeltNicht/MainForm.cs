using EinsteinWurfeltNicht.Controller;
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
        ChessBoardView chessBoardView;
        EwnController mainController;

        public MainForm(EwnController controller)
        {
            InitializeComponent();
            mainController = controller;
            chessBoardView = new ChessBoardView(controller, chessPanel.Height, chessPanel.Width);
            mainController.chessBoardView = chessBoardView;
            mainController.diceLabel = diceLabel;
            diceLabel.Text = mainController.moveChessNum.ToString();
            mainController.player1Label = playerLabel1;
            mainController.player2Label = playerLabel2;
            chessPanel.Controls.Add(chessBoardView);
            chessBoardView.SetPlayer1(mainController.player1);
            chessBoardView.SetPlayer2(mainController.player2);
        }
    }
}
