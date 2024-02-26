using MySql.Data.MySqlClient;
using System.Runtime.CompilerServices;

namespace EtamConverter.Data
{
    public class DB_CHANNELS : MYSQL_IO
    {
        public const string DB__CHANNELS = "Channels";
        public const string DB_WO_CHANNEL_CODE = "WOChannelCode"; // WO Channel Code
        public const string DB_MARKET_DESC = "MarketDesc"; // Market Desc.
        public const string DB_CHANNEL_NAME = "ChannelName"; // Channel Name
        public const string DB_AGG_FLAG = "AggFlag"; // Aggregation Flag
        public const string DB_ETAM_CODE = "EtamCode"; // Ariana Channel Code
        public const string DB_LUDT = "LastUpdateDateTime"; // Row Last Update Date Time (SQL Svr time!)

        // Primary Key Field Params
        public const string DB_P_CHANNEL_CODE = "@p" + DB_WO_CHANNEL_CODE;

        public DB_CHANNELS(MYSQL_DBIO MySQLDB) : base(MySQLDB)
        {
            // Initialise our SystemControl Class & Data Adapter...

        }

        public MySqlCommand SetSelect()
        {
            _MySQLCmd.CommandText = "SELECT * FROM " + DB__CHANNELS;
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
