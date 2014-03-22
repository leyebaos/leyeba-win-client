using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
    public class Log
    {
        public static void debug(Type t, string message)
        {
            #if DEBUG
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            if (log.IsDebugEnabled)
            {
                log.Debug(message);
            }
            log = null;
            #endif
        }

        public static void debug(Type t, Exception ex)
        {
            #if DEBUG
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            if (log.IsDebugEnabled)
            {
                log.Debug(ex.Message + Environment.NewLine + ex.StackTrace);
            }
            log = null;
            #endif
        }

        public static void error(Type t, string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
            log = null;
        }

        public static void error(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            if (log.IsErrorEnabled)
            {
                log.Error(ex.Message + Environment.NewLine + ex.StackTrace);
            }
            log = null;
        }

        public static void fatal(Type t, string message)
        {

            log4net.ILog log = log4net.LogManager.GetLogger(t);
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }
            log = null;
        }

        public static void fatal(Type t, Exception ex)
        {

            log4net.ILog log = log4net.LogManager.GetLogger(t);
            if (log.IsFatalEnabled)
            {
                log.Fatal(ex.Message + Environment.NewLine + ex.StackTrace);
            }
            log = null;
        }

        public static void info(Type t, string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }
            log = null;
        }

        public static void info(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            if (log.IsInfoEnabled)
            {
                log.Info(ex.Message+Environment.NewLine+ex.StackTrace);
            }
            log = null;
        }

        public static void warn(Type t, string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
            log = null;
        }

        public static void warn(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            if (log.IsWarnEnabled)
            {
                log.Warn(ex.Message + Environment.NewLine + ex.StackTrace);
            }
            log = null;
        }
    }
}
