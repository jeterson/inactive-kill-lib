namespace Lib
{
    using System;
    using System.Diagnostics;

    public class ExecutaComando
    {
        private string[] comandoStart;
        private string[] comandoStop;

        public ExecutaComando(string[] comandoStart, string[] comandoStop)
        {
            this.comandoStart = comandoStart;
            this.comandoStop = comandoStop;
        }

        public void ExecuteStart()
        {
            try
            {
                string[] comandoStart = this.comandoStart;
                int index = 0;
                while (true)
                {
                    if (index >= comandoStart.Length)
                    {
                        break;
                    }
                    string str = comandoStart[index];
                    Process objA = new Process();
                    try
                    {
                        objA.StartInfo.FileName = Environment.GetEnvironmentVariable("comspec");
                        objA.StartInfo.Arguments = "/c {str}";
                        objA.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        objA.StartInfo.RedirectStandardOutput = true;
                        objA.StartInfo.UseShellExecute = false;
                        objA.StartInfo.CreateNoWindow = true;
                        objA.Start();
                        objA.WaitForExit();
                    }
                    finally
                    {
                        if (!ReferenceEquals(objA, null))
                        {
                            objA.Dispose();
                        }
                    }
                    index++;
                }
            }
            catch (Exception exception)
            {
                LoggerApi.LogError("Erro ao exectar comando Iniciar:  - Erro original: " + exception.Message);
            }
        }

        public void ExecuteStop()
        {
            try
            {
                string[] comandoStop = this.comandoStop;
                int index = 0;
                while (true)
                {
                    if (index >= comandoStop.Length)
                    {
                        break;
                    }
                    string str = comandoStop[index];
                    Process objA = new Process();
                    try
                    {
                        objA.StartInfo.FileName = Environment.GetEnvironmentVariable("comspec");
                        objA.StartInfo.Arguments = "/c {str}";
                        objA.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        objA.StartInfo.RedirectStandardOutput = true;
                        objA.StartInfo.UseShellExecute = false;
                        objA.StartInfo.CreateNoWindow = true;
                        objA.Start();
                        objA.WaitForExit();
                    }
                    finally
                    {
                        if (!ReferenceEquals(objA, null))
                        {
                            objA.Dispose();
                        }
                    }
                    index++;
                }
            }
            catch (Exception exception)
            {
                LoggerApi.LogError("Erro ao exectar comando Parar: - Erro original: " + exception.Message);
            }
        }
    }
}

