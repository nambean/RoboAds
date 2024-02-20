using System;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace FacebookAdsTool.Utils
{

    public class RegistryUtil
    {
        public const string REGISTRY_PATH_CONFIG = "Software\\RoboAds\\Configuration";
        public const string REGISTRY_PATH_STARTUP = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        public const string KEY_CONNECT_STATUS = "ConnectStatus";
        public const string KEY_LOG_TIME = "LogTime";
        public const string KEY_LOG_DAY = "LogDay";
        public const string KEY_LOG_SEND = "LogSend";
        public const string KEY_DATE_LOG_SEND = "DateLogSend";
        public const string KEY_SERVER_URL = "ServerUrl";        
        public const string KEY_ICON_STATUS = "Iconstatus";
        public const string KEY_VERSION = "Version";
        public const string KEY_PORTS = "Ports";
        public const string KEY_GROUP_URL = "GroupUrl";
        public const string KEY_ACCESS_TOKEN = "AccessToken";
        public const string KEY_FACEBOOK_ACCESS_TOKEN = "FacebookAccessToken";
        public const string KEY_USER_NAME = "UserName";
        public const string KEY_PASSWORD = "Password";
        public const string KEY_TIME_SCHEDULER = "TimeScheduler";
        public const string KEY_LICENSE = "License";


        public int timeAuto;
        public string lisence;



        public static void SetUrl(RegistryType type, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var urlencryp = EncryptString(value, "prosec");
                SetCongifValueToRegistry(type, urlencryp);
            }
        }

        public static string GetUrl(RegistryType type)
        {
            var result = "";
            var value = GetConfigValueFromRegistry(type);
            if (!string.IsNullOrEmpty(value))
            {
                result = DecryptString(value, "admin");
            }
            return result;
        }

        public void LoadSettings()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\RoboAds");

            //if it does exist, retrieve the stored values  
            if (key != null)
            {
                timeAuto = Int32.Parse(RegistryUtil.GetConfigValueFromRegistry(RegistryType.TimeScheduler));
                lisence = RegistryUtil.GetConfigValueFromRegistry(RegistryType.License);
                key.Close();
            }
        }

        public static void SetCongifValueToRegistry(RegistryType type, string value)
        {
            RegistryKey key;

            if (!CheckRegistryKeyInCurrentUser(REGISTRY_PATH_CONFIG))
            {
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(REGISTRY_PATH_CONFIG, RegistryKeyPermissionCheck.ReadWriteSubTree);
            }
            else
            {
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(REGISTRY_PATH_CONFIG, RegistryKeyPermissionCheck.ReadWriteSubTree);
            }

            if (key != null)
            {
                key.SetValue(GetKeyName(type), value);
            }
            else
            {
                key.SetValue(GetKeyName(type), string.Empty);
            }
        }

        /// <summary>
        /// Lấy các giá trị cấu hình lưu trong Registry
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetConfigValueFromRegistry(RegistryType type)
        {
            var keyName = GetKeyName(type);
            if (!CheckRegistryKeyInCurrentUser(REGISTRY_PATH_CONFIG))
            {
                return null;
            }
            var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(REGISTRY_PATH_CONFIG, RegistryKeyPermissionCheck.ReadWriteSubTree);
            var result = key != null ? key.GetValue(keyName) : null;

            if (result != null)
            {
                return result.ToString();
            }
            return string.Empty;
        }

        public static void SetStartup(string productName, string startPath)
        {
            var rkApp = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(REGISTRY_PATH_STARTUP, true);
            rkApp.SetValue(productName, startPath);
        }

        /// <summary>
        /// Lấy giá trị Key theo keyPath và keyName
        /// </summary>
        /// <param name="keyPath"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        private object GetKeyValue(string keyPath, string keyName)
        {
            if (!CheckRegistryKeyInCurrentUser(keyPath))
            {
                return null;
            }
            var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
            return key != null ? key.GetValue(keyName) : null;
        }

        private static bool CheckRegistryKeyInCurrentUser(string registryPath)
        {
            try
            {
                var tmpKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(registryPath, false);
                return tmpKey != null ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Hàm mã hóa chuỗi
        /// </summary>
        /// <param name="message"></param>
        /// <param name="passphrase"></param>
        /// <returns></returns>
        public static string EncryptString(string message, string passphrase)
        {
            byte[] results;
            var utf8 = new System.Text.UTF8Encoding();

            // Buoc 1: Bam chuoi su dung MD5
            var hashProvider = new MD5CryptoServiceProvider();
            byte[] tdesKey = hashProvider.ComputeHash(utf8.GetBytes(passphrase));

            // Step 2. Tao doi tuong TripleDESCryptoServiceProvider moi
            var tdesAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Cai dat bo ma hoa
            tdesAlgorithm.Key = tdesKey;
            tdesAlgorithm.Mode = CipherMode.ECB;
            tdesAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert chuoi (Message) thanh dang byte[]
            byte[] dataToEncrypt = utf8.GetBytes(message);

            // Step 5. Ma hoa chuoi
            try
            {
                var encryptor = tdesAlgorithm.CreateEncryptor();
                results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            }
            finally
            {
                // Xoa moi thong tin ve Triple DES va HashProvider de dam bao an toan
                tdesAlgorithm.Clear();
                hashProvider.Clear();
            }

            // Step 6. Tra ve chuoi da ma hoa bang thuat toan Base64
            return Convert.ToBase64String(results);
        }

        /// <summary>
        /// Hàm giải mã chuỗi
        /// </summary>
        /// <param name="message"></param>
        /// <param name="passphrase"></param>
        /// <returns></returns>
        public static string DecryptString(string message, string passphrase)
        {
            byte[] results;
            var utf8 = new System.Text.UTF8Encoding();

            // Step 1. Bam chuoi su dung MD5
            var hashProvider = new MD5CryptoServiceProvider();
            byte[] tdesKey = hashProvider.ComputeHash(utf8.GetBytes(passphrase));

            // Step 2. Tao doi tuong TripleDESCryptoServiceProvider moi
            var tdesAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Cai dat bo giai ma
            tdesAlgorithm.Key = tdesKey;
            tdesAlgorithm.Mode = CipherMode.ECB;
            tdesAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert chuoi (Message) thanh dang byte[]
            byte[] dataToDecrypt = Convert.FromBase64String(message);

            // Step 5. Bat dau giai ma chuoi
            try
            {
                var decryptor = tdesAlgorithm.CreateDecryptor();
                results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            }
            finally
            {
                // Xoa moi thong tin ve Triple DES va HashProvider de dam bao an toan
                tdesAlgorithm.Clear();
                hashProvider.Clear();
            }

            // Step 6. Tra ve ket qua bang dinh dang UTF8
            return utf8.GetString(results);
        }
        private static string GetKeyName(RegistryType type)
        {
            switch (type)
            {
                case RegistryType.ConnectStatus:
                    return KEY_CONNECT_STATUS;
                case RegistryType.LogTime:
                    return KEY_LOG_TIME;
                case RegistryType.LogSend:
                    return KEY_LOG_SEND;
                case RegistryType.DateLogSend:
                    return KEY_DATE_LOG_SEND;
                case RegistryType.ServerUrl:
                    return KEY_SERVER_URL;
                case RegistryType.TimeScheduler:
                    return KEY_TIME_SCHEDULER;
                case RegistryType.LogDay:
                    return KEY_LOG_DAY;
                case RegistryType.IconStatus:
                    return KEY_ICON_STATUS;
                case RegistryType.Version:
                    return KEY_VERSION;
                case RegistryType.Ports:
                    return KEY_PORTS;
                case RegistryType.GroupUrl:
                    return KEY_GROUP_URL;
                case RegistryType.AccessToken:
                    return KEY_ACCESS_TOKEN;
                case RegistryType.UserName:
                    return KEY_USER_NAME;
                case RegistryType.Password:
                    return KEY_PASSWORD;
                case RegistryType.FacebookAccessToken:
                    return KEY_FACEBOOK_ACCESS_TOKEN;
                case RegistryType.License:
                    return KEY_LICENSE;
                default:
                    return "";
            }
        }

        /// <summary>
        /// create 1 registry and create icon app at Control Panel\Programs\Programs and Features
        /// </summary>
        /// <param name="version">Version của app</param>
        /// <param name="nameApp">App name</param>
        /// <param name="pathUninstall">Path file .exe unistall app</param>
        public static void CreateRegistryUnistallApp(string version, string nameApp, string pathUninstall, string iconApp, string author)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true);
            key.CreateSubKey(nameApp);
            key = key.OpenSubKey(nameApp, true);
            key.SetValue("DisplayName", nameApp);
            key.SetValue("DisplayVersion", version);
            key.SetValue("InstallLocation", nameApp);
            key.SetValue("UninstallString", pathUninstall);
            key.SetValue("DisplayIcon", iconApp);
            key.SetValue("Publisher", author);
        }
    }
}
