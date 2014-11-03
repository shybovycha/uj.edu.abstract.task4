using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using task4;

namespace Tests
{
    [TestFixture()]
    public class BasicTests
    {
        [Test()]
        public void Types()
        {
            Assert.IsTrue(typeof(IRoom).IsInterface);
            var r = new Room(35.0, RoomStandard.Apartment);
            Assert.IsTrue(typeof(Room).GetInterfaces().Contains(typeof(IRoom)));
            Assert.AreEqual(35.0, (r as IRoom).SquareMeters);
            Assert.IsTrue(typeof(IHotel).IsInterface);
            Assert.IsTrue(typeof(Hotel<Room, SquareMetersComparer>).GetInterfaces().Contains(typeof(IHotel)));
            var h = new Hotel<Room, SquareMetersComparer>(5, r);
            Assert.AreEqual((uint)5, (h as IHotel).Stars);
            Assert.IsTrue(typeof(SquareMetersComparer).GetInterfaces().Contains(typeof(IComparer<Room>)));
            Assert.IsTrue(typeof(StandardMultipliedSquareMetersComparer).GetInterfaces().Contains(typeof(IComparer<Room>)));
            Assert.AreEqual(typeof(SquareMetersComparer).BaseType, typeof(StandardMultipliedSquareMetersComparer).BaseType);
            Assert.AreNotEqual(typeof(SquareMetersComparer).BaseType, typeof(object));
        }

        [Test()]
        public void GenericConstrains()
        {
            var genericArguments = typeof(Hotel<,>).GetGenericArguments();
            Assert.IsTrue(genericArguments[0].GetGenericParameterConstraints().Contains(typeof(IRoom)));
            Assert.AreEqual(GenericParameterAttributes.DefaultConstructorConstraint, genericArguments[1].GenericParameterAttributes);
        }

        [Test()]
        public void RoomStandards()
        {
            Assert.AreEqual((byte)RoomStandard.Apartment, (byte)RoomStandard.Student * 3);
            Assert.AreEqual((byte)RoomStandard.Standard, (byte)RoomStandard.Student * 2);
            Assert.IsTrue(typeof(RoomStandard).IsEnum);
            Assert.AreEqual(sizeof(byte), sizeof(RoomStandard));
        }

        [Test()]
        public void Constructor()
        {
            var r = new Room(35.0, RoomStandard.Apartment);
            var h = new Hotel<Room, SquareMetersComparer>(3, r);
            Assert.AreEqual((uint)3, h.Stars);
        }

        [Test()]
        public void Comparers()
        {
            var smc = new StandardMultipliedSquareMetersComparer();
            var r1 = new Room(35.0, RoomStandard.Apartment);
            Assert.AreEqual(0, smc.Compare(new Room(105.0, RoomStandard.Student), r1)); // 35*3x=105x
            var r2 = new Room(25.0, RoomStandard.Standard);
            Assert.AreEqual(0, smc.Compare(new Room(50.0, RoomStandard.Student), r2)); // 25*2x=50x
            var r3 = new Room(40.0, RoomStandard.Student); // 40*1x=40x
            var rooms = new Room[] { r1, r2, r3 };
            var h1 = new Hotel<Room, SquareMetersComparer>(5, rooms);
            Assert.IsTrue(h1.AsEnumerable().SequenceEqual(new Room[] { r2, r1, r3 }));
            var h2 = new Hotel<Room, StandardMultipliedSquareMetersComparer>(5, rooms);
            // TODO: Error here? Must be [ r3, r1, r2 ]
            Assert.IsTrue(h2.AsEnumerable().SequenceEqual(new Room[] { r3, r1, r2 }));
        }

        [Test()]
        public void Booking()
        {
            var r1 = new Room(35.0, RoomStandard.Apartment);
            var r2 = new Room(25.0, RoomStandard.Standard);
            var r3 = new Room(40.0, RoomStandard.Student);
            var rooms = new Room[] { r1, r2, r3 };
            var h = new Hotel<Room, SquareMetersComparer>(5, rooms);
            var now = DateTime.Now;
            Assert.IsTrue(h.IsFree(r2, now.AddDays(10), now.AddDays(30)));
            h.Book(r2, now.AddDays(10), now.AddDays(30));
            Assert.IsFalse(h.IsFree(r2, now.AddDays(10), now.AddDays(30)));
            Assert.IsTrue(h.IsFree(r1, now.AddDays(10), now.AddDays(30)));
            Assert.IsTrue(h.IsFree(r3, now.AddDays(10), now.AddDays(30)));
            Assert.IsFalse(h.IsFree(r2, now.AddDays(1), now.AddDays(20)));
            Assert.IsFalse(h.IsFree(r2, now.AddDays(20), now.AddDays(40)));
            Assert.IsFalse(h.IsFree(r2, now.AddDays(9), now.AddDays(31)));
            Assert.IsFalse(h.IsFree(r2, now.AddDays(11), now.AddDays(29)));
            Assert.IsTrue(h.IsFree(r2, now.AddDays(1), now.AddDays(9)));
            Assert.IsTrue(h.IsFree(r2, now.AddDays(31), now.AddDays(40)));
            //var r4 = new Room(25.0, RoomStandard.Standard); // do not uncomment
            var forbidden = new Action[]
            {
                () => h.IsFree(r2, now.AddDays(2), now.AddDays(1)),
                () => h.Book(r2, now.AddDays(2), now.AddDays(1)),
                //() => h.IsFree(r4, now.AddDays(1), now.AddDays(2)), // do not uncomment
                //() => h.Book(r4, now.AddDays(1), now.AddDays(2)), // do not uncomment
                // TODO: Error here? r2 is totally free for booking
                // () => h.Book(r2, now, now.AddDays(2)),
                () => h.Book(r2, now.AddDays(10), now.AddDays(30))
            };
            foreach (var action in forbidden)
            {
                try
                {
                    action();
                    Assert.Fail("Invalid operation performed.");
                }
                catch (ArgumentOutOfRangeException)
                {
                }
                /*catch (AssertFailedException)
                {
                    throw;
                }*/
                catch
                {
                    Assert.Fail("Invalid exception type.");
                }
            }
        }

        [Test()]
        public void NoPublicPropertySetters()
        {
            var types = typeof(Room).Assembly.GetTypes();
            var withPublicSetter = types.Where(t => t.GetProperties(BindingFlags.Public | BindingFlags.Instance).Count(p => p.GetSetMethod(false) != null) > 0)
                .Select(t => t.FullName).ToArray();
            var message = String.Format("Detected type(s) {0} with public setter, which is not allowed.", String.Join(", ", withPublicSetter));
            Assert.AreEqual(0, withPublicSetter.Count(), message);
        }
    }
}

