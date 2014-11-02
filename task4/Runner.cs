using System;

namespace task4
{
    public class Runner
    {
        public Runner()
        {
        }

        public static int Main(String[] args)
        {
            //var smc = new StandardMultipliedSquareMetersComparer();
            var r1 = new Room(35.0, RoomStandard.Apartment);
            var r2 = new Room(25.0, RoomStandard.Standard);
            var r3 = new Room(40.0, RoomStandard.Student); // 40*1x=40x
            var rooms = new Room[] { r1, r2, r3 };
            var h1 = new Hotel<Room, SquareMetersComparer>(5, rooms);
            Console.WriteLine("Rooms: {0}", String.Join(",", h1.Rooms));
            //Assert.IsTrue(h1.AsEnumerable().SequenceEqual(new Room[] { r2, r1, r3 }));

            return 0;
        }
    }
}

