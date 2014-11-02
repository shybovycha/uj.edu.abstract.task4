using System;

namespace task4
{
    public class Room : IRoom
    {
        protected RoomStandard Type;

        public double SquareMeters 
        { 
            get
            {
                return _square_meters;
            }
        }

        protected double _square_meters;

        public Room(double square, RoomStandard type)
        {
            _square_meters = square;
            Type = type;
        }
    }
}

