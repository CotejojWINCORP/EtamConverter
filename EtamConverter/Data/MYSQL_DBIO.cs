using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace EtamConverter.Data
{
    public class MYSQL_DBIO
    {
        public const int DB_CONNECTED = 0;
        public const int DB_RECONNECTED = 1;
        public const int DB_RECONNECT_PENDING = 8;
        public const int DB_RECONNECT_FAILED = 9;

        protected MySqlConnection _MySQLConn;
        protected Mutex _MySQLMutex = null;

        public MYSQL_DBIO(string ConnectString)
        {
            _MySQLMutex = new Mutex();
            _MySQLMutex.WaitOne();

            try
            {
                _MySQLConn = new MySqlConnection(ConnectString);
                DBConnect();
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                _MySQLMutex.ReleaseMutex();
                throw;
            }

            _MySQLMutex.ReleaseMutex();
        }

        private void DBConnect()
        {
            try
            {
                _MySQLConn.Close();
                _MySQLConn.Open();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                throw;
            }
        }

        public MySqlConnection MySQLConn
        {
            get { return _MySQLConn; }
        }

        public void WaitOne()
        {
            _MySQLMutex.WaitOne();
        }

        public void ReleaseMutex()
        {
            _MySQLMutex.ReleaseMutex();
        }
    }
}
