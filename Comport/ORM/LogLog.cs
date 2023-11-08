using System.Diagnostics;
using System;

namespace Comport.ORM
{
    public sealed class LogLog
    {
        private const string PREFIX = "log4net: ";

        private const string ERR_PREFIX = "log4net:ERROR ";

        private const string WARN_PREFIX = "log4net:WARN ";

        private static bool s_debugEnabled;

        private static bool s_quietMode;

        public static bool InternalDebugging
        {
            get
            {
                return s_debugEnabled;
            }
            set
            {
                s_debugEnabled = value;
            }
        }

        public static bool QuietMode
        {
            get
            {
                return s_quietMode;
            }
            set
            {
                s_quietMode = value;
            }
        }

        public static bool IsDebugEnabled => s_debugEnabled && !s_quietMode;

        public static bool IsWarnEnabled => !s_quietMode;

        public static bool IsErrorEnabled => !s_quietMode;

        private LogLog()
        {
        }

        static LogLog()
        {
            s_debugEnabled = false;
            s_quietMode = false;
            try
            {
                //InternalDebugging = OptionConverter.ToBoolean(SystemInfo.GetAppSetting("log4net.Internal.Debug"), defaultValue: false);
                //QuietMode = OptionConverter.ToBoolean(SystemInfo.GetAppSetting("log4net.Internal.Quiet"), defaultValue: false);
            }
            catch (Exception exception)
            {
                Error("LogLog: Exception while reading ConfigurationSettings. Check your .config file is well formed XML.", exception);
            }
        }

        public static void Debug(string message)
        {
            if (IsDebugEnabled)
            {
                EmitOutLine("log4net: " + message);
            }
        }

        public static void Debug(string message, Exception exception)
        {
            if (IsDebugEnabled)
            {
                EmitOutLine("log4net: " + message);
                if (exception != null)
                {
                    EmitOutLine(exception.ToString());
                }
            }
        }

        public static void Warn(string message)
        {
            if (IsWarnEnabled)
            {
                EmitErrorLine("log4net:WARN " + message);
            }
        }

        public static void Warn(string message, Exception exception)
        {
            if (IsWarnEnabled)
            {
                EmitErrorLine("log4net:WARN " + message);
                if (exception != null)
                {
                    EmitErrorLine(exception.ToString());
                }
            }
        }

        public static void Error(string message)
        {
            if (IsErrorEnabled)
            {
                EmitErrorLine("log4net:ERROR " + message);
            }
        }

        public static void Error(string message, Exception exception)
        {
            if (IsErrorEnabled)
            {
                EmitErrorLine("log4net:ERROR " + message);
                if (exception != null)
                {
                    EmitErrorLine(exception.ToString());
                }
            }
        }

        private static void EmitOutLine(string message)
        {
            try
            {
                Console.Out.WriteLine(message);
                Trace.WriteLine(message);
            }
            catch
            {
            }
        }

        private static void EmitErrorLine(string message)
        {
            try
            {
                Console.Error.WriteLine(message);
                Trace.WriteLine(message);
            }
            catch
            {
            }
        }
    }
}