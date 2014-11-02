using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace task4
{
    public class Hotel<TRoom, TComparer> : IHotel, IEnumerable<TRoom>
        where TRoom : class, IRoom 
        where TComparer : IComparer<TRoom>, new()
    {
        public List<TRoom> Rooms;
        protected TComparer Comparer;
        protected int _stars;

        public int Stars 
        { 
            get 
            {
                return _stars;
            } 
        }

        public Hotel()
        {
            _stars = 0;
            Rooms = new List<TRoom>();
            Comparer = new TComparer();
        }

        public Hotel(int stars, TRoom room)
        {
            Rooms = new List<TRoom>();
            _stars = stars;
            Comparer = new TComparer();
            Rooms.Add(room);
        }

        public Hotel(int stars, TRoom[] rooms)
        {
            Rooms = new List<TRoom>();
            _stars = stars;
            Rooms.AddRange(rooms);
            Comparer = new TComparer();
            Rooms.Sort(Comparer);
        }

        public void Add(TRoom room)
        {
            Rooms.Add(room);
            Rooms.Sort(Comparer);
        }

        public bool IsFree(TRoom room, DateTime d1, DateTime d2)
        {
            // TODO
            return false;
        }

        public void Book(TRoom room, DateTime d1, DateTime d2)
        {
            // TODO
        }

        public IEnumerable<TRoom> Where(Func<TRoom, bool> f)
        {
            return Enumerable.Where(Rooms, f);
        }

        public IEnumerator<TRoom> GetEnumerator()
        {
            /*foreach (TRoom i in Rooms)
            {
                if (i == null)
                {
                    break;
                }

                yield return i;
            }*/

            return Rooms.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

