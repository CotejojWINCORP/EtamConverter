namespace EtamConverter.Methods
{
    public class JulianTime
    {
        private const int SEC_PER_MIN = 60;
        private const int MIN_PER_HOUR = 60;
        private const int SEC_PER_HOUR = MIN_PER_HOUR * SEC_PER_MIN;

        private int FRAMES_PER_SEC;
        private int FRAMES_PER_MIN;
        private int FRAMES_PER_HOUR;

        private long _Frames = 0;
        private int _FF = 0;
        private int _SS = 0;
        private int _MM = 0;
        private int _HH = 0;
        private int _HH12 = 12;

        public JulianTime(bool is30Fps = false)
        {
            FRAMES_PER_SEC = is30Fps ? 30 : 25;
            FRAMES_PER_MIN = FRAMES_PER_SEC * SEC_PER_MIN;
            FRAMES_PER_HOUR = FRAMES_PER_MIN * MIN_PER_HOUR;
        }

        public JulianTime(JulianTime fromValue, bool is30Fps = false) : this(is30Fps)
        {
            SetFromFrames(fromValue._Frames);
        }

        public JulianTime(long frames, bool is30Fps = false) : this(is30Fps)
        {
            SetFromFrames(frames);
        }

        public JulianTime(string hhMmSsFf, bool is30Fps = false) : this(is30Fps)
        {
            HH_MM_SS_FF = hhMmSsFf;
        }

        public long Frames
        {
            get { return _Frames; }
            set { SetFromFrames(value); }
        }

        public int Seconds
        {
            get { return (_HH * SEC_PER_HOUR + _MM * SEC_PER_MIN + _SS); }
            set { SetFromFrames(value * FRAMES_PER_SEC); }
        }

        public int Minutes
        {
            get { return (_HH * MIN_PER_HOUR + _MM); }
            set { SetFromFrames(value * FRAMES_PER_MIN); }
        }

        public int Hours
        {
            get { return _HH; }
            set { SetFromFrames(value * FRAMES_PER_HOUR); }
        }

        public bool Negative
        {
            get { return _FF < 0; }
        }

        public string HHMM
        {
            get { return _HH.ToString("00") + _MM.ToString("00"); }
            set { SetFromHHMMSSFFString(value); }
        }

        public string HH_MM
        {
            get { return _HH.ToString("00:") + _MM.ToString("00"); }
            set { SetFromHHMMSSFFString(value); }
        }

        public string MM_SS
        {
            get { return _MM.ToString("00:") + _SS.ToString("00"); }
            set { SetFromHHMMSSFFString(value); }
        }

        public string HH_MM_SS
        {
            get { return _HH.ToString("00:") + _MM.ToString("00:") + _SS.ToString("00"); }
            set { SetFromHHMMSSFFString(value); }
        }

        public string HHMMSSFF
        {
            get { return _HH.ToString("00") + _MM.ToString("00") + _SS.ToString("00") + _FF.ToString("00") + _Frames.ToString(" ;-"); }
            set { SetFromHHMMSSFFString(value); }
        }

        public string HH_MM_SS_FF
        {
            get { return _HH.ToString("00:") + _MM.ToString("00:") + _SS.ToString("00:") + _FF.ToString("00") + _Frames.ToString(" ;-"); }
            set { SetFromHHMMSSFFString(value); }
        }

        public string WO_HHMMSS_XM
        {
            get
            {
                string XM = "AM";
                if (_HH >= 12)
                {
                    XM = "PM";
                    if (_HH >= 24)
                    {
                        XM = "XM";
                    }
                }
                return _HH12.ToString("00:") + _MM.ToString("00:") + _SS.ToString("00") + " " + XM;
            }
            set
            {
                int hhOffset = 0;
                if (value.ToUpper().Contains("PM"))
                {
                    hhOffset = 12;
                }
                if (value.ToUpper().Contains("XM"))
                {
                    hhOffset = 24;
                }
                SetFromHHMMSSFFString(value, hhOffset);
            }
        }

        private void SetFromFrames(long frames)
        {
            _Frames = frames;
            _HH = (int)(frames / FRAMES_PER_HOUR);
            _HH12 = _HH % 12;
            if (_HH12 == 0)
            {
                _HH12 = 12;
            }
            _MM = (int)((frames % FRAMES_PER_HOUR) / FRAMES_PER_MIN);
            _SS = (int)(((frames % FRAMES_PER_MIN) / FRAMES_PER_SEC));
            _FF = (int)(frames % FRAMES_PER_SEC);
        }

        private void SetFromHHMMSSFFString(string hhMmSsFf, int hhOffset = -1)
        {
            string wkgStr = CleanString(hhMmSsFf);
            while (wkgStr.Length < 6)
            {
                wkgStr = "0" + wkgStr;
            }
            wkgStr += "00";

            try
            {
                // Trap any non-numerics, but there shouldn't be ???
                _HH = Convert.ToInt32(wkgStr.Substring(0, 2)) + hhOffset; // Offset for AM/PM/XM conversions.
                _HH = Convert.ToInt32(wkgStr.Substring(0, 2));
                if (hhOffset >= 0)
                {
                    _HH = _HH % 12 + hhOffset;
                }
                // NB: _HH12 is set by the call to 'SetFromFrames()'.
                _MM = Convert.ToInt32(wkgStr.Substring(2, 2));
                _SS = Convert.ToInt32(wkgStr.Substring(4, 2));
                _FF = Convert.ToInt32(wkgStr.Substring(6, 2));
            }
            catch
            {
                // Handle parsing exception
            }
            SetFromFrames(_HH * FRAMES_PER_HOUR + _MM * FRAMES_PER_MIN + _SS * FRAMES_PER_SEC + _FF);
            if (hhMmSsFf.Contains('-'))
            {
                _Frames = -_Frames;
            }
        }

        private string CleanString(string unCleanString)
        {
            string cString = string.Empty;
            if (unCleanString == null)
            {
                return cString;
            }

            foreach (char ch in unCleanString)
            {
                if (ch >= '0' && ch <= '9')
                {
                    cString += ch;
                }
            }
            return cString;
        }

    }
}
