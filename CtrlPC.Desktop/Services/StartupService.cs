using System;
using System.Diagnostics;
using Microsoft.Win32;

namespace CtrlPC.Desktop.Services
{
    public static class StartupService
    {
        public static void EnsureRegistered()
        {
            string runKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";

            string exePath = Process.GetCurrentProcess().MainModule.FileName;
            string appName = "CrtlPC";

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(runKeyPath, true))
            {
                object currentValue = key.GetValue(appName);

                if (currentValue == null)
                {
                    key.SetValue(appName, exePath);
                }
                else
                {
                    string registeredPath = currentValue.ToString();

                    if (!string.Equals(registeredPath, exePath, StringComparison.OrdinalIgnoreCase))
                    {
                        key.SetValue(appName, exePath);
                    }
                }
            }
        }
    }
}
