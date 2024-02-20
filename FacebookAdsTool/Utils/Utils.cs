using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Reflection;
using System.Threading;

namespace FacebookAdsTool.Utils
{
    public class UtilsLib
    {
        /// <summary>
        /// Get value from file config by Key
        /// </summary>
        /// <param name="key">Key in file config</param>
        /// <returns></returns>
        public static string GetValueFromFileConfig(string key)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (value != null)
                return value;
            return string.Empty;
        }

        public static string OnlyGetPathProgramFile()
        {
            string pathFile = @"C:\Program Files\FacebookAdsTool";
            return pathFile;
        }
        /// <summary>
        /// Create folder source execute
        /// </summary>
        /// <returns></returns>
        public static string GetPathProgramFile()
        {
            string pathSetup = OnlyGetPathProgramFile(); // GetValueFromFileConfig("FileSetup");
            if (Directory.Exists(pathSetup))
            {
                Directory.Delete(pathSetup, true);
                Directory.CreateDirectory(pathSetup);
            }
            else
                Directory.CreateDirectory(pathSetup);
            return pathSetup;
        }

        /// <summary>
        /// Lay ten file theo duong dan toi file
        /// </summary>
        /// <param name="urlAddress"></param>
        /// <returns></returns>
        public static string GetFileNameFormUrl(string urlAddress)
        {
            if (string.IsNullOrEmpty(urlAddress))
                return string.Empty;
            Uri uri = new Uri(urlAddress);
            string filename = System.IO.Path.GetFileName(uri.LocalPath);
            return filename;
        }

        /// <summary>
        /// Runfile with permission adminstrator
        /// </summary>
        /// <param name="path">Path file</param>
        public static void RunFileWithPermissionAdmin(string path, bool shellEx = true)
        {
            ProcessStartInfo info = new ProcessStartInfo(path);
            info.UseShellExecute = shellEx;
            info.Verb = "runas";
            Process.Start(info);
        }

        /// <summary>
        /// Run file excute
        /// </summary>
        /// <param name="path"></param>
        public static void RunFile(string path)
        {
            ProcessStartInfo info = new ProcessStartInfo(path);
            Process.Start(path);
        }

        /// <summary>
        /// Run in CMD
        /// </summary>
        /// <param name="excute">Excute command line or path file</param>
        /// <param name="isCommand">is run command line or run file </param>
        public static void RunCMDWithPermissionAdmin(string excute, bool isCommand = true)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "CMD.exe";
            // info.CreateNoWindow = false;
            //info.RedirectStandardInput = true;
            //info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            if (isCommand)
                info.Arguments = "/c " + excute;
            else
                info.Arguments = "/c " + "'" + excute + "'";
            info.Verb = "runas";

            Process proc = new Process();
            proc.StartInfo = info;
            proc.Start();
            proc.WaitForExit();
        }

        /// <summary>
        /// Create shortcut excute file
        /// </summary>
        /// <param name="pathFileExcute">Path to file exe excute</param>
        /// <param name="shortcutPathName">Path to show shortcut (Desktop, Menu Program...)</param>
        /// <param name="isCreate">Is create true/false</param>
        public static void CreateShortcut(string pathFileExcute, bool isCrateShortcutDesktop, bool isCreate)
        {
            // var pathDesktop =;
            var shortCutLinkFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\ProsecInc.lnk";
            if (!isCrateShortcutDesktop)
            {
                shortCutLinkFilePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu) + "\\ProsecInc.lnk";
            }

            if (isCreate)
            {
                // If you exists shortcude, delete and new create shortcut
                if (System.IO.File.Exists(shortCutLinkFilePath))
                    System.IO.File.Delete(shortCutLinkFilePath);

                WshShell myShell = new WshShell();
                WshShortcut myShortcut = (WshShortcut)myShell.CreateShortcut(shortCutLinkFilePath);
                myShortcut.TargetPath = pathFileExcute; //The exe file this shortcut executes when double clicked 
                myShortcut.IconLocation = pathFileExcute + ",0"; //Sets the icon of the shortcut to the exe`s icon 
                myShortcut.WorkingDirectory = pathFileExcute; //The working directory for the exe 
                myShortcut.Arguments = ""; //The arguments used when executing the exe 
                myShortcut.Save(); //Creates the shortcut 
            }
            else
            {
                if (System.IO.File.Exists(shortCutLinkFilePath))
                    System.IO.File.Delete(shortCutLinkFilePath);
            }
        }

        /// <summary>
        /// Remove shortcut if exist
        /// </summary>
        public static void RemoveShortcut()
        {
            var shortCutLinkFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\ProsecInc.lnk";
            // If you exists shortcude, delete shortcut
            if (System.IO.File.Exists(shortCutLinkFilePath))
                System.IO.File.Delete(shortCutLinkFilePath);
        }

        /// <summary>
        /// Check app running
        /// </summary>
        /// <returns></returns>
        public static bool AppIsRunning()
        {
            var asm = Assembly.GetExecutingAssembly();
            var guid = asm.GetType().GUID.ToString();

            using (var mutex = new Mutex(false, "Global\\" + guid))
            {
                if (mutex.WaitOne(0, false))
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// Get version windown
        /// </summary>
        /// <returns></returns>
        public static int GetOSName()
        {
            OperatingSystem osInfo = System.Environment.OSVersion;
            string version = osInfo.Version.Major.ToString() + "." + osInfo.Version.Minor.ToString();
            switch (version)
            {
                case "10.0":
                    return (int)WindowEnum.Win10_S2016;
                case "6.3":
                    return (int)WindowEnum.Win81_S2012R2;
                case "6.2":
                    return (int)WindowEnum.Win8_S2012;
                case "6.1":
                    return (int)WindowEnum.Win7_S2008R2;
                case "6.0":
                    return (int)WindowEnum.WinVista_S2008;
                case "5.2":
                    return (int)WindowEnum.WinXP_S2003R2;
                case "5.1":
                    return (int)WindowEnum.WinXP;
                case "5.0":
                    return (int)WindowEnum.Win2000;
                default:
                    break;
            }
            return 0;
        }

        /// <summary>
        /// Kiểm tra thuộc tính của dynamic object
        /// </summary>
        /// <param name="checkObject">Đối tượng cần kiểm tra</param>
        /// <param name="name">Thuộc tính</param>
        /// <returns></returns>
        public static bool PropertyExists(dynamic checkObject, string name)
        {
            if (checkObject is ExpandoObject)
                return ((IDictionary<string, object>)checkObject).ContainsKey(name);

            return checkObject.GetType().GetProperty(name) != null;
        }
    }
}
