using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public static class TextHelper
    {
        public static string TimeFormatMinutes(int value)
        {
            return string.Format("{0:00}:{1:00}",
                        (value / 60) % 60,
                        value % 60);
        }

        public static int getNumberOfDays(int m, int y)
        {
            int leap = (1 - (y % 4 + 2) % (y % 4 + 1)) * ((y % 100 + 2) % (y % 100 + 1)) + (1 - (y % 400 + 2) % (y % 400 + 1));

            return 28 + ((m + (m / 8)) % 2) + 2 % m + ((1 + leap) / m) + (1 / m) - (leap / m);
        }

    }
}
