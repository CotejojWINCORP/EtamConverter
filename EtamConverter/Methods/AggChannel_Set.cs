using EtamConverter.Data;
using EtamConverter.Models;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;

namespace EtamConverter.Methods
{
    public class AggChannel_Set
    {
        protected MYSQL_DBIO _MySQLDB;
        protected Dictionary<string, AggChannels> _AggChannelSet = null;

        public AggChannel_Set(MYSQL_DBIO MySQLDB)
        {
            _MySQLDB = MySQLDB;
            _AggChannelSet = new Dictionary<string, AggChannels>();
        }

        public AggChannels GetAggChannel(string WOAggChannelCode)
        {
            if (_AggChannelSet.ContainsKey(WOAggChannelCode))
            {
                return _AggChannelSet[WOAggChannelCode];
            }

            if (DBReadAggChannel(WOAggChannelCode))
            {
                return _AggChannelSet[WOAggChannelCode];
            }

            var PAC = new AggChannels(WOAggChannelCode, false);
            _AggChannelSet.Add(WOAggChannelCode, PAC);
            return PAC;
        }

        private bool DBReadAggChannel(string WOAggChannelCode)
        {
            using (var dReader = ReadAggChannelFromDB(WOAggChannelCode))
            {
                if (!dReader.Read())
                {
                    return false;
                }

                var PAC = new AggChannels(WOAggChannelCode)
                {
                    WOLeadChannelCode = MySQLDB.DBstring2string(dReader[DB_AGG_CHANNELS.DB_WO_LEAD_CHANNEL_CODE]),
                    EtamCode = MySQLDB.DBlong2long(dReader[DB_CHANNELS.DB_ETAM_CODE])
                };

                _AggChannelSet.Add(WOAggChannelCode, PAC);
                return true;
            }
        }

        private MySqlDataReader ReadAggChannelFromDB(string WOAggChannelCode)
        {
            try
            {
                var mySQLChannels = new DB_CHANNELS(_MySQLDB);
                Console.WriteLine(mySQLChannels.ToString());
                var mySQLCmd = mySQLChannels.MySQLCmd;
                mySQLCmd.CommandText = "select " +
                                       DB_AGG_CHANNELS.DB_WO_AGG_CHANNEL_CODE + ", " +
                                       DB_AGG_CHANNELS.DB_WO_LEAD_CHANNEL_CODE + ", " +
                                       DB_CHANNELS.DB_ETAM_CODE +
                                       " from " + DB_AGG_CHANNELS.DB__AGG_CHANNELS + " as AC " +
                                       "left join " + DB_CHANNELS.DB__CHANNELS + " as C " +
                                       "on AC." + DB_AGG_CHANNELS.DB_WO_AGG_CHANNEL_CODE + " = C." + DB_CHANNELS.DB_WO_CHANNEL_CODE + " " +
                                       "where " + mySQLChannels.SQLFParam(DB_AGG_CHANNELS.DB_WO_AGG_CHANNEL_CODE);
                mySQLChannels.AddFParam(DB_AGG_CHANNELS.DB_WO_AGG_CHANNEL_CODE, WOAggChannelCode);
                mySQLChannels.WaitOne();
                mySQLCmd.Prepare();
                return mySQLCmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Debug.Print("Read WO Agg Channel: Excp [" + ex.Message + "]");
                throw; // You might want to handle the exception here
            }
        }
    }
}
