using System.Collections.Generic;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Windows.Forms;
using log4net;

namespace Comport.ORM
{
    internal static class StaticResources
    {
        private static ILog logger = null;

        public static JsonSerializerSettings jsonSetting = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public static ILog Logger
        {
            get
            {
                if (logger == null)
                {
                    XmlConfigurator.ConfigureAndWatch(new FileInfo(Application.StartupPath + "\\log4net.config"));
                    logger = LogManager.GetLogger("ORM");
                }

                return logger;
            }
        }

        public static T Clone<T>(T obj)
        {
            if (obj == null)
            {
                return default(T);
            }

            if (obj is ICloneable)
            {
                return (T)((ICloneable)(object)obj).Clone();
            }

            string value = JsonConvert.SerializeObject(obj, jsonSetting);
            return JsonConvert.DeserializeObject<T>(value, jsonSetting);
        }

        public static Type GetListElementType(Type type)
        {
            if (type == null)
            {
                return null;
            }

            if (type.HasElementType)
            {
                return type.GetElementType();
            }

            while (type != typeof(object))
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                {
                    return type.GenericTypeArguments[0];
                }

                type = type.BaseType;
            }

            return null;
        }

        public static Type GetDictionaryValueType(Type type)
        {
            if (type == null)
            {
                return null;
            }

            while (type != typeof(object))
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                {
                    return type.GenericTypeArguments[1];
                }

                type = type.BaseType;
            }

            return null;
        }
    }
}