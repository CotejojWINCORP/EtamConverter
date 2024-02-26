using MySql.Data.MySqlClient;
using EtamConverter.Data;
using EtamConverter.Models;

namespace EtamConverter.Methods
{
    public class Channel_Set
    {
        protected MYSQL_DBIO _MySQLDB; // DB Connection for Read/s...

        protected Dictionary<string, Channels> _ChannelSet = null;

        public Channel_Set(MYSQL_DBIO MySQLDB)
        {
            _MySQLDB = MySQLDB;
            _ChannelSet = new Dictionary<string, Channels>(); // Create our dictionary set of channels
        }

        // Channel Conversion Set for WO to Ariana Conversion
        // NB: Channels are loaded on an as-needed basis ...

        public  Channels GetChannel(string WOChannelCode)
        {
            // 'Find' & return our Ariana Code (Reading the DB if required...)
            if (_ChannelSet.ContainsKey(WOChannelCode))
            {
                return _ChannelSet[WOChannelCode];
            }

            // Not currently in our dataset, check read DB, Add & return ...
            if (DBReadChannel(WOChannelCode)) // if true, it was read & found !!!
            {
                return _ChannelSet[WOChannelCode];
            }

            // Not found, add an 'inactive' channel to flag the same to save hitting the DB again.
            var PC = new Channels(WOChannelCode, false);
            _ChannelSet.Add(WOChannelCode, PC);
            return PC; // & return 'inactive' channel
        }

        private bool DBReadChannel(string WOChannelCode)
        {
            // Read (& Load Channel Set if found) our WOChannel.
            // Returns 'true' if the channel is found, false if not...
            MySqlDataReader dReader;
            try
            {
                var mySQLChannels = new DB_CHANNELS(_MySQLDB);
                var mySQLCmd = mySQLChannels.MySQLCmd; // Grab our 'connected' Command...
                mySQLCmd.CommandText = "select " + DB_CHANNELS.DB_WO_CHANNEL_CODE + ", " +
                                      DB_CHANNELS.DB_ETAM_CODE + " " +
                                      "from " + DB_CHANNELS.DB__CHANNELS + " " +
                                      "where " +
                                      mySQLChannels.SQLFParam(DB_CHANNELS.DB_WO_CHANNEL_CODE);
                mySQLChannels.AddFParam(DB_CHANNELS.DB_WO_CHANNEL_CODE, WOChannelCode);
                mySQLChannels.WaitOne();
                mySQLCmd.Prepare();
                dReader = mySQLCmd.ExecuteReader();
                mySQLChannels.ReleaseMutex();

                if (!dReader.Read()) // We have no rows ...
                {
                    dReader.Close(); // Release before we exit...
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Read Channel: Exception [" + ex.Message + "]");
                return false;
            }

            // Stash this channel into our list so that we don't have to read it again ...
            var PC = new Channels(WOChannelCode);
            PC.EtamCode = MySQLDB.DBlong2long(dReader[DB_CHANNELS.DB_ETAM_CODE].ToString());

            _ChannelSet.Add(WOChannelCode, PC);

            dReader.Close(); // Release the hounds ...

            return true; // & Return our success, so that the value will be returned from our Dictionary...
        }
    }

}

