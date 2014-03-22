using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
    public class RegistryHelper
    {
        /// <summary> 
        /// 设置程序开机启动 
        /// 或取消开机启动 
        /// </summary> 
        /// <param name="started">设置开机启动，或者取消开机启动</param> 
        /// <param name="exeName">注册表中程序的名字</param> 
        /// <param name="path">开机启动的程序路径</param> 
        /// <returns>开启或则停用是否成功</returns> 
        public static bool RunWhenStart(bool started, string exeName, string path)
        {
            string keyPath = @"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
            return SettingReg(started, exeName, path, keyPath, Registry.LocalMachine);
        }

        /// <summary>
        /// 添加/删除注册表键值
        /// </summary>
        /// <param name="isSetting">true:添加;false：删除</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="keyPath">键所在的路径(不包含根)</param>
        /// <param name="root">注册表根</param>
        /// <returns>true:操作成功</returns>
        public static bool SettingReg(bool isSetting, string key, object val, string keyPath, RegistryKey root)
        {
            RegistryKey regKey = root.OpenSubKey(keyPath, true);//打开注册表子项 
            if (regKey == null)//如果该项不存在的话，则创建该子项 
            {
                regKey = root.CreateSubKey(keyPath);
            }

            try
            {
                if (isSetting)
                {
                    regKey.SetValue(key, val);//设置为开机启动 
                }
                else
                {
                    regKey.DeleteValue(key);//取消开机启动 
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                regKey.Close();
            }
            return true;
        }
    }
}
