using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EinsteinWurfeltNicht.Model
{
    public interface IPlayer : IModelObservable
    {
        ArrayList Chesses { get;}

        void SetChessPos(int chessNum, int posId);
    }
}
