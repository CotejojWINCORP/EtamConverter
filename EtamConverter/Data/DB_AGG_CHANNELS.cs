using MySql.Data.MySqlClient;


namespace EtamConverter.Data
{
    public class DB_AGG_CHANNELS: MYSQL_IO
    {

        public const string DB__AGG_CHANNELS = "AggChannels";
        public const string DB_WO_AGG_CHANNEL_CODE = "WOAggChannelCode"; // WO Agg Channel Code
        public const string DB_NETWORK_BUY_DESC = "NetwrokBuyDesc"; // Agg Channel Desc.
        public const string DB_WO_LEAD_CHANNEL_CODE = "WOLeadChannelCode"; // WO Lead Channel Code for this Agg Channel
        public const string DB_LEAD_CHANNEL_DESC = "LeadChannelDesc"; // Lead Channel Desc.
        public const string DB_LUDT = "LastUpdateDateTime"; // Row Last Update Date Time (SQL Svr time!)

        // Primary Key Field Params 
        public const string DB_P_WO_AGG_CHANNEL_CODE = "@p" + DB_WO_AGG_CHANNEL_CODE;

        public DB_AGG_CHANNELS(MYSQL_DBIO MySQLDB) : base(MySQLDB)
        {
            try
            {
                var connectionString = "Server=localhost;Database=etamconverter;Uid=dbuser;Password=jomsql_123;";
                MySQLDB = new MYSQL_DBIO(connectionString);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during initialization
                Console.WriteLine("Error initializing MYSQLDBIO: " + ex.Message);
                throw; // Optionally, you can choose to throw or handle the exception here
            }

        }

        public MySqlCommand SetSelect()
        {
            // Q&D Select
            _MySQLCmd.CommandText = "SELECT * FROM " + DB__AGG_CHANNELS;
            return _MySQLCmd;
        }

        public MySqlCommand SetInsert()
        {
            // TBA
            return _MySQLCmd;
        }

        public MySqlCommand SetUpdate()
        {
            // TBA
            return _MySQLCmd;
        }
    }
}
