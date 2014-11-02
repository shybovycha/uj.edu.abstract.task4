using System;
using System.Collections;
using System.Collections.Generic;

namespace task4
{
    public abstract class RoomComparer : IComparer<Room>
    {
        public RoomComparer()
        {
        }

        public abstract int Compare(Room x, Room y);
    }
}

