using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EinsteinWurfeltNicht.View;

namespace EinsteinWurfeltNicht.Model
{
    class AiPlayer : IPlayer, IModelObservable
    {
        ArrayList observers;

        public AiPlayer()
        {
            observers = new ArrayList();
            chesses = new ArrayList();
            numPosHash = new int[CHESS_NUM] {0, 1, 2, 5, 6, 10};
            for (int i = 0; i < CHESS_NUM; i++)
                chesses.Add(new Chess(ChessOwner.AI, numPosHash[i], i));
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
