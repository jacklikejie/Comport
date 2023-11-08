using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Comport.ORM
{
    public static class MappingPropertyExtension
    {
        public static bool SetValue<T>(this T obj, string address, object value, SetContext context = null) where T : class, IMapping
        {
            string[] array = DataItem.AnalyzeAddress(address);
            if (array == null)
            {
                return false;
            }

            return obj.SetValue(array, value, context);
        }

        public static bool SetValue<T>(this T obj, string[] address, object value, SetContext context = null) where T : class, IMapping
        {
            try
            {
                InternalSetValue(obj, address, value, context);
                return true;
            }
            catch (Exception ex)
            {
                string text = ((address == null) ? "null" : string.Join(".", address));
                StaticResources.Logger.Debug("设置属性值发生错误：" + text + ex.Message);
                return false;
            }
        }

        private static void InternalSetValue(object obj, string[] address, object value, SetContext context)
        {
            int num = Array.IndexOf(address, "*");
            if (num >= 0)
            {
                string[] array = address.Skip(num).ToArray();
                IList list = ((num == 0) ? obj : InternalGetValue(obj, address.Take(num).ToArray())) as IList;
                for (int i = 0; i < list.Count; i++)
                {
                    array[0] = i.ToString();
                    InternalSetValue(list, array, StaticResources.Clone(value), context);
                }

                return;
            }

            object obj2 = obj;
            if (address.Length > 1)
            {
                obj2 = InternalGetValue(obj, address.Take(address.Length - 1).ToArray());
            }

            string text = address[address.Length - 1];
            Type type = obj2.GetType();
            if (obj2 is IDictionary)
            {
                value = ConvertType(value, StaticResources.GetDictionaryValueType(type));
                (obj2 as IDictionary)[text] = value;
                return;
            }

            if (obj2 is IList)
            {
                value = ConvertType(value, StaticResources.GetListElementType(type));
                (obj2 as IList)[int.Parse(text)] = value;
                return;
            }

            bool flag = context?.SkipPropertyWithoutSetter ?? false;
            PropertyInfo property = obj2.GetType().GetProperty(text);
            if (property.SetMethod != null || !flag)
            {
                value = ConvertType(value, property.PropertyType);
                property.SetValue(obj2, value);
            }
        }

        public static void SetValues<T>(this T obj, Dictionary<string, object> values, bool ignoreError = false) where T : class, IMapping
        {
            if (values == null || values.Count == 0)
            {
                return;
            }

            foreach (KeyValuePair<string, object> value in values)
            {
                if (!obj.SetValue(value.Key, value.Value) && !ignoreError)
                {
                    throw new Exception(string.Concat("批量设置值的过程中出现错误。[", value.Key, ":", value.Value, "]"));
                }
            }
        }

        public static void SetValues<T>(this T obj, Dictionary<string[], object> values, bool ignoreError = false) where T : class, IMapping
        {
            if (values == null || values.Count == 0)
            {
                return;
            }

            foreach (KeyValuePair<string[], object> value in values)
            {
                if (!obj.SetValue(value.Key, value.Value) && !ignoreError)
                {
                    throw new Exception(string.Concat("批量设置值的过程中出现错误。[", string.Join(".", value.Key), ":", value.Value, "]"));
                }
            }
        }

        public static object GetValue(this IMapping obj, string address, int startLevel = 0)
        {
            string[] array = DataItem.AnalyzeAddress(address);
            if (array == null)
            {
                throw new ArgumentException("address 无效。" + address);
            }

            return InternalGetValue(obj, array, startLevel);
        }

        public static bool TryGetValue(this IMapping obj, string[] address, out object value, int startLevel = 0)
        {
            value = null;
            if (address == null)
            {
                return false;
            }

            try
            {
                value = InternalGetValue(obj, address, startLevel);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static object GetValue(this IMapping obj, string[] address, int startIndex = 0)
        {
            return InternalGetValue(obj, address, startIndex);
        }

        private static object InternalGetValue(object obj, string[] address, int startIndex = 0)
        {
            if (address == null || address.Length == 0)
            {
                throw new ArgumentException("参数错误 address 。");
            }

            if (startIndex < 0 || startIndex >= address.Length)
            {
                throw new IndexOutOfRangeException("起始位置超出索引范围。");
            }

            object obj2 = obj;
            for (int i = startIndex; i < address.Length; i++)
            {
                string text = address[i];
                if (obj2 is IDictionary)
                {
                    IDictionary dictionary = obj2 as IDictionary;
                    if (!dictionary.Contains(text))
                    {
                        throw new KeyNotFoundException("已检索该属性，并且集合中不存在 key。" + address[i]);
                    }

                    obj2 = dictionary[text];
                }
                else
                {
                    obj2 = ((!(obj2 is IList)) ? obj2.GetType().GetProperty(text).GetValue(obj2) : ((!(address[i] == "*")) ? (obj2 as IList)[int.Parse(text)] : (obj2 as IList)[0]));
                }
            }

            return obj2;
        }

        public static bool TryRemoveKey(this IMapping obj, string address)
        {
            return obj.TryRemoveKey(DataItem.AnalyzeAddress(address));
        }

        internal static bool TryRemoveKey(this object obj, string[] addr)
        {
            if (addr == null)
            {
                return false;
            }

            int num = Array.IndexOf(addr, "*");
            if (num >= 0)
            {
                string[] array = addr.Skip(num).ToArray();
                IList list = ((num == 0) ? obj : InternalGetValue(obj, addr.Take(num).ToArray())) as IList;
                for (int i = 0; i < list.Count; i++)
                {
                    array[0] = i.ToString();
                    if (!list.TryRemoveKey(array))
                    {
                        return false;
                    }
                }

                return true;
            }

            try
            {
                object obj2 = obj;
                if (addr.Length > 1)
                {
                    obj2 = InternalGetValue(obj, addr.Take(addr.Length - 1).ToArray());
                }

                string text = addr[addr.Length - 1];
                Type type = obj2.GetType();
                if (obj2 is IDictionary)
                {
                    (obj2 as IDictionary).Remove(text);
                    return true;
                }

                if (obj2 is IList)
                {
                    IList list = obj2 as IList;
                    if (text == "*")
                    {
                        list.Clear();
                    }
                    else
                    {
                        list.RemoveAt(int.Parse(text));
                    }

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static object ConvertType(object value, Type type)
        {
            if (value == null || type.IsAssignableFrom(value.GetType()))
            {
                return value;
            }

            if (typeof(IConvertible).IsAssignableFrom(type))
            {
                return Convert.ChangeType(value, type);
            }

            return value;
        }
    }
}
