using EtamConverter.Data;
using EtamConverter.Models;
using MySql.Data.MySqlClient;

namespace EtamConverter.Methods
{
    public class EtamConverterMethods
    {

        private MYSQL_DBIO? _MySQLDBIO;

        private Channel_Set? _ChannelSet;
        private AggChannel_Set? _AggChannelSet;

        public MYSQL_DBIO MySQLDBIO => _MySQLDBIO;
        public void InitDBConnection(string connectionString)
        {

            try
            {
                _MySQLDBIO = new MYSQL_DBIO(connectionString);
                Console.WriteLine("Database State is " + _MySQLDBIO.MySQLConn.State);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error initializing MYSQLDBIO: " + ex.Message);
            }

        } 
        public  bool ConvertToEtam ( string WOInputFilePath, string EtamOutputFile , string connectionString, bool AggMode = false  )
        {

          

           WOSpotList wSpotList = new WOSpotList();
           ReadWOFile ReadWO = new ReadWOFile();
           ReadWO.ReadFile(WOInputFilePath, ref wSpotList, "");

           EtamSpotList eSpotList = new EtamSpotList();

           InitDBConnection(connectionString);


            if (_MySQLDBIO == null)
            {
                Console.WriteLine("MYSQLDBIO is null. Please ensure it's properly initialized.");
                // Handle the null case as needed
                return false; // or throw an exception
            }

             _ChannelSet = new Channel_Set(_MySQLDBIO);

             _AggChannelSet = new AggChannel_Set(_MySQLDBIO);

            try
            {
                if (AggMode)
                {
                    ConvertAggMarketSpots(wSpotList, eSpotList);
                }
                else
                {
                    ConvertSubMarketSpots(wSpotList, eSpotList);
                }
            }
            catch (Exception ex)
            {
                // Malfunction in breakfast dispenser...

            }

            WriteEtamFile a = new WriteEtamFile();
            a.WriteEtamFileNow(EtamOutputFile,ref eSpotList , ref wSpotList);
            return true;
        }

        private void ConvertAggMarketSpots(WOSpotList wSpotList, EtamSpotList eSpotList)
        {
            // Sub Market Conversion ...
            long SpotCounter = 0;

            // Massage our WO spots into our Ariana Spots

            // In Non-Agg Mode, loop through all of the spots, processing as we go...
            foreach (WOSpot wSpot in wSpotList.Spots())
            {
                // Do the channel conversion, if not found, skip ???
                var PC = _ChannelSet.GetChannel(wSpot.ChannelsPlaced); // Find our channel code... (in case)

                if (!PC.ActiveChannel) // NOT A Valid Channel, so skip it straight away...
                {
                    continue;
                }

                EtamSpot eSpot = new EtamSpot();

                // Set up our spot, although we may still 'abandon' it here ...
                if (wSpot.ChGroupCode.Length > 0) // Active Group Channel Code ...
                {
  
                    var PAC = _AggChannelSet.GetAggChannel(wSpot.ChGroupCode);

                    if (PAC.ActiveLeadChannel) // We have a LEAD Channel, so it MUST match to be included...
                    {
                        if (wSpot.ChannelsPlaced != PAC.WOLeadChannelCode) // Not our "Lead" Channel.
                        {
                            continue; // skip to the next spot
                        }
                        eSpot.EtamCode = PAC.EtamCode; // Use our Agg channel code...
                    }
                    else // We're booked "solus" ...
                    {
                        eSpot.EtamCode = PC.EtamCode;
                    }
                }
                else // Probably a filler spot, use the sub-market lookup & include it...
                {
                    eSpot.EtamCode = PC.EtamCode;
                }

                SpotCounter++;

                eSpot.SpotNo = Convert.ToInt32(SpotCounter);
                eSpot.AirDate = wSpot.AirDate;
                eSpot.AirTime = wSpot.AirTime;
                eSpot.Programme = wSpot.Programme;
                eSpot.Rate = wSpot.Rate;

                eSpotList.Add(eSpot); // All good if we made it here, so add it to the list...
            }
        }

        private void ConvertSubMarketSpots(WOSpotList wSpotList, EtamSpotList eSpotList)
        {
            // Sub Market Conversion ...
            long SpotCounter = 0;

            // Massage our WO spots into our Ariana Spots

            // In Non-Agg Mode, loop through all of the spots, processing as we go...
            foreach (WOSpot wSpot in wSpotList.Spots())
            {
                // Do the channel conversion, if not found, skip ???
                var PC = _ChannelSet.GetChannel(wSpot.ChannelsPlaced);
                if (!PC.ActiveChannel)
                {
                    continue; // Skip to the next spot
                }

                EtamSpot eSpot = new EtamSpot();
                SpotCounter++;

                eSpot.SpotNo = Convert.ToInt32(SpotCounter);
                eSpot.AirDate = wSpot.AirDate;
                eSpot.AirTime = wSpot.AirTime;
                eSpot.EtamCode = PC.EtamCode;
                eSpot.Programme = wSpot.Programme;
                eSpot.Rate = wSpot.Rate;

                // Check if we're booked Agg, in case we need to zero the $$$$
                if (wSpot.ChGroupCode.Length > 0) // Active Group Channel Code ...
                {

                    var PAC = _AggChannelSet.GetAggChannel(wSpot.ChGroupCode);
                    if (PAC.ActiveChannel) // Found ...
                    {
                        if (wSpot.ChannelsPlaced != PAC.WOLeadChannelCode) // NOT our 'lead channel...
                        {
                            eSpot.Rate = 0;
                        }
                    }
                }

                eSpotList.Add(eSpot);
            }
        }


    }
}
