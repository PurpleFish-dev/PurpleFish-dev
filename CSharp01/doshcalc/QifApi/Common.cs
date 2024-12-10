using System;
using QifApi;

namespace QifApi
{
    internal static class Common
    {
        internal static bool GetBoolean(string value)
        {
            bool result = false;

            if ((bool.TryParse(value, out result) == false) && (value.Length > 0))
            {
                throw new InvalidCastException(Resources.InvalidBooleanFormat);
            }

            return result;

        }

        /*
		 * private static string GetRealDateString(string qifDateString)
        {
            // Find the apostraphe
            int i = qifDateString.IndexOf("'");

            // Prepare the return string
            string sRet = "";

            // If the apostraphe is present
            if (i != -1)
            {
                // Extract everything but the apostraphe
                sRet = qifDateString.Substring(0, i) + "/" + qifDateString.Substring(i + 1);

                // Replace spaces with zeros
                sRet = sRet.Replace(" ", "0");

                // Return the new string
                return sRet;
            }
            else
            {
                // Otherwise, just return the raw value
                return qifDateString;
            }
        }
		 * */

        internal static decimal GetDecimal(string value)
        {
            decimal result = 0;

            if (decimal.TryParse(value, out result) == false)
            {
                throw new InvalidCastException(Resources.InvalidDecimalFormat);
            }

            return result;
        }

        /*
		 * internal static DateTime GetDateTime(string value)
        {
            // Prepare the return value
            DateTime result = new DateTime();

            // If parsing the date string fails
            if (DateTime.TryParse(GetRealDateString(value), out result) == false)
            {
                // Identify that the value couldn't be formatted
                throw new InvalidCastException(Resources.InvalidDateFormat);
            }

            // Return the date value
            return result;
        }
		 * */

        internal static void DetermineDateFormat(string rawDate, ref QifDom.yearFormat yearFormat, ref QifDom.dayMonthFormat dayMonthFormat)
        {
            try
            {
                char[] delimiterChars = { ' ', ',', '/', '.', ':', '\t' };
                string[] tokens = rawDate.Split(delimiterChars);
                int A, B, C;
                if ((!int.TryParse(tokens[0], out A)) ||
                    (!int.TryParse(tokens[1], out B)) ||
                    (!int.TryParse(tokens[2], out C)))
                {
                    throw new InvalidCastException(Resources.InvalidDateFormat);
                }

                if (C <= 99)
                {
                    if (yearFormat == QifDom.yearFormat.yyyy) throw new InvalidCastException(Resources.InvalidDateFormat);
                    yearFormat = QifDom.yearFormat.yy;
                }
                else
                {
                    if (yearFormat == QifDom.yearFormat.yy) throw new InvalidCastException(Resources.InvalidDateFormat);
                    yearFormat = QifDom.yearFormat.yyyy;
                }

                if (B > 12)
                {
                    if (dayMonthFormat == QifDom.dayMonthFormat.ddmm) throw new InvalidCastException(Resources.InvalidDateFormat);
                    dayMonthFormat = QifDom.dayMonthFormat.mmdd;
                }

                if (A > 12)
                {
                    if (dayMonthFormat == QifDom.dayMonthFormat.mmdd) throw new InvalidCastException(Resources.InvalidDateFormat);
                    dayMonthFormat = QifDom.dayMonthFormat.ddmm;
                }
            }
            catch (Exception)
            {
                throw new InvalidCastException(Resources.InvalidDateFormat);
            }
        }

        internal static bool TryParseDate(string _rawDate, QifDom.yearFormat yearFormat, QifDom.dayMonthFormat dayMonthFormat, ref DateTime _date)
         {
            if ((yearFormat == QifDom.yearFormat.Undetermined) || (dayMonthFormat == QifDom.dayMonthFormat.Undetermined)) throw new InvalidCastException(Resources.InvalidDateFormat);
            try
			{
				char[] delimiterChars = { ' ', ',','/' ,'.', ':', '\t' };
                string[] tokens = _rawDate.Split(delimiterChars);
				int A, B, C;
				if(	(!int.TryParse(tokens[0], out A)) ||
					(!int.TryParse(tokens[1], out B)) ||
					(!int.TryParse(tokens[2], out C))	)
				{
					throw new InvalidCastException(Resources.InvalidDateFormat);
				}

                if (yearFormat == QifDom.yearFormat.yy)
                {
                    if (C < 71) C += 2000;
                    else C += 1900;
                }

                if (dayMonthFormat == QifDom.dayMonthFormat.ddmm) _date = new DateTime(C, B, A);
                else _date = new DateTime(C, A, B);

                return true;				
			}
			catch(Exception)
			{
				throw new InvalidCastException(Resources.InvalidDateFormat);
			}
		}       
    }
}

                
               

		/*internal static bool UpdateDateFormatAndParse(string value, ref QifDom.fileDateFormat format, ref DateTime date)
        {
			if(value == "None")
			{
				date = DateTime.MaxValue;
				return true;
			}
			
			try
			{
				char[] delimiterChars = { ' ', ',','/' ,'.', ':', '\t' };
				string[] tokens = value.Split(delimiterChars);
				int A, B, C;
				if(	(!int.TryParse(tokens[0], out A)) ||
					(!int.TryParse(tokens[1], out B)) ||
					(!int.TryParse(tokens[2], out C))	)
				{
					throw new InvalidCastException(Resources.InvalidDateFormat);
				}

				if(C > 99)
				{
					if( ((A > 12) && (C > 99)) || (format == QifDom.fileDateFormat.ddmmyyyy) )
					{
						date = new DateTime(C, B, A);
						return true;
					}
					else if( (format == QifDom.fileDateFormat.mmddyyyy) || ((B > 12) && (C > 99)) )
					{
						date = new DateTime(C, A, B);
						return true;
					}
					else
					{				
						format = QifDom.fileDateFormat.xxxxyyyy;
					}
				}				
				else
				{
					if(format == QifDom.fileDateFormat.ddmm19yy)
					{
						date = new DateTime(1900 + C, B, A);
						return true;
					}
					else if(format == QifDom.fileDateFormat.ddmm20yy)
					{
						date = new DateTime(2000 + C, B, A);
						return true;
					}
					else if(format == QifDom.fileDateFormat.mmdd19yy)
					{
						date = new DateTime(1900 + C, A, B);
						return true;
					}
					else if(format == QifDom.fileDateFormat.mmdd20yy)
					{
						date = new DateTime(2000 + C, A, B);
						return true;
					}

					if(A > 12)
					{
						format = QifDom.fileDateFormat.ddmmxxyy;
					}
					else if(B > 12)
					{
						format = QifDom.fileDateFormat.mmddxxyy;
					}				
				}			
				return false;			
			}
			catch(Exception)
			{
				throw new InvalidCastException(Resources.InvalidDateFormat);
			}
		}*/
       