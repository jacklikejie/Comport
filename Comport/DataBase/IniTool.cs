using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Comport.DataBase
{
    public class IniTool
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string A_0, string A_1, string A_2, string A_3);

        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string A_0, string A_1, string A_2, StringBuilder A_3, int A_4, string A_5);

        [DllImport("kernel32")]
        private static extern uint GetPrivateProfileString(string A_0, string A_1, string A_2, byte[] A_3, uint A_4, string A_5);

        public static string ReadString(string section, string key, string def, string filePath)
        {
            if (!Path.IsPathRooted(filePath))
            {
                filePath = Path.GetFullPath(filePath);
            }

            if (!File.Exists(filePath))
            {
                return def;
            }

            byte[] array = new byte[1024];
            uint privateProfileString = GetPrivateProfileString(section, key, def, array, (uint)array.Length, filePath);
            if (privateProfileString == 0)
            {
                return def;
            }

            return Encoding.Default.GetString(array, 0, (int)privateProfileString);
        }

        public static int ReadInt(string section, string key, int def, string filePath)
        {
            string text = ReadString(section, key, string.Empty, filePath);
            if (string.IsNullOrEmpty(text))
            {
                return def;
            }

            text = text.Trim();
            int result = 0;
            if (!int.TryParse(text, out result))
            {
                return def;
            }

            return result;
        }

        public static double ReadDouble(string section, string key, double def, string filePath)
        {
            string text = ReadString(section, key, string.Empty, filePath);
            if (string.IsNullOrEmpty(text))
            {
                return def;
            }

            text = text.Trim();
            double result = 0.0;
            if (!double.TryParse(text, out result))
            {
                return def;
            }

            return result;
        }

        //public static bool ReadBool(string section, string key, bool def, string filePath)
        //{
        //    int a_ = 17;
        //    string text = ReadString(section, key, string.Empty, filePath);
        //    if (string.IsNullOrEmpty(text))
        //    {
        //        return def;
        //    }

        //    text = text.Trim().ToUpper();
        //    if (text == UserText.b("ﾥ\ueda7囹", a_) || text == UserText.b("\uf2a5盛ﾩ\ue9ab", a_))
        //    {
        //        return true;
        //    }

        //    if (text == UserText.b("\ue8a5\ue7a7", a_) || text == UserText.b("\ue0a5\ue9a7\ue6a9ﾫ\uebad", a_))
        //    {
        //        return false;
        //    }

        //    return def;
        //}

        public static List<string> GetSections(string filePath)
        {
            List<string> list = new List<string>();
            if (!Path.IsPathRooted(filePath))
            {
                filePath = Path.GetFullPath(filePath);
            }

            if (!File.Exists(filePath))
            {
                return list;
            }

            byte[] array = new byte[65536];
            uint privateProfileString = GetPrivateProfileString(null, null, null, array, (uint)array.Length, filePath);
            int num = 0;
            for (int i = 0; i < privateProfileString; i++)
            {
                if (array[i] == 0)
                {
                    list.Add(Encoding.Default.GetString(array, num, i - num));
                    num = i + 1;
                }
            }

            return list;
        }

        public static List<string> GetKeys(string section, string filePath)
        {
            List<string> list = new List<string>();
            if (!Path.IsPathRooted(filePath))
            {
                filePath = Path.GetFullPath(filePath);
            }

            if (!File.Exists(filePath))
            {
                return list;
            }

            byte[] array = new byte[65536];
            uint privateProfileString = GetPrivateProfileString(section, null, null, array, (uint)array.Length, filePath);
            int num = 0;
            for (int i = 0; i < privateProfileString; i++)
            {
                if (array[i] == 0)
                {
                    list.Add(Encoding.Default.GetString(array, num, i - num));
                    num = i + 1;
                }
            }

            return list;
        }

        public static bool WriteString(string section, string key, string value, string filePath)
        {
            if (!Path.IsPathRooted(filePath))
            {
                filePath = Path.GetFullPath(filePath);
            }

            if (!File.Exists(filePath))
            {
                return false;
            }

            long num = WritePrivateProfileString(section, key, value, filePath);
            if (num == 0)
            {
                return false;
            }

            return true;
        }

        //public static bool WriteBool(string section, string key, bool value, string filePath)
        //{
        //    int a_ = 12;
        //    string value2 = (value ? UserText.b("\uf8a0\ue6a2\uf6a4", a_) : UserText.b("\uefa0\ueca2", a_));
        //    return WriteString(section, key, value2, filePath);
        //}

        public static bool WriteInt(string section, string key, int value, string filePath)
        {
            return WriteString(section, key, value.ToString(), filePath);
        }

        public static bool WriteFloat(string section, string key, float value, string filePath)
        {
            return WriteString(section, key, value.ToString(), filePath);
        }

        public static bool WriteDouble(string section, string key, double value, string filePath)
        {
            return WriteString(section, key, value.ToString(), filePath);
        }
    }
}