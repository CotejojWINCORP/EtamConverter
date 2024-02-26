namespace EtamConverter.Models
{
    public class AggChannels
    {
        protected string _WOAggChannelCode = null;
        protected bool _ActiveChannel = false;
        protected bool _ActiveLeadChannel = false;
        protected string _WOLeadChannelCode = null;
        protected long _EtamCode = -1;

        public AggChannels(string WOAggChannelCode, bool ActiveChannel = true)
        {
            _WOAggChannelCode = WOAggChannelCode;
            _ActiveChannel = ActiveChannel;
        }

        public bool ActiveChannel
        {
            get { return _ActiveChannel; }
        }

        public bool ActiveLeadChannel
        {
            get { return _ActiveLeadChannel; }
        }

        public string WOLeadChannelCode
        {
            get { return _WOLeadChannelCode; }
            set
            {
                _WOLeadChannelCode = value;
                _ActiveLeadChannel = !string.IsNullOrEmpty(value);
            }
        }

        public long EtamCode
        {
            get { return _EtamCode; }
            set { _EtamCode = value; }
        }
    }
}
