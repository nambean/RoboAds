using FacebookAdsTool.Libs;
using FacebookAdsTool.Utils;
using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace FacebookAdsTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Lấy thông tin người dùng
                var userInfoResponse = HttpClientUtils.Instance.GetCurrentUser();
                if (!userInfoResponse.IsSuccessStatusCode)
                {
                    Application.Run(new Login());
                    return;
                }

                //Create table fist time start app
                //RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\RoboAdsSettings");



                string license = "";

                license = RegistryUtil.GetConfigValueFromRegistry(RegistryType.License);
                if (license == "")
                {
                    //storing the values  
                    Random rnd = new Random();
                    license = "RoboADS-" + rnd.Next(10000000, 999999999).ToString();
                    RegistryUtil.SetCongifValueToRegistry(RegistryType.License, license);
                    RegistryUtil.SetCongifValueToRegistry(RegistryType.TimeScheduler, "5");
                }

                Application.Run(new Dashboad());
            } catch(Exception ex)
            {
                LogUtil.WriteLogExeption(ex.ToString());
            }
        }

        public static bool IsInDesignMode()
        {
            if (Application.ExecutablePath.IndexOf("devenv.exe", StringComparison.OrdinalIgnoreCase) > -1)
            {
                return true;
            }
            return false;
        }
    }
}
