using EtamConverter.Models;

namespace EtamConverter.Methods
{
    public class EtamSpotList
    {
        public List<EtamSpot> _Spots;

        public EtamSpotList()
        {
            _Spots = new List<EtamSpot>();
        }

        public void Add(EtamSpot spot)
        {
            _Spots.Add(spot);
        }

        public List<EtamSpot> Spots()
        {
            return _Spots;
        }
    }
}
