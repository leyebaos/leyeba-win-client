using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Util
{
    public class BinaryHelper
    {
        /// <summary>
        /// 将对象保存为文件
        /// </summary>
        /// <param name="fileName">保存文件路径，包括文件名</param>
        /// <param name="graph">序列化对象</param>
        /// <returns></returns>
        public static bool SaveObjectToFile(string fileName, object graph)
        {
            try
            {
                //序列化
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    BinaryFormatter bf = new BinaryFormatter();                    
                    bf.Serialize(fs, graph);
                }
                return true;
            }
            catch (Exception exp)
            {
                Log.error(typeof(BinaryHelper), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + exp.Message);
                return false;
            }
        }
        /// <summary>
        /// 将对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T FromObjectTo<T>(string fileName)
        {
            try
            {
                //序列化
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    return (T)bf.Deserialize(fs);
                }
            }
            catch (Exception exp)
            {
                Log.error(typeof(BinaryHelper), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + exp.Message);
                return default(T);
            }
        }
    }
}
