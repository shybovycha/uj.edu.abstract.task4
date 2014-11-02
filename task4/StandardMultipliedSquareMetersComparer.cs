using System;
using System.Collections.Generic;

namespace task4
{
    public class StandardMultipliedSquareMetersComparer : RoomComparer
    {
        public StandardMultipliedSquareMetersComparer()
        {
        }

        public override int Compare(Room a, Room b)
        {
            if (((int) a.SquareMeters % (int) b.SquareMeters) == 0)
                return 0;

            return Math.Sign(b.SquareMeters - a.SquareMeters);
        }
    }
}

