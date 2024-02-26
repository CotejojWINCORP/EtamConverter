using EtamConverter.Models;

namespace EtamConverter.Methods
{
    public class WOSpotList
    {
        protected List<WOSpot> _Spots;

        public WOSpotList()
        {
            _Spots = new List<WOSpot>();
        }

        public void Add(WOSpot spot)
        {
            _Spots.Add(spot);
        }

        public List<WOSpot> Spots()
        {
            return _Spots;
        }
    }
}
