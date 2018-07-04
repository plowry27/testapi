using System;
using System.Data.SqlClient;

namespace TodoApi.Other
{
    public static class Helper
    {
        public static string SafeGetString(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetString(colIndex);
            }
            return string.Empty;
        }

        public static int SafeGetInt(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetInt32(colIndex);
            }
            return 0;
        }

        public static DateTime ConvertStringToDateTime(string date)
        {
            if (date != null)
            {
                DateTime datetime = Convert.ToDateTime(date);
                return datetime;
            }
            else
            {
                DateTime datetime = Convert.ToDateTime(date);
                return System.DateTime.Now;
            }
        }
    }
}