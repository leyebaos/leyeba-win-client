using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util.ConfigManage
{
    public class ConfigHelper
    {
        public static UserSetting UserSettingConfig
        {
            get {
                UserSetting userSetting = AppDomain.CurrentDomain.GetData("UserSetting") as UserSetting;
                if (userSetting == null)
                {
                    userSetting = new UserSetting();
                    AppDomain.CurrentDomain.SetData("UserSetting", userSetting);
                }
                return userSetting;
            }
            set {
                AppDomain.CurrentDomain.SetData("UserSetting", value);
            }
        }

        public static SystemSetting SystemSettingConfig
        {
            get
            {
                SystemSetting sysSetting = AppDomain.CurrentDomain.GetData("SystemSetting") as SystemSetting;
                if (sysSetting == null)
                {
                    sysSetting = new SystemSetting();
                    AppDomain.CurrentDomain.SetData("SystemSetting", sysSetting);
                }
                return sysSetting;
            }
            set
            {
                AppDomain.CurrentDomain.SetData("SystemSetting", value);
            }
        }
    }
}
