using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EinsteinWurfeltNicht.View;

namespace EinsteinWurfeltNicht.Model
{
    public class AiPlayer : IPlayer
    {
        const int CHESS_NUM = 6;
        ArrayList observers;

        ArrayList chesses;
        public ArrayList Chesses
        {
            get { return chesses; }
        }

        public AiPlayer()
        {
            observers = new ArrayList();
            chesses = new ArrayList();
            int [] numPosHash = new int[CHESS_NUM] {0, 1, 2, 5, 6, 10};
            for (int i = 0; i < CHESS_NUM; i++)
                chesses.Add(new Chess(ChessOwner.AI, numPosHash[i], i));
        }

        public void SetChessPos(int chessNum, int posId)
        {
            (chesses[chessNum] as Chess).posId = posId;
            Notify();
        }

        public void Attatch(IModelObserver observer)
        {
            observers.Add(observer);
        }

        public void Notify()
        {
            foreach (Object o in observers)
            {
                IModelObserver observer = o as IModelObserver;
                observer.Update(this);
            }
        }
    }
}
