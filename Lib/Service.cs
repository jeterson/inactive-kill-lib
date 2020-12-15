namespace Lib
{
    using System;
    using System.Windows.Forms;

    public class Service
    {
        private Timer timer = new Timer();
        private DAO_KillSession kill = new DAO_KillSession();
        private int intervalo;

        public Service(int intervalo)
        {
            this.timer.Tick += new EventHandler(this.Tick);
        }

        public void Start()
        {
            this.timer.Interval = (this.Intervalo == 0) ? 0xea60 : this.Intervalo;
            this.timer.Start();
            LoggerApi.LogInfo("Servico Iniciado");
        }

        public void Stop()
        {
            this.timer.Stop();
            LoggerApi.LogInfo("Servico Parado");
        }

        private void Tick(object sender, EventArgs e)
        {
            LoggerApi.LogInfo("Executando Script...");
            if (Configurator.UsaEspelhamento)
            {
                this.kill.KillSessionEspelhamento();
            }
            else
            {
                this.kill.KillSessionPadrao();
            }
        }

        public int Intervalo
        {
            get {
                return this.intervalo * 0xea60;
            }
            set
            {
                this.intervalo = value;
            }
        }
    }
}

