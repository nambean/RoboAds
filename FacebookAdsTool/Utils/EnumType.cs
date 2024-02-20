
using System.Collections.Generic;

namespace FacebookAdsTool.Utils
{
    // Version Windown
    public enum WindowEnum
    {
        // 10/Server 2016
        Win10_S2016 = 8,
        // 8.1/Server 2012 R2
        Win81_S2012R2 = 7,
        // 8/Server 2012
        Win8_S2012 = 6,
        // 7/Server 2008 R2
        Win7_S2008R2 = 5,
        // Server 2008/Vista
        WinVista_S2008 = 4,
        // Server 2003 R2/Server 2003/XP 64-Bit Edition
        WinXP_S2003R2 = 3,
        // XP
        WinXP = 2,
        // 2000
        Win2000 = 1,
    }

    public enum RegistryType
    {
        ConnectStatus,
        LogTime,
        LogDay,
        LogSend,
        DateLogSend,
        ServerUrl,
        GroupUrl,
        TimeScheduler,
        IconStatus,
        Version,
        Ports,
        AccessToken,
        UserName,
        Password,
        FacebookAccessToken,
        License
    }
}
