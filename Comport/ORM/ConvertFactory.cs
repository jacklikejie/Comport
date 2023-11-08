using System;

namespace Comport.ORM
{
    internal class ConvertFactory
    {
        public static IConverter Create(string type, string argsJson)
        {
            try
            {
                switch (type.ToLower())
                {
                    case "time":
                        return new TimeConverter(argsJson);
                    case "numeric":
                        return new NumericConverter(argsJson);
                    case "mapping":
                        return new MappingConverter(argsJson);
                    case "bitarray":
                        return new BitArrayConverter(argsJson);
                    case "list":
                        return new ListConverter(argsJson);
                    case "dictionary":
                        return new DictionaryConverter(argsJson);
                    default:
                        throw new ArgumentException("不支持的Converter类型：" + type);
                }
                //return type.ToLower() switch
                //{
                //    "time" => new TimeConverter(argsJson),
                //    "numeric" => new NumericConverter(argsJson),
                //    "mapping" => new MappingConverter(argsJson),
                //    "bitarray" => new BitArrayConverter(argsJson),
                //    "list" => new ListConverter(argsJson),
                //    "dictionary" => new DictionaryConverter(argsJson),
                //    _ => throw new ArgumentException("不支持的Converter类型：" + type),
                //};
            }
            catch (Exception ex)
            {
                string message = $"创建IConverter出错({type},{argsJson})：{ex.Message}";
                throw new Exception(message);
            }
        }
    }
}