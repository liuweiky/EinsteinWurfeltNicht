using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EinsteinWurfeltNicht.Model
{
    abstract class IPlayer
    {
        public const int CHESS_NUM = 6;
        public ArrayList chesses;
        protected int[] numPosHash;
    }
}
