namespace Lib
{
    using System;
    using System.Data.OracleClient;
    using System.Text;

    public class DAO_KillSession
    {
        private DAO_Oracle dao_oracle = new DAO_Oracle();
        private string[] ips_servidores = Configurator.ExtractIps();
        private int loop = 0;

        private bool KillSession(string ip)
        {
            bool flag;
            OracleConnection connection = this.dao_oracle.GetConnection(ip);
            try
            {
                connection.Open();
                StringBuilder builder = new StringBuilder();
                builder.Append(Configurator.Script);
                new OracleCommand(builder.ToString(), connection).ExecuteNonQuery();
                LoggerApi.LogInfo("KillSession(): Sessoes matadas");
                flag = true;
            }
            catch (Exception exception)
            {
                LoggerApi.LogWarning("KillSession(): Erro ao matar sessoes, tentando no proxmo servidor...(Mensagem Original: " + exception.Message + ")");
                if (this.loop < this.ips_servidores.Length)
                {
                    this.loop++;
                    this.KillSession(this.ips_servidores[this.loop]);
                    flag = false;
                }
                else
                {
                    this.loop = 0;
                    LoggerApi.LogWarning("KillSession(): Sessoes nao encontradas na lista de servidores");
                    LoggerApi.LogInfo("Retornando ao M\x00e9todo principal");
                    flag = true;
                }
            }
            finally
            {
                this.dao_oracle.CloseConnecition(connection);
            }
            return flag;
        }

        public bool KillSessionEspelhamento()
        {
            bool flag;
            OracleConnection connection = this.dao_oracle.GetConnection();
            string ip = this.ips_servidores[this.loop];
            try
            {
                connection.Open();
                StringBuilder builder = new StringBuilder();
                builder.Append(Configurator.Script);
                new OracleCommand(builder.ToString(), connection).ExecuteNonQuery();
                LoggerApi.LogInfo("KillSessionEspelhamento(): Sessoes Matadas");
                flag = true;
            }
            catch (Exception exception)
            {
                LoggerApi.LogWarning("KillSessionEspelhamento(): Erro ao matar, Partindo para servidores espelhados..(Mensagem Original: " + exception.Message + ")");
                this.KillSession(ip);
                flag = false;
            }
            finally
            {
                this.dao_oracle.CloseConnecition(connection);
            }
            return flag;
        }

        public bool KillSessionPadrao()
        {
            bool flag;
            OracleConnection connection = this.dao_oracle.GetConnection();
            try
            {
                connection.Open();
                StringBuilder builder = new StringBuilder();
                builder.Append(Configurator.Script);
                new OracleCommand(builder.ToString(), connection).ExecuteNonQuery();
                LoggerApi.LogInfo("KIllSessionPadrao(); Sessoes do bloco de select matadas");
                flag = true;
            }
            catch (Exception exception)
            {
                LoggerApi.LogError("KillSessionPadrao(): Erro ao matar sessoes...(Mensagem Original: " + exception.Message + ")");
                flag = false;
            }
            finally
            {
                this.dao_oracle.CloseConnecition(connection);
            }
            return flag;
        }
    }
}

