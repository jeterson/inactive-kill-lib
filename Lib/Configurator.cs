namespace Lib
{
    using Lib.Properties;
    using System;
    using System.IO;
    using System.Management;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class Configurator
    {
        public const string VERSION_LIB = "1.0.7";
        private static string directoryPath;

        public static string[] ExtractIps()
        {
            try
            {
                return Ips.Split(new char[] { ';' });
            }
            catch
            {
                return null;
            }
        }

        public static string GenerateLicensa()
        {
            return "MY-SECRET-KEY"; //AESCrypto.Encrypt(GetKeyRegistro());
        }

        public static string GenerateLicensaTrial()
        {
            return AESCrypto.Encrypt(GetKeyRegistro());
        }

        public static string GetCPUId()
        {
            try
            {
                string str = string.Empty;
                ManagementObjectCollection.ManagementObjectEnumerator objA = new ManagementClass("Win32_Processor").GetInstances().GetEnumerator();
                try
                {
                    while (true)
                    {
                        if (!objA.MoveNext())
                        {
                            break;
                        }
                        ManagementObject current = (ManagementObject)objA.Current;
                        if (str == string.Empty)
                        {
                            str = current.Properties["ProcessorId"].Value.ToString();
                        }
                    }
                }
                finally
                {
                    if (!ReferenceEquals(objA, null))
                    {
                        objA.Dispose();
                    }
                }
                return str;
            }
            catch
            {
                return "0";
            }
        }

        public static string GetKeyRegistro()
        {
            string cPUId = GetCPUId();
            return AESCrypto.Encrypt(GetMACAddress() + "_" + cPUId);
        }

        public static string GetKeyRegistroTrial()
        {
            string cPUId = GetCPUId();
            return AESCrypto.Encrypt(GetMACAddress() + "_" + cPUId);
        }

        public static string GetMACAddress()
        {
            try
            {
                string str = string.Empty;
                ManagementObjectCollection.ManagementObjectEnumerator objA = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances().GetEnumerator();
                try
                {
                    while (true)
                    {
                        if (!objA.MoveNext())
                        {
                            break;
                        }
                        ManagementObject current = (ManagementObject)objA.Current;
                        if ((str == string.Empty) && ((bool)current["IPEnabled"]))
                        {
                            str = current["MacAddress"].ToString();
                        }
                        current.Dispose();
                    }
                }
                finally
                {
                    if (!ReferenceEquals(objA, null))
                    {
                        objA.Dispose();
                    }
                }
                return str.Replace(":", " ");
            }
            catch
            {
                return "0";
            }
        }

        public static string GetPath()
        {
            if (ReferenceEquals(directoryPath, null))
            {
                directoryPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            }
            return directoryPath;
        }

        public static void Load()
        {
            DBUsuario = AESCrypto.Decrypt(Settings.Default.DBUsuario);
            DBSenha = AESCrypto.Decrypt(Settings.Default.DBSenha);
            DBPorta = AESCrypto.Decrypt(Settings.Default.DBPorta);
            DBHost = AESCrypto.Decrypt(Settings.Default.DBHost);
            DBDataSource = AESCrypto.Decrypt(Settings.Default.DBDataSource);
            Script = AESCrypto.Decrypt(Settings.Default.DBScript);
            Ips = AESCrypto.Decrypt(Settings.Default.DBIps);
            UsaEspelhamento = Settings.Default.Espelhamento;
            Trial = Settings.Default.Trial;
            KeyLicense = Trial ? AESCrypto.Decrypt(Settings.Default.KeyLicenseTrial) : AESCrypto.Decrypt(Settings.Default.KeyLicense);
            if (string.IsNullOrWhiteSpace(Script))
            {
                Script = Settings.Default.ScriptPadrao;
            }
        }

        public static void Save()
        {
            Settings.Default.DBUsuario = AESCrypto.Encrypt(DBUsuario);
            Settings.Default.DBSenha = AESCrypto.Encrypt(DBSenha);
            Settings.Default.DBScript = AESCrypto.Encrypt(Script);
            Settings.Default.DBPorta = AESCrypto.Encrypt(DBPorta);
            Settings.Default.DBDataSource = AESCrypto.Encrypt(DBDataSource);
            Settings.Default.DBIps = AESCrypto.Encrypt(Ips);
            Settings.Default.DBHost = AESCrypto.Encrypt(DBHost);
            Settings.Default.Espelhamento = UsaEspelhamento;
            Settings.Default.KeyLicense = AESCrypto.Encrypt(KeyLicense);
            Settings.Default.Trial = Trial;
            Settings.Default.Save();
        }

        public static string DBUsuario { get; set; }

        public static string DBSenha { get; set; }

        public static string DBPorta { get; set; }

        public static string DBDataSource { get; set; }

        public static string DBHost { get; set; }

        public static bool UsaEspelhamento { get; set; }

        public static string Ips { get; set; }

        public static string Script { get; set; }

        public static string KeyLicense { get; set; }

        public static bool Trial { get; set; }
    }
}

