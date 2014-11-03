using System;

namespace task4
{
    public interface IRoom
    {
        DateTime BookedFrom { get; }
        DateTime BookedTo { get; }
        double SquareMeters { get; }
        void SetBookedFrom(DateTime v);
        void SetBookedTo(DateTime v);
    }
}

