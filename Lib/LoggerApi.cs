namespace Lib
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    public class LoggerApi
    {
        private static bool _init = true;
        private static string _logname = "log";

        private static string getDate()
        {




            return
                DateTime.Now.Year.ToString("D4") + "-" +
                DateTime.Now.Month.ToString("D2") + "-" +
                DateTime.Now.Day.ToString("D2") + " " +

                 DateTime.Now.Hour.ToString("D2") + ":" +
                  DateTime.Now.Minute.ToString("D2") + ":" +
                  DateTime.Now.Second.ToString("D2");


        }

        private static void Log(string level, string message)
        {
            
            String str1 = "[" + getDate() + "] [" + level + "] " + message + "\r\n";
            String str2 = DateTime.Now.Year.ToString("D4") + "_" + DateTime.Now.Month.ToString("D2") + DateTime.Now.Day.ToString("D2");

            TextWriter objA = null;
            try
            {
                string path = Configurator.GetPath() + @"\logs";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                objA = new StreamWriter(path + @"\" + str2 + ".txt", true);
                if (_init)
                {
                    objA.Write("\r\n");
                    _init = false;
                }
                objA.Write(str1);
                Console.Write(str1);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Console.WriteLine("Erro ao salvar o arquivo de log do sistema\nDetalhes: " + exception.Message);
            }
            finally
            {
                try
                {
                    if (!ReferenceEquals(objA, null))
                    {
                        objA.Close();
                    }
                }
                catch
                {
                }
            }
        }

        public static void LogError(string message)
        {
            Log("ERROR", message);
        }

        public static void LogInfo(string message)
        {
            Log("INFO", message);
        }

        public static void LogWarning(string message)
        {
            Log("WARNING", message);
        }

        public static string LogName
        {
            get
            {
                return _logname;
            }
            set
            {
                _logname = value;
            }
        }
    }
}

