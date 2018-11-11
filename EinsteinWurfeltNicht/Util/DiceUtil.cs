using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EinsteinWurfeltNicht.Util
{
    public class DiceUtil
    {
        static Random RANDOM;
        static DiceUtil()
        {
            RANDOM = new Random(1);
        }
        public static int GetChessNum()
        {
            return RANDOM.Next(0, 5);
        }
    }
}
