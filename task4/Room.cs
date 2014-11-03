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

        protected DateTime _booked_from;
        protected DateTime _booked_to;

        public DateTime BookedFrom 
        {
            get 
            {
                return _booked_from;
            }
        }

        public DateTime BookedTo
        {
            get 
            {
                return _booked_to;
            }
        }

        public Room(double square, RoomStandard type)
        {
            _square_meters = square;
            Type = type;
        }

        public void SetBookedFrom(DateTime v)
        {
            _booked_from = v;
        }

        public void SetBookedTo(DateTime v)
        {
            _booked_to = v;
        }
    }
}
