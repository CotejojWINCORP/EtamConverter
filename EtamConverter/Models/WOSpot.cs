

using EtamConverter.Methods;

namespace EtamConverter.Models
{
    public class WOSpot
    {
        public string Advertiser { get; set; }
        public string ChGroupCode { get; set; }
        public string ChannelsPlaced { get; set; }
        protected DateTime _AirDate; // Date Value...
        protected decimal _Rate;
        public string Programme { get; set; }
        protected JulianTime _AirTime = new JulianTime(); // Custom Time value to allow for 'AM/PM/XM' & 30-hour clock.
        public string AirLength { get; set; } // Not used by Arianna, so soak it up as a string for the moment ...
        public string OrderNo { get; set; }
        public string AdvProd { get; set; }
        public string BreakCode { get; set; }

        public string AirDateasDDMMYYYYstring
        {
            set
            {
                try
                {
                    _AirDate = DateTime.ParseExact(value, "d/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                }
                catch (Exception ex)
                {
                    // Handle parsing exception
                }
            }
            get
            {
                return _AirDate.ToString("dd/MM/yyyy");
            }
        }

        public DateTime AirDate
        {
            get
            {
                return _AirDate;
            }
            set
            {
                _AirDate = value;
            }
        }

        // todo Custom Processing of WO Time AM/PM/XM ...

        public string AirTimeasHHMMSSXXstring
        {
            get
            {
                return _AirTime.WO_HHMMSS_XM;
            }
            set
            {
                try
                {
                    _AirTime.WO_HHMMSS_XM = value;
                }
                catch (Exception ex)
                {
                    // Handle parsing exception
                }
            }
        }

        public JulianTime AirTime
        {
            get
            {
                return _AirTime;
            }
            set
            {
                _AirTime = value;
            }
        }

        public string Rateasstring
        {
            set
            {
                try
                {
                    _Rate = Convert.ToDecimal(value);
                }
                catch (Exception ex)
                {
                    _Rate = 0;
                }
            }
            get
            {
                return _Rate.ToString("$###,###,##0.00");
            }
        }

        public decimal Rate
        {
            get
            {
                return _Rate;
            }
            set
            {
                _Rate = value;
            }
        }
    }
}
