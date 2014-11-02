using System;
using System.Collections.Generic;

namespace task4
{
    public class SquareMetersComparer : RoomComparer
    {
        public SquareMetersComparer()
        {
        }

        public override int Compare(Room a, Room b)
        {
            return Math.Sign(a.SquareMeters - b.SquareMeters);
        }
    }
}

