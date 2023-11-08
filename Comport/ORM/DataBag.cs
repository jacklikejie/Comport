using System.Collections.Generic;
using System;
using System.Collections;

namespace Comport.ORM
{
    public class DataBag : IEnumerable<object>, IEnumerable
    {
        private Dictionary<Type, object> dict = new Dictionary<Type, object>(1);

        internal object this[Type type]
        {
            get
            {
                return Get(type);
            }
            set
            {
                dict[type] = value;
            }
        }

        internal void Combine(DataBag dataBag)
        {
            if (dataBag == null)
            {
                return;
            }

            foreach (KeyValuePair<Type, object> item in dataBag.dict)
            {
                dict[item.Key] = item.Value;
            }
        }

        public object Get(Type type)
        {
            return dict[type];
        }

        public static explicit operator int(DataBag bag)
        {
            return (int)bag.Get(typeof(int));
        }

        public static explicit operator float(DataBag bag)
        {
            return (float)bag.Get(typeof(float));
        }

        public static explicit operator double(DataBag bag)
        {
            return (double)bag.Get(typeof(double));
        }

        public static explicit operator short(DataBag bag)
        {
            return (short)bag.Get(typeof(short));
        }

        public static explicit operator bool(DataBag bag)
        {
            return (bool)bag.Get(typeof(bool));
        }

        public static explicit operator string(DataBag bag)
        {
            return (string)bag.Get(typeof(string));
        }

        public static explicit operator int[](DataBag bag)
        {
            return (int[])bag.Get(typeof(int[]));
        }

        public static explicit operator float[](DataBag bag)
        {
            return (float[])bag.Get(typeof(float[]));
        }

        public static explicit operator double[](DataBag bag)
        {
            return (double[])bag.Get(typeof(double[]));
        }

        public static explicit operator short[](DataBag bag)
        {
            return (short[])bag.Get(typeof(short[]));
        }

        public static explicit operator bool[](DataBag bag)
        {
            return (bool[])bag.Get(typeof(bool[]));
        }

        public static explicit operator string[](DataBag bag)
        {
            return (string[])bag.Get(typeof(string[]));
        }

        public IEnumerator<object> GetEnumerator()
        {
            return dict.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dict.Values.GetEnumerator();
        }
    }
}