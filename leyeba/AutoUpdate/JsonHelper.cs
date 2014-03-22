using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace AutoUpdate
{
    public class JsonHelper
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="jsonString">Json字符串</param>
        /// <returns>泛型结果</returns>
        public static T FromJsonTo<T>(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString)) return default(T);
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
                {
                    T jsonObject = (T)ser.ReadObject(ms);
                    return jsonObject;
                }
            }
            catch (Exception exp)
            {
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AutoUpdate.JsonHelper.error.txt"), exp.Message);
                return default(T);
            }
        }
    }
}
