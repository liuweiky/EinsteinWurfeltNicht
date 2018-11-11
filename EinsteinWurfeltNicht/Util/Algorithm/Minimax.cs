using EinsteinWurfeltNicht.Model;
using EinsteinWurfeltNicht.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EinsteinWurfeltNicht.Util.Algorithm
{
    public class Minimax
    {
        public ChessOwner[,] chessBoard;
        public Minimax()
        {
            chessBoard = new ChessOwner[ChessBoardView.CHESS_BOARD_SIZE, ChessBoardView.CHESS_BOARD_SIZE];
        }


    }
}
