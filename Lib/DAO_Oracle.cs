namespace Lib
{
    using System;
    using System.Data;
    using System.Data.OracleClient;

    public class DAO_Oracle
    {
        public bool CheckConnection()
        {
            OracleConnection cnx = this.GetConnection();
            bool flag = false;
            try
            {
                cnx.Open();
                flag = true;
            }
            catch (Exception exception)
            {
                LoggerApi.LogError("DAO_Oracle.CheckConnection(): " + exception.Message);
            }
            finally
            {
                this.CloseConnecition(cnx);
            }
            return flag;
        }

        public void CloseConnecition(OracleConnection cnx)
        {
            try
            {
                if ((cnx != null) && (cnx.State != ConnectionState.Closed))
                {
                    cnx.Close();
                    cnx.Dispose();
                }
            }
            catch (Exception exception)
            {
                LoggerApi.LogError("DAO_Oracle.CloseConnection(): " + exception.Message);
            }
        }

        public OracleConnection GetConnection()
        {
            LoggerApi.LogInfo("Str de Conexão");
            
            //string[] strArray = new string[] { "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=", Configurator.DBHost, ")(PORT=", Configurator.DBPorta, ")))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=", Configurator.DBDataSource, ")));User Id=", Configurator.DBUsuario, ";Password=" };
            String str = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + Configurator.DBHost + ")(PORT=" + Configurator.DBPorta + ")))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + Configurator.DBDataSource + ")));User Id=" + Configurator.DBUsuario + ";Password=" + Configurator.DBSenha + ";";
            //strArray[9] = Configurator.DBSenha;
            //strArray[10] = ";";
         //   LoggerApi.LogInfo(string.Concat(strArray));
            return new OracleConnection(str);
            
        }

        public OracleConnection GetConnection(string ip)
        {
            String str = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" +ip + ")(PORT=" + Configurator.DBPorta + ")))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + Configurator.DBDataSource + ")));User Id=" + Configurator.DBUsuario + ";Password=" + Configurator.DBSenha + ";";
            return new OracleConnection(str);
        }
    }
}

