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
            int seed = Guid.NewGuid().GetHashCode();
            RANDOM = new Random(seed);
        }
        public static int GetChessNum()
        {
            return (RANDOM.Next() % 6);
        }
        public static int GetRandomNum()
        {
            return RANDOM.Next();
        }
    }
}
