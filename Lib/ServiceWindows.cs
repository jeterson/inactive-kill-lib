namespace Lib
{
    using System;
    using System.Windows.Forms;

    public class ServiceWindows
    {
        private Timer timer = new Timer();
        private ExecutaComando executa;
        private int intervalo;

        public ServiceWindows(string[] comandoStart, string[] comandoStop)
        {
            this.executa = new ExecutaComando(comandoStart, comandoStop);
            this.timer.Tick += new EventHandler(this.Tick);
        }

        public void Start()
        {
            this.timer.Interval = (this.Intervalo == 0) ? 0xea60 : this.Intervalo;
            this.timer.Start();
            LoggerApi.LogInfo("Servico Windows Iniciado");
        }

        public void Stop()
        {
            this.timer.Stop();
            LoggerApi.LogInfo("Servico Windows Parado");
        }

        private void Tick(object sender, EventArgs e)
        {
            LoggerApi.LogInfo("Reiniciando Servicos");
            this.executa.ExecuteStop();
            this.executa.ExecuteStart();
        }

        public int Intervalo
        {
            get
            {
                return this.intervalo * 0xea60;
            }
            set
            {
                this.intervalo = value;
            }
        }
    }
}

