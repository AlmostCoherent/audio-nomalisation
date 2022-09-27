using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFMpeg
{
    public static class Extensions
    {
        public static string ExtractNumberFromFFMPEGLine(this string inputString) {
            StringBuilder sbNumber = new StringBuilder();
            foreach (Char k in inputString)
            {
                if (char.IsDigit(k) || ('.' == k) || ('-' == k))
                {
                    sbNumber.Append(k);
                }
            }

            string ret = sbNumber.ToString();
            try
            {
                float.Parse(ret);
            }
            catch
            {
                throw new ApplicationException("Failed parse " + inputString);
            }
            return ret;

        }
    }
}
