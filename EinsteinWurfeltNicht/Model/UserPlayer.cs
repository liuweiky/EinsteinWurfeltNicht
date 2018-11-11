using EinsteinWurfeltNicht.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EinsteinWurfeltNicht.Model
{
    class UserPlayer : IPlayer, IModelObservable
    {
        ArrayList observers;

        public UserPlayer()
        {
            observers = new ArrayList();
            chesses = new ArrayList();
            numPosHash = new int[CHESS_NUM] { 14, 18, 19, 22, 23, 24 };
            for (int i = 0; i < CHESS_NUM; i++)
                chesses.Add(new Chess(ChessOwner.PLAYER, numPosHash[i], i));
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
