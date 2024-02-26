
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MySql.Data.MySqlClient;
using System.Data;



namespace EtamConverter.Data
{
    public static class MySQLDB
    {
        public static string DBdatetime2string(object DBObject)
        {
            if (DBNull.Value.Equals(DBObject))
            {
                return string.Empty;
            }

            try
            {
                return ((DateTime)DBObject).ToShortDateString();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return string.Empty;
            }
        }

        public static DateTime DBdatetime2datetime(object DBObject)
        {
            if (DBNull.Value.Equals(DBObject))
            {
                return new DateTime(0);
            }

            try
            {
                return (DateTime)DBObject;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return DateTime.MinValue;
            }
        }

        public static DateTime DBdate2date(object DBObject)
        {
            if (DBNull.Value.Equals(DBObject))
            {
                return DateTime.MinValue;
            }

            try
            {
                return (DateTime)DBObject;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return DateTime.MinValue;
            }
        }

        public static DateTime? DBdate2ndate(object DBObject)
        {
            if (DBNull.Value.Equals(DBObject))
            {
                return null;
            }

            try
            {
                return (DateTime)DBObject;
            }
            catch
            {
                return null;
            }
        }

        public static long DBlong2long(object DBObject)
        {
            if (DBNull.Value.Equals(DBObject))
            {
                return 0;
            }

            try
            {
                return Convert.ToInt64(DBObject);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
        }

        public static int DBint2int(object DBObject)
        {
            if (DBNull.Value.Equals(DBObject))
            {
                return 0;
            }

            try
            {
                return (int)DBObject;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
        }

        public static short DBshort2short(object DBObject)
        {
            if (DBNull.Value.Equals(DBObject))
            {
                return 0;
            }

            try
            {
                return (short)DBObject;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
        }

        public static string DBint2string(object DBObject)
        {
            if (DBNull.Value.Equals(DBObject))
            {
                return string.Empty;
            }

            try
            {
                return DBObject.ToString();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return string.Empty;
            }
        }

        public static bool DBbool2bool(object DBObject)
        {
            if (DBNull.Value.Equals(DBObject))
            {
                return false;
            }

            try
            {
                return (bool)DBObject;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
        }

        public static bool bool2DBbool(bool boolfield)
        {
            return boolfield;
        }

        public static int DBint2bool(object DBObject)
        {
            if (DBNull.Value.Equals(DBObject))
            {
                return 0;
            }

            try
            {
                return Convert.ToBoolean(DBObject) ? 1 : 0;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return 0;
            }
        }

        public static int bool2DBint(bool boolfield)
        {
            return boolfield ? -1 : 0;
        }

        public static string DBlong2string(object DBObject)
        {
            if (DBNull.Value.Equals(DBObject))
            {
                return string.Empty;
            }

            try
            {
                return DBObject.ToString();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return string.Empty;
            }
        }

        public static decimal DBdecimal2decimal(object DBObject)
        {
            if (DBNull.Value.Equals(DBObject))
            {
                return 0;
            }

            try
            {
                return Convert.ToDecimal(DBObject);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return 0;
            }
        }

        public static string DBdecimal2string(object DBObject)
        {
            if (DBNull.Value.Equals(DBObject))
            {
                return string.Empty;
            }

            try
            {
                return DBObject.ToString();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return string.Empty;
            }
        }

        public static object datetime2DBDateTime(DateTime? DateField)
        {
            if (DateField == null)
            {
                return DBNull.Value;
            }

            try
            {
                return DateField.Value;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return DBNull.Value;
            }
        }

        public static object string2DBDateTime(string DateField)
        {
            if (string.IsNullOrWhiteSpace(DateField))
            {
                return DBNull.Value;
            }

            try
            {
                return Convert.ToDateTime(DateField);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return DBNull.Value;
            }
        }

        public static object string2DBint(string NumField)
        {
            if (string.IsNullOrWhiteSpace(NumField))
            {
                return DBNull.Value;
            }

            try
            {
                return Convert.ToInt32(NumField);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return DBNull.Value;
            }
        }

        public static object decimal2DBdecimal(decimal NumField)
        {
            try
            {
                return NumField;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return DBNull.Value;
            }
        }

        public static object string2DBdecimal(string NumField)
        {
            if (string.IsNullOrWhiteSpace(NumField))
            {
                return DBNull.Value;
            }

            try
            {
                return Convert.ToDecimal(NumField);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return DBNull.Value;
            }
        }

        public static object int2DBint(long IntField)
        {
            return IntField;
        }

        public static object int2DBintnull(long IntField)
        {
            if (IntField == 0)
            {
                return DBNull.Value;
            }

            return IntField;
        }

        public static object long2DBlong(long LongField)
        {
            if (LongField == 0)
            {
                return DBNull.Value;
            }

            return LongField;
        }

        public static object string2DBstring(string StringField)
        {
            if (string.IsNullOrWhiteSpace(StringField))
            {
                return DBNull.Value;
            }

            return StringField;
        }

        public static string DBstring2string(object DBObject)
        {
            if (DBNull.Value.Equals(DBObject))
            {
                return string.Empty;
            }

            try
            {
                return DBObject.ToString().Trim();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return string.Empty;
            }
        }

        public static char DBstring2char(object DBObject)
        {
            if (DBNull.Value.Equals(DBObject))
            {
                return ' ';
            }

            try
            {
                return DBObject.ToString()[0];
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return ' ';
            }
        }

        public static string stringEscSingleQuote(string StringField)
        {
            return StringField.Replace("'", "''");
        }

        public static string stringEscDoubleQuote(string StringField)
        {
            return StringField.Replace("\"", "\"\"");
        }

        public static object date2DBdate(DateTime DateField)
        {
            if (DateField != null)
            {
                return DateField;
            }
            else
            {
                return DBNull.Value;
            }
        }
    }

    internal static class SQLSVR
    {
        public const string SYSDATETIME = "SYSDATETIME()";
        public const string SYSUTCDATETIME = "SYSUTCDATETIME()";
        public const string SYSNOWDATETIME = "NOW()";
    }


    public class MYSQL_IO
    {
        protected MYSQL_DBIO _MySQLDBIO;
        protected MySqlCommand _MySQLCmd;
        public const string PARAMCHAR = "@";

        public MYSQL_IO(MYSQL_DBIO MySQLDBIO)
        {
            this._MySQLDBIO = MySQLDBIO;
            _MySQLCmd = new MySqlCommand();
            _MySQLCmd.Connection = _MySQLDBIO.MySQLConn;
        }

        public MySqlCommand MySQLCmd
        {
            get { return _MySQLCmd; }
        }

        public void WaitOne()
        {
            _MySQLDBIO.WaitOne();
        }

        public void ReleaseMutex()
        {
            _MySQLDBIO.ReleaseMutex();
        }

        public string SQLParam(string FieldName)
        {
            return PARAMCHAR + FieldName;
        }

        public string SQLFParam(string FieldName, string FieldOp = "=")
        {
            return FieldName + " " + FieldOp + " " + PARAMCHAR + FieldName;
        }

        public void AddFParam(string FieldName, object Value)
        {
            _MySQLCmd.Parameters.AddWithValue(PARAMCHAR + FieldName, Value);
        }

        public void SetFParam(string FieldName, object Value)
        {
            _MySQLCmd.Parameters[PARAMCHAR + FieldName].Value = Value;
        }
    }
}
