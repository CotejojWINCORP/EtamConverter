using EtamConverter.Models;
using Microsoft.VisualBasic.FileIO;

namespace EtamConverter.Methods
{
    public class ReadWOFile
    {
        public void ReadFile(string InputFilePath, ref WOSpotList SpotList, string ErrorPath)
        {
            // Spot list is 'new' upon initial call!!!
            // Open & Read the supplied filename for WO Spots, loading into 'SpotList' 
            SpotList = new WOSpotList();

            TextFieldParser WOInput = null;

            try
            {
                WOInput = new TextFieldParser(InputFilePath);
                WOInput.TextFieldType = FieldType.Delimited;
                WOInput.SetDelimiters(",");
                WOInput.HasFieldsEnclosedInQuotes = true;
                WOInput.TrimWhiteSpace = false;

                if (!WOInput.EndOfData)
                {
                    WOInput.ReadLine(); // Skip over header row...
                }
                if (!WOInput.EndOfData)
                {
                    WOInput.ReadLine(); // Skip over blank line after header row ...
                }

                string[] SpotRow;

                while (!WOInput.EndOfData)
                {
                    // Check 'peek' for End of spots/"Total" line...
                    if (WOInput.PeekChars(1)[0] <= ' ')
                    {
                        // "" total Line or 'Blank line' before total...
                        // This is NOT working; need to figure out how to stop at a blank line!!!
                        break;
                    }
                    SpotRow = WOInput.ReadFields();

                    WOSpot Spot = new WOSpot();
                    Spot.Advertiser = SpotRow[WOReader.ADVERTISER_IDX];
                    Spot.ChGroupCode = SpotRow[WOReader.CHGROUPCODE_IDX];
                    Spot.ChannelsPlaced = SpotRow[WOReader.CHANNELSPLACED_IDX];
                    Spot.AirDateasDDMMYYYYstring = SpotRow[WOReader.AIRDATE_IDX];
                    Spot.Rateasstring = SpotRow[WOReader.RATE_IDX];
                    Spot.Programme = SpotRow[WOReader.PROGRAMME_IDX];
                    Spot.AirTimeasHHMMSSXXstring = SpotRow[WOReader.AIRTIME_IDX];
                    Spot.AirLength = SpotRow[WOReader.AIRLENGTH_IDX]; // (just a string for now, not used ...)
                    Spot.OrderNo = SpotRow[WOReader.ORDERNO_IDX];
                    Spot.AdvProd = SpotRow[WOReader.ADV_PROD_IDX];
                    Spot.BreakCode = SpotRow[WOReader.BREAK_CODE_IDX];

                    SpotList.Add(Spot);
            
                }
            }
            catch (Exception ex)
            {
                // Malfunction in breakfast dispenser ...
            }
            
            // Close File
            WOInput.Close();
        }


        // Reader Class

        public static class WOReader
        {
            public const int ADVERTISER_IDX = 0;
            public const int CHGROUPCODE_IDX = ADVERTISER_IDX + 1;
            public const int CHANNELSPLACED_IDX = CHGROUPCODE_IDX + 1;
            public const int AIRDATE_IDX = CHANNELSPLACED_IDX + 1;
            public const int RATE_IDX = AIRDATE_IDX + 1;
            public const int PROGRAMME_IDX = RATE_IDX + 1;
            public const int AIRTIME_IDX = PROGRAMME_IDX + 1;
            public const int AIRLENGTH_IDX = AIRTIME_IDX + 1;
            public const int ORDERNO_IDX = AIRLENGTH_IDX + 1;
            public const int ADV_PROD_IDX = ORDERNO_IDX + 1;
            public const int BREAK_CODE_IDX = ADV_PROD_IDX + 1;
        }

    }
}
