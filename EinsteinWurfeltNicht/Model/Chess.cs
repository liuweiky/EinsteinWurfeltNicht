using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EinsteinWurfeltNicht.View;

namespace EinsteinWurfeltNicht.Model
{
    enum ChessOwner {AI, PLAYER, EMPTY};
    enum ChessState {ELIMINATED, ALIVE}
    class Chess
    {
        public readonly ChessOwner owner;
        public int posId;   // 位置id = row * CHESS_BOARD_SIZE + colum
        public readonly int chessNum;
        public ChessState state;

        public Chess(ChessOwner co, int pos, int n)
        {
            owner = co;
            posId = pos;
            chessNum = n;
            state = ChessState.ALIVE;
        }
    }
}
