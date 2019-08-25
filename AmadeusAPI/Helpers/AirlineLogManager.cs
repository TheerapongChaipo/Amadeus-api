using System;
using System.Reflection;

namespace AmadeusAPI.Helpers
{
    public class AirlineLogManager
    {

        private static readonly log4net.ILog creditcardLog = log4net.LogManager.GetLogger(typeof(AirlineLogManager));

        /******************** Exiting ********************/
        public static void Exiting(object obj, string msg, Type _type, MethodBase _method, bool showObject = false)
        {
            Exiting(msg, _type, _method);
            if (showObject)
                Info(obj, _type, _method);
            else
                Debug(obj, _type, _method);
        }

        public static void Exiting(string msg, Type _type, MethodBase _method)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                Exiting(_type, _method);
            }
            else
            {
                Info(string.Format("Exiting {0}", msg), _type, _method);
            }
        }
        public static void Exiting(Type _type, MethodBase _method)
        {
            Info("Exiting", _type, _method);
        }

        /******************** Entering ********************/
        public static void Entering(object obj, string msg, Type _type, MethodBase _method, bool showObject = false)
        {
            Entering(msg, _type, _method);
            if (showObject)
                Info(obj, _type, _method);
            else
                Debug(obj, _type, _method);
        }
        public static void Entering(string msg, Type _type, MethodBase _method)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                Entering(_type, _method);
            }
            else
            {
                Info(string.Format("Entering - {0}", msg), _type, _method);
            }
        }
        public static void Entering(Type _type, MethodBase _method)
        {
            Info("Entering", _type, _method);
        }

        /******************** Callout, invoking ********************/
        public static void Invoking(object obj, string msg, Type _type, MethodBase _method, bool showObject = false, bool jsonObject = false)
        {
            Invoking(msg, _type, _method);
            if (showObject)
                Info(obj, _type, _method, jsonObject);
            else
                Debug(obj, _type, _method, jsonObject);
        }
        public static void Invoking(string msg, Type _type, MethodBase _method)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                Invoking(_type, _method);
            }
            else
            {
                Info(string.Format("Invoking - {0}", msg), _type, _method);
            }
        }
        public static void Invoking(Type _type, MethodBase _method)
        {
            Info("Invoking", _type, _method);
        }

        /******************** Reply  ********************/
        public static void Reply(object obj, string msg, Type _type, MethodBase _method, bool showObject = false, bool jsonObject = false)
        {
            Reply(msg, _type, _method);
            if (showObject)
                Info(obj, _type, _method, jsonObject);
            else
                Debug(obj, _type, _method, jsonObject);
        }

        public static void Reply(string msg, Type _type, MethodBase _method)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                Reply(_type, _method);
            }
            else
            {
                Info(string.Format("Reply - {0}", msg), _type, _method);
            }
        }
        public static void Reply(Type _type, MethodBase _method)
        {
            Info("Reply", _type, _method);
        }

        /******************** DEBUG ********************/
        public static void Debug(string msg, Type _type, MethodBase _method)
        {
            creditcardLog.Debug(string.Format("{0}:{1}:{2}", _type.Name, _method.Name, msg));
        }
        /* Print data object in detail */
        public static void Debug(object obj, Type _type, MethodBase _method, bool jsonObject = false)
        {
            if (creditcardLog.IsDebugEnabled)
            {

                if (obj != null)
                {
                    if (jsonObject)
                    {
                        creditcardLog.Debug(string.Format("{0}:{1}:{2}[{2}{3}{2}]", _type.Name, _method.Name, Environment.NewLine, DataObjectHandler.SerializeObjToJsonString(obj)));
                    }
                    else
                    {
                        creditcardLog.Debug(string.Format("{0}:{1}:{2}[{2}{3}{2}]", _type.Name, _method.Name, Environment.NewLine, DataObjectHandler.SerializeObjToXmlString(obj)));
                    }
                }
                else
                {
                    creditcardLog.Debug(string.Format("{0}:{1}:[]", _type.Name, _method.Name));
                }
            }
        }
        /* DEBUG with exception */
        public static void Debug(string msg, Type _type, MethodBase _method, Exception ex)
        {
            creditcardLog.Debug(string.Format("{0}:{1}:{2}", _type.Name, _method.Name, msg), ex);
        }


        /******************* Error ********************/
        public static void Error(string msg, Type _type, MethodBase _method)
        {

            creditcardLog.Error(string.Format("{0}:{1}:{2}", _type.Name, _method.Name, msg));
        }
        /* Print data object in detail */
        public static void Error(object obj, Type _type, MethodBase _method, Exception ex)
        {
            if (creditcardLog.IsErrorEnabled)
            {

                if (obj != null)
                    creditcardLog.Error(string.Format("{0}:{1}:{2}[{2}{3}{2}]", _type.Name, _method.Name, Environment.NewLine, DataObjectHandler.SerializeObjToXmlString(obj)), ex);
                else
                    creditcardLog.Error(string.Format("{0}:{1}:[]", _type.Name, _method.Name), ex);
            }
        }
        /* Error with exception */
        public static void Error(string msg, Type _type, MethodBase _method, Exception ex)
        {

            creditcardLog.Error(string.Format("{0}:{1}:{2}", _type.Name, _method.Name, msg), ex);
        }


        /******************** Info ********************/
        public static void Info(string msg, Type _type, MethodBase _method)
        {
            string message = string.Format("{0}:{1}:{2}", _type.Name, _method.Name, msg);
            creditcardLog.Info(message);
        }
        /* Print data object in detail */
        public static void Info(object obj, Type _type, MethodBase _method, bool jsonObject = false)
        {
            if (creditcardLog.IsInfoEnabled)
            {
                if (obj != null)
                {
                    if (jsonObject)
                    {
                        creditcardLog.Info(string.Format("{0}:{1}:{2}[{2}{3}{2}]", _type.Name, _method.Name, Environment.NewLine, DataObjectHandler.SerializeObjToJsonString(obj)));
                    }
                    else
                    {
                        creditcardLog.Info(string.Format("{0}:{1}:{2}[{2}{3}{2}]", _type.Name, _method.Name, Environment.NewLine, DataObjectHandler.SerializeObjToXmlString(obj)));
                    }
                }
                else
                {
                    creditcardLog.Info(string.Format("{0}.{1}:[]", _type.Name, _method.Name));
                }
            }
        }
        /* Info with exception */
        public static void Info(string msg, Type _type, MethodBase _method, Exception ex)
        {
            creditcardLog.Info(string.Format("{0}:{1}:{2}", _type.Name, _method.Name, msg), ex);
        }

    }
}