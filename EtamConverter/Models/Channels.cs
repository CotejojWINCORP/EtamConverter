namespace EtamConverter.Models
{
    public class Channels
    {
        public string _WOChannelCode = null;
        public bool _ActiveChannel = false;

        // Transform value/s
        public long _EtamCode = -1;

        public Channels(string WOChannelCode, bool ActiveChannel = true)
        {
            _WOChannelCode = WOChannelCode;
            _ActiveChannel = ActiveChannel;
        }

        public bool ActiveChannel
        {
            get { return _ActiveChannel; }
        }

        public long EtamCode
        {
            get { return _EtamCode; }
            set { _EtamCode = value; }
        }
    }
}
