using EtamConverter.Models;
using MySqlX.XDevAPI;

namespace EtamConverter.Methods
{
    public class WriteEtamFile
    {
        public  void  WriteEtamFileNow(string OutputFilePath, ref EtamSpotList SpotList , ref WOSpotList WSpotList)
        {
            // Spot list is 'loaded' and ready to go, no further conversion required...
            // Open & Read the supplied filename for WO Spots, loading into '_WOSpotList' 
            using (StreamWriter Output = new StreamWriter(OutputFilePath))
            {
                string ALine = string.Empty;
                const string ADelim = "\t;";
                const string separator = ";";

                try
                {
                    //=== Write Header Stuff ===
                    ALine = "<START>";
                    Output.WriteLine(ALine);
                    string ClientName = WSpotList.Spots().First().Advertiser;
                    ALine = "<CLIENT>" + ClientName;
                    Output.WriteLine(ALine);
                    string ProductDes = WSpotList.Spots().First().AdvProd;
                    ALine = "<DESC>" + ProductDes;
                    Output.WriteLine(ALine);
                    ALine = "<SPOTS> " + SpotList.Spots().Count + ADelim;
                    Output.WriteLine(ALine);

                    foreach (EtamSpot Spot in SpotList.Spots())
                    {
                        ALine = Spot.SpotNo.ToString("000") + separator;
                        ALine += Spot.AirDateasYYYYMMDDstring + separator;
                        ALine += Spot.AirTimeasHHMMstring + separator;
                        ALine += Spot.EtamCodeasNNNNstring+ separator; 
                        ALine += Spot.Programme + separator;
                        ALine += Spot.Rateasstring + separator;
                        Output.WriteLine(ALine);
                    }
                    ALine = "<END>";
                    Output.WriteLine(ALine);
                    Console.WriteLine("File has been re written");
                }
                catch (Exception ex)
                {
                    // Handle exceptions as needed

                }
                Console.WriteLine("Closiung?");
                Output.Close();
            }
        }
    }
}
