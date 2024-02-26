using EtamConverter.Methods;

namespace EtamConverter.Models
{
    public class EtamSpot
    {
        public int SpotNo { get; set; } = -1;
        protected DateTime _AirDate;
        protected JulianTime _AirTime;
        protected long _EtamCode = -1;
        public string Programme { get; set; } = string.Empty;
        protected decimal _Rate = 0;

        public DateTime AirDate
        {
            get { return _AirDate; }
            set { _AirDate = value; }
        }

        public string AirDateasYYYYMMDDstring
        {
            set
            {
                try
                {
                    _AirDate = DateTime.ParseExact(value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                }
                catch (Exception)
                {
                    // Handle the exception if necessary
                }
            }
            get { return _AirDate.ToString("yyyyMMdd"); }
        }

        public string AirTimeasHHMMstring
        {
            get { return _AirTime.HHMM; }
            set
            {
                try
                {
                    _AirTime.HHMM = value;
                }
                catch (Exception)
                {
                    // Handle the exception if necessary
                }
            }
        }

        public JulianTime AirTime
        {
            get { return _AirTime; }
            set { _AirTime = value; }
        }

        public string Rateasstring
        {
            set { _Rate = Convert.ToDecimal(value); }
            get { return _Rate.ToString("########0"); }
        }

        public decimal Rate
        {
            get { return _Rate; }
            set { _Rate = value; }
        }

        public long EtamCode
        {
            get { return _EtamCode; }
            set { _EtamCode = value; }
        }

        public string EtamCodeasNNNNstring
        {
            get { return _EtamCode.ToString("0000"); }
            set { _EtamCode = Convert.ToInt64(value); }
        }
    }
}
